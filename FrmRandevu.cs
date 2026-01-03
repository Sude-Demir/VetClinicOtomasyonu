using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VetClinic.UI1
{
    public partial class FrmRandevu : XtraForm
    {
        public static System.ComponentModel.BindingList<Randevu> RandevuListesi { get; private set; }

        // Static constructor to initialize from database
        static FrmRandevu()
        {
            LoadFromDatabase();
        }

        public static void LoadFromDatabase()
        {
            using (var db = new VetClinic.UI1.Data.VetClinicContext())
            {
                db.EnsureSeeded();
                var dbRandevular = db.Randevular.ToList();
                var randevular = dbRandevular.Select(r => new Randevu
                {
                    Id = r.Id,
                    Tur = r.Tur,
                    Durum = r.Durum,
                    HastaAd = r.HastaAd,
                    HastaSoyad = r.HastaSoyad,
                    RandevuTarihi = r.RandevuTarihi,
                    Aciklama = r.Aciklama,
                    Hekim = r.Hekim
                }).ToList();
                
                if (RandevuListesi == null)
                    RandevuListesi = new System.ComponentModel.BindingList<Randevu>(randevular);
                else
                {
                    RandevuListesi.Clear();
                    foreach (var r in randevular)
                        RandevuListesi.Add(r);
                }
            }
        }

        // Simüle edilmiş dolu saatler
        private List<string> doluSaatler = new List<string> { "10:00", "11:00", "14:00", "15:30" };
        
        // Seçilen değerler
        private string secilenHekim = "";
        private DateTime secilenTarih = DateTime.Today;
        private string secilenSaat = "";
        private string secilenTip = "";
        private string secilenSure = "30 dk";
        private string secilenAciliyet = "Normal";
        private string secilenHasta = "";

        // UI Kontrolleri
        private SimpleButton btnAcPopup;
        private Panel panelOzet;
        private Label lblOzet;
        private Panel panelPopup;
        private Panel panelOverlay;
        private FlowLayoutPanel flpSaatler;

        public FrmRandevu()
        {
            InitializeComponent();
            BuildUI();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new Size(900, 650);
            this.Name = "FrmRandevu";
            this.Text = "Randevu Oluştur";
            this.FormBorderStyle = FormBorderStyle.None;
            this.ResumeLayout(false);
        }

        private void BuildUI()
        {
            // Arka plan resmi
            try {
                string resPath = string.Empty;
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                for (int i = 0; i < 5; i++) {
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "customer_bg.png");
                    if (System.IO.File.Exists(checkPath)) { resPath = checkPath; break; }
                    currentDir = System.IO.Directory.GetParent(currentDir)?.FullName;
                    if (currentDir == null) break;
                }
                if (!string.IsNullOrEmpty(resPath) && System.IO.File.Exists(resPath)) {
                    this.BackgroundImage = Image.FromFile(resPath);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
            } catch { }

            // === ANA PANEL (Özet) ===
            panelOzet = new Panel();
            panelOzet.Size = new Size(500, 400);
            panelOzet.BackColor = Color.FromArgb(245, 255, 255, 255);
            panelOzet.Location = new Point((this.ClientSize.Width - 500) / 2, 120);
            this.Controls.Add(panelOzet);

            Label lblBaslik = new Label();
            lblBaslik.Text = "📅 RANDEVU OLUŞTUR";
            lblBaslik.Font = new Font("Segoe UI Black", 22F, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(200, 100, 30); // Turuncu tema
            lblBaslik.Location = new Point(20, 20);
            lblBaslik.AutoSize = true;
            panelOzet.Controls.Add(lblBaslik);

            // Seçenekleri Aç Butonu
            btnAcPopup = new SimpleButton();
            btnAcPopup.Text = "🔧 Seçenekleri Aç";
            btnAcPopup.Size = new Size(200, 50);
            btnAcPopup.Location = new Point(150, 80);
            btnAcPopup.Appearance.BackColor = Color.FromArgb(210, 110, 30); // Turuncu tema
            btnAcPopup.Appearance.ForeColor = Color.White;
            btnAcPopup.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnAcPopup.Appearance.Options.UseBackColor = true;
            btnAcPopup.Appearance.Options.UseForeColor = true;
            btnAcPopup.Appearance.Options.UseFont = true;
            btnAcPopup.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            btnAcPopup.Cursor = Cursors.Hand;
            btnAcPopup.Click += (s, e) => ShowPopup();
            panelOzet.Controls.Add(btnAcPopup);

            // Özet Görüntüleme
            lblOzet = new Label();
            lblOzet.Text = "Henüz seçim yapılmadı.\nLütfen 'Seçenekleri Aç' butonuna tıklayın.";
            lblOzet.Font = new Font("Segoe UI", 12F);
            lblOzet.ForeColor = Color.DimGray;
            lblOzet.Location = new Point(30, 150);
            lblOzet.Size = new Size(440, 230);
            panelOzet.Controls.Add(lblOzet);

            this.Resize += (s, e) => {
                panelOzet.Location = new Point((this.ClientSize.Width - panelOzet.Width) / 2, 120);
            };

            CreatePopupPanel();
        }

        private void CreatePopupPanel()
        {
            // Overlay (karartma)
            panelOverlay = new Panel();
            panelOverlay.Dock = DockStyle.Fill;
            panelOverlay.BackColor = Color.FromArgb(150, 0, 0, 0);
            panelOverlay.Visible = false;
            panelOverlay.Click += (s, e) => HidePopup();
            this.Controls.Add(panelOverlay);

            // Popup Panel
            panelPopup = new Panel();
            panelPopup.Size = new Size(420, 580);
            panelPopup.BackColor = Color.White;
            panelPopup.Visible = false;
            panelPopup.Padding = new Padding(15);
            this.Controls.Add(panelPopup);

            int y = 10;

            // Başlık + Kapat
            Label lblPopupTitle = new Label();
            lblPopupTitle.Text = "📋 Randevu Detayları";
            lblPopupTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblPopupTitle.ForeColor = Color.FromArgb(50, 100, 150);
            lblPopupTitle.Location = new Point(15, y);
            lblPopupTitle.AutoSize = true;
            panelPopup.Controls.Add(lblPopupTitle);

            SimpleButton btnKapat = new SimpleButton();
            btnKapat.Text = "✕";
            btnKapat.Size = new Size(35, 35);
            btnKapat.Location = new Point(370, y);
            btnKapat.Appearance.BackColor = Color.Salmon;
            btnKapat.Appearance.ForeColor = Color.White;
            btnKapat.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnKapat.Appearance.Options.UseBackColor = true;
            btnKapat.Appearance.Options.UseForeColor = true;
            btnKapat.Appearance.Options.UseFont = true;
            btnKapat.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            btnKapat.Click += (s, e) => HidePopup();
            panelPopup.Controls.Add(btnKapat);

            y += 45;

            // === HEKİM SEÇİMİ ===
            AddLabel(panelPopup, "👨‍⚕️ Hekim:", 15, y);
            y += 22;
            ComboBoxEdit cmbHekim = new ComboBoxEdit();
            // cmbHekim.Properties.Items.AddRange(new[] { "Dr. Ahmet - Genel Cerrahi", "Dr. Elif - İç Hastalıkları", "Dr. Mehmet - Ortopedi" }); // Eski statik yapı
            cmbHekim.Size = new Size(390, 30);
            cmbHekim.Location = new Point(15, y);
            cmbHekim.EditValueChanged += (s, e) => secilenHekim = cmbHekim.Text;
            panelPopup.Controls.Add(cmbHekim);
            y += 38;

            // === TARİH SEÇİMİ ===
            AddLabel(panelPopup, "📆 Tarih:", 15, y);
            y += 22;
            DateEdit dateEdit = new DateEdit();
            dateEdit.Properties.MinValue = DateTime.Today;
            dateEdit.DateTime = DateTime.Today;
            dateEdit.Size = new Size(390, 30);
            dateEdit.Location = new Point(15, y);
            dateEdit.EditValueChanged += (s, e) => {
                secilenTarih = dateEdit.DateTime;
                UpdateSaatButonlari();
            };
            panelPopup.Controls.Add(dateEdit);
            y += 38;

            // === SAAT SEÇİMİ ===
            AddLabel(panelPopup, "⏰ Saat:", 15, y);
            y += 22;
            flpSaatler = new FlowLayoutPanel();
            flpSaatler.Size = new Size(390, 70);
            flpSaatler.Location = new Point(15, y);
            flpSaatler.AutoScroll = true;
            flpSaatler.BackColor = Color.FromArgb(245, 245, 245);
            panelPopup.Controls.Add(flpSaatler);
            GenerateSaatButonlari();
            y += 78;

            // === RANDEVU TİPİ ===
            AddLabel(panelPopup, "📋 Randevu Tipi:", 15, y);
            y += 22;
            ComboBoxEdit cmbTip = new ComboBoxEdit();
            cmbTip.Properties.Items.AddRange(new[] { "Muayene", "Aşı", "Kontrol", "Tahlil", "Acil" });
            cmbTip.Size = new Size(390, 30);
            cmbTip.Location = new Point(15, y);
            cmbTip.EditValueChanged += (s, e) => secilenTip = cmbTip.Text;
            panelPopup.Controls.Add(cmbTip);
            y += 38;

            // === RANDEVU SÜRESİ ===
            AddLabel(panelPopup, "⏱️ Süre:", 15, y);
            y += 22;
            RadioGroup rgSure = new RadioGroup();
            rgSure.Properties.Items.AddRange(new[] {
                new DevExpress.XtraEditors.Controls.RadioGroupItem("15 dk", "15 dk"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem("30 dk", "30 dk"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem("45 dk", "45 dk")
            });
            rgSure.Size = new Size(390, 28);
            rgSure.Location = new Point(15, y);
            rgSure.EditValue = "30 dk";
            rgSure.EditValueChanged += (s, e) => secilenSure = rgSure.EditValue?.ToString() ?? "30 dk";
            panelPopup.Controls.Add(rgSure);
            y += 35;

            // === ACİLİYET ===
            AddLabel(panelPopup, "🚨 Aciliyet:", 15, y);
            y += 22;
            RadioGroup rgAciliyet = new RadioGroup();
            rgAciliyet.Properties.Items.AddRange(new[] {
                new DevExpress.XtraEditors.Controls.RadioGroupItem("Normal", "Normal"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem("Orta", "Orta"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem("Acil", "Acil")
            });
            rgAciliyet.Size = new Size(390, 28);
            rgAciliyet.Location = new Point(15, y);
            rgAciliyet.EditValue = "Normal";
            rgAciliyet.EditValueChanged += (s, e) => secilenAciliyet = rgAciliyet.EditValue?.ToString() ?? "Normal";
            panelPopup.Controls.Add(rgAciliyet);
            y += 35;

            // === HASTA/PET SEÇİMİ ===
            AddLabel(panelPopup, "🐾 Hasta/Pet:", 15, y);
            y += 22;
            ComboBoxEdit cmbHasta = new ComboBoxEdit();
            // cmbHasta.Properties.Items.AddRange(new[] { "Pamuk - Kedi", "Boncuk - Köpek", "Leo - Köpek", "Karabaş - Köpek", "Maviş - Kuş" }); // Eski statik yapı
            cmbHasta.Size = new Size(390, 30);
            cmbHasta.Location = new Point(15, y);
            cmbHasta.EditValueChanged += (s, e) => secilenHasta = cmbHasta.Text;
            panelPopup.Controls.Add(cmbHasta);
            y += 45;

            // Veritabanından verileri çek ve ComboBox'ları doldur
            LoadDatabaseData(cmbHekim, cmbHasta);

            // === BUTONLAR ===
            SimpleButton btnKaydet = new SimpleButton();
            btnKaydet.Text = "✅ Seçimleri Kaydet";
            btnKaydet.Size = new Size(185, 45);
            btnKaydet.Location = new Point(15, y);
            btnKaydet.Appearance.BackColor = Color.SeaGreen;
            btnKaydet.Appearance.ForeColor = Color.White;
            btnKaydet.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnKaydet.Appearance.Options.UseBackColor = true;
            btnKaydet.Appearance.Options.UseForeColor = true;
            btnKaydet.Appearance.Options.UseFont = true;
            btnKaydet.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            btnKaydet.Click += (s, e) => KaydetVeKapat();
            panelPopup.Controls.Add(btnKaydet);

            SimpleButton btnVazgec = new SimpleButton();
            btnVazgec.Text = "❌ Vazgeç";
            btnVazgec.Size = new Size(185, 45);
            btnVazgec.Location = new Point(210, y);
            btnVazgec.Appearance.BackColor = Color.Gray;
            btnVazgec.Appearance.ForeColor = Color.White;
            btnVazgec.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnVazgec.Appearance.Options.UseBackColor = true;
            btnVazgec.Appearance.Options.UseForeColor = true;
            btnVazgec.Appearance.Options.UseFont = true;
            btnVazgec.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            btnVazgec.Click += (s, e) => HidePopup();
            panelPopup.Controls.Add(btnVazgec);
        }

        private void LoadDatabaseData(ComboBoxEdit cmbHekim, ComboBoxEdit cmbHasta)
        {
            try
            {
                using (var db = new Data.VetClinicContext())
                {
                    // 1. Hekimleri Yönetici Panelindeki Personellerden Çek
                    var hekimler = db.Personeller
                        .Where(p => p.Gorev.Contains("Hekim") || p.Yetki == "Veteriner")
                        .Select(p => $"{p.AdSoyad} - {p.Uzmanlik}")
                        .ToList();

                    cmbHekim.Properties.Items.Clear();
                    if (hekimler.Any())
                    {
                        cmbHekim.Properties.Items.AddRange(hekimler);
                    }
                    else
                    {
                        // Veritabanı boşsa veya hekim bulunamadıysa görseldeki gerçek hekimleri ekle
                        cmbHekim.Properties.Items.AddRange(new[] { "Dr. Ayşe Yılmaz - İç Hastalıkları", "Dr. Mehmet Karaca - Cerrahi" });
                    }

                    // 2. Mevcut Müşterinin Hayvanlarını Çek
                    string email = LoginForm.GirisYapanKullanici;
                    var kullanici = db.Kullanicilar.FirstOrDefault(k => k.Email == email);
                    
                    cmbHasta.Properties.Items.Clear();
                    if (kullanici != null)
                    {
                        // Önce kullanıcı kaydındaki direkt hayvanı ekle
                        if (!string.IsNullOrEmpty(kullanici.HayvanAdi))
                        {
                            cmbHasta.Properties.Items.Add($"{kullanici.HayvanAdi} - {kullanici.HayvanTurCins}");
                        }

                        // Sonra HastaDetay tablosunda bu kişi adına kayıtlı diğer hayvanları bul
                        var ekHastalar = db.Hastalar
                            .Where(h => h.Sahibi == kullanici.AdSoyad)
                            .Select(h => $"{h.HayvanAdi} - {h.Tur}")
                            .ToList();

                        foreach (var h in ekHastalar)
                        {
                            if (!cmbHasta.Properties.Items.Contains(h))
                                cmbHasta.Properties.Items.Add(h);
                        }
                    }

                    // Eğer hiç hayvan bulunamadıysa örnekleri göster
                    if (cmbHasta.Properties.Items.Count == 0)
                    {
                        cmbHasta.Properties.Items.AddRange(new[] { "Pamuk - Kedi", "Boncuk - Köpek" });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Veri yükleme hatası: " + ex.Message);
            }
        }

        private void AddLabel(Control parent, string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lbl.ForeColor = Color.DimGray;
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;
            parent.Controls.Add(lbl);
        }

        private void GenerateSaatButonlari()
        {
            flpSaatler.Controls.Clear();
            string[] saatler = { "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30" };

            foreach (var saat in saatler)
            {
                SimpleButton btn = new SimpleButton();
                btn.Text = saat;
                btn.Size = new Size(60, 28);
                btn.Tag = saat;

                bool dolu = doluSaatler.Contains(saat);
                bool secili = saat == secilenSaat;

                if (secili)
                {
                    btn.Appearance.BackColor = Color.Gold;
                    btn.Appearance.ForeColor = Color.Black;
                }
                else if (dolu)
                {
                    btn.Appearance.BackColor = Color.IndianRed;
                    btn.Appearance.ForeColor = Color.White;
                    btn.Enabled = false;
                }
                else
                {
                    btn.Appearance.BackColor = Color.MediumSeaGreen;
                    btn.Appearance.ForeColor = Color.White;
                }

                btn.Appearance.Options.UseBackColor = true;
                btn.Appearance.Options.UseForeColor = true;
                btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;

                btn.Click += (s, e) => {
                    secilenSaat = btn.Tag.ToString();
                    GenerateSaatButonlari(); // Yeniden çiz
                };

                flpSaatler.Controls.Add(btn);
            }
        }

        private void UpdateSaatButonlari()
        {
            // Tarih değişince dolu saatleri güncelle (simülasyon)
            // Gerçek uygulamada DB'den çekilir
            GenerateSaatButonlari();
        }

        private void ShowPopup()
        {
            panelOverlay.Visible = true;
            panelOverlay.BringToFront();
            panelPopup.Location = new Point((this.ClientSize.Width - panelPopup.Width) / 2, (this.ClientSize.Height - panelPopup.Height) / 2);
            panelPopup.Visible = true;
            panelPopup.BringToFront();
        }

        private void HidePopup()
        {
            panelPopup.Visible = false;
            panelOverlay.Visible = false;
        }

        private void KaydetVeKapat()
        {
            // Validasyon
            if (string.IsNullOrEmpty(secilenHekim) || string.IsNullOrEmpty(secilenSaat) || 
                string.IsNullOrEmpty(secilenTip) || string.IsNullOrEmpty(secilenHasta))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Saat parse et
            var saatParts = secilenSaat.Split(':');
            int saat = int.Parse(saatParts[0]);
            int dakika = int.Parse(saatParts[1]);
            DateTime randevuTarihi = secilenTarih.Date.AddHours(saat).AddMinutes(dakika);

            // Hasta bilgilerini ayır (Ad - Tür formatında)
            string hastaAd = secilenHasta.Split('-')[0].Trim();

            // Yeni ID oluştur
            int yeniId = RandevuListesi.Count > 0 ? RandevuListesi.Max(r => r.Id) + 1 : 1;

            // Yeni randevu oluştur
            Randevu yeniRandevu = new Randevu
            {
                Id = yeniId,
                Tur = secilenTip,
                Durum = secilenAciliyet == "Acil" ? "Acil" : "Beklemede",
                HastaAd = hastaAd,
                HastaSoyad = LoginForm.GirisYapanKullanici ?? "Müşteri",
                RandevuTarihi = randevuTarihi,
                Aciklama = $"Süre: {secilenSure}, Aciliyet: {secilenAciliyet}",
                Hekim = secilenHekim.Split('-')[0].Trim()
            };


            // Veritabanına kaydet
            using (var db = new VetClinic.UI1.Data.VetClinicContext())
            {
                db.Randevular.Add(new VetClinic.UI1.Data.RandevuEntity
                {
                    Tur = yeniRandevu.Tur,
                    Durum = yeniRandevu.Durum,
                    HastaAd = yeniRandevu.HastaAd,
                    HastaSoyad = yeniRandevu.HastaSoyad,
                    RandevuTarihi = yeniRandevu.RandevuTarihi,
                    Aciklama = yeniRandevu.Aciklama,
                    Hekim = yeniRandevu.Hekim
                });
                db.SaveChanges();
            }

            // Listeyi veritabanından yeniden yükle
            LoadFromDatabase();

            // Özeti güncelle
            string ozet = $"✅ Randevu Başarıyla Oluşturuldu!\n\n" +
                          $"👨‍⚕️ Hekim: {secilenHekim}\n" +
                          $"📆 Tarih: {secilenTarih:dd.MM.yyyy}\n" +
                          $"⏰ Saat: {secilenSaat}\n" +
                          $"📋 Tip: {secilenTip}\n" +
                          $"⏱️ Süre: {secilenSure}\n" +
                          $"🚨 Aciliyet: {secilenAciliyet}\n" +
                          $"🐾 Hasta: {secilenHasta}\n\n" +
                          $"📌 Randevunuz 'Randevularım' menüsünde görünecektir.";

            lblOzet.Text = ozet;
            lblOzet.ForeColor = Color.DarkGreen;

            MessageBox.Show("Randevunuz başarıyla oluşturuldu!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            HidePopup();
        }
    }
}
