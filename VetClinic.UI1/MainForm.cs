using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;   // 🔥 BU SATIR
using System.Linq;
using System.ComponentModel;

namespace VetClinic.UI1
{

    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public static BindingList<Hasta> HastaListesi = new BindingList<Hasta>();
        public static List<Tuple<string, string>> Users = new List<Tuple<string, string>>();


       

        public MainForm()
        {
            InitializeComponent();
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
            panelHastaEkle.Visible = false;
            panelHastaListele.Visible = false;

            // DevExpress temasını devre dışı bırak, kendi renklerimi kullan
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LookAndFeel.UseWindowsXPTheme = false;
            
            // AccordionControl (Menü) renkleri - Honeydew yeşil tonu
            accMenu.LookAndFeel.UseDefaultLookAndFeel = false;
            accMenu.BackColor = Color.Honeydew;
            
            // Menü öğelerinin renkleri
            accMenu.Appearance.AccordionControl.BackColor = Color.Honeydew;
            accMenu.Appearance.AccordionControl.Options.UseBackColor = true;
            
            accMenu.Appearance.Item.Normal.BackColor = Color.Honeydew;
            accMenu.Appearance.Item.Normal.ForeColor = Color.DarkGreen;
            accMenu.Appearance.Item.Normal.Options.UseBackColor = true;
            accMenu.Appearance.Item.Normal.Options.UseForeColor = true;
            
            accMenu.Appearance.Item.Hovered.BackColor = Color.LightGreen;
            accMenu.Appearance.Item.Hovered.ForeColor = Color.DarkGreen;
            accMenu.Appearance.Item.Hovered.Options.UseBackColor = true;
            accMenu.Appearance.Item.Hovered.Options.UseForeColor = true;
            
            accMenu.Appearance.Group.Normal.BackColor = Color.Honeydew;
            accMenu.Appearance.Group.Normal.ForeColor = Color.DarkGreen;
            accMenu.Appearance.Group.Normal.Options.UseBackColor = true;
            accMenu.Appearance.Group.Normal.Options.UseForeColor = true;

            // PicBackground SİLİNDİ

            
            // Header resim yükleme kodu SİLİNDİ
            
            // Ana içerik paneline güzel bir arka plan rengi ver
            panelContent.BackColor = Color.Honeydew;

            // Anasayfa resmini yükle
            try
            {
                string path = System.IO.Path.Combine(Application.StartupPath, @"..\..\Resources\home_welcome.png");
                if (System.IO.File.Exists(path))
                {
                    picHome.Image = Image.FromFile(path);
                }
            }
            catch { }
        }
        
        // ANASAYFA - Tüm panelleri gizle, hoşgeldin ekranı kalsın
        private void accordionControlElementAnasayfa_Click(object sender, EventArgs e)
        {
            ShowFullScreenImage(true);
        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {

        }

        // Formu ana panel içinde açmak için yardımcı metod
        private void OpenFormInPanel(Form frm)
        {
            // Paneldeki diğer açık FORMLARI kapat/temizle (Menu ve HastaEkle paneli hariç)
            // Ters döngü ile silmek güvenlidir
            for (int i = panelContent.Controls.Count - 1; i >= 0; i--)
            {
                Control ctrl = panelContent.Controls[i];
                // Eğer kontrol bir Form ise (türetilmişse) ve yeni açılan değilse kapat
                if (ctrl is Form && ctrl != frm)
                {
                    ((Form)ctrl).Close(); // Formu kapat ve kaynakları serbest bırak
                    // panelContent.Controls.Remove(ctrl); // Close zaten Remove yapar
                }
            }

            // Sabit panelleri gizle
            panelHastaEkle.Visible = false; 
            panelHastaListele.Visible = false;
            ShowFullScreenImage(false);

            // Formu ayarla (Gömülü pencere)
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            
            // Panele ekle ve göster
            panelContent.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        // RANDEVU OLUŞTUR - Yeni randevu formu aç
        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            FrmRandevu frm = new FrmRandevu();
            frm.FormClosed += (s, args) => {
                // Randevu formu kapandığında anasayfaya dön
                ShowFullScreenImage(true);
            };
            OpenFormInPanel(frm); 
        }

        // RANDEVU LİSTESİ - Randevu listesi formunu aç
        private void accordionControlElement9_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frm = new FrmRandevuListesi();
            // frm.ShowDialog(); // POPUP İPTAL
            OpenFormInPanel(frm); // Gömülü aç
        }

        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
            // Hasta Ekle panelini görünür yap (çünkü panelHastaListele onun içinde)
            panelHastaEkle.Visible = true;
            panelHastaEkle.BringToFront();
            
            // İçindeki layoutControl1'i gizle, panelHastaListele'yi göster
            layoutControl1.Visible = false;
            panelHastaListele.Visible = true;
            panelHastaListele.BringToFront();

            // Grid'i yapılandır
            gridControl1.DataSource = null;
            gridControl1.DataSource = HastaListesi;
            
            var view = gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null)
            {
                view.PopulateColumns();
                
                // Kolon İsimlerini ve Görünürlüğünü Ayarla
                if (view.Columns["SahibiAd"] != null) { view.Columns["SahibiAd"].Caption = "HASTA AD"; view.Columns["SahibiAd"].Visible = true; }
                if (view.Columns["SahibiSoyad"] != null) { view.Columns["SahibiSoyad"].Caption = "HASTA SOYAD"; view.Columns["SahibiSoyad"].Visible = true; }
                if (view.Columns["DogumTarihi"] != null) { view.Columns["DogumTarihi"].Caption = "HASTA DOĞUM TARİHİ"; view.Columns["DogumTarihi"].Visible = true; }
                if (view.Columns["Cinsiyet"] != null) { view.Columns["Cinsiyet"].Caption = "HASTA CİNSİYET"; view.Columns["Cinsiyet"].Visible = true; }
                if (view.Columns["Tur"] != null) { view.Columns["Tur"].Caption = "HASTA TÜR"; view.Columns["Tur"].Visible = true; }
                
                // İstenmeyenleri Gizle
                if (view.Columns["HayvanAd"] != null) view.Columns["HayvanAd"].Visible = false;
                if (view.Columns["ResimYolu"] != null) view.Columns["ResimYolu"].Visible = false;

                // Başlık (Header) Stilini Renklendir - Koyu yeşil, beyaz yazı
                // Önce DevExpress temasını devre dışı bırak
                gridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
                gridControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
                
                view.Appearance.HeaderPanel.BackColor = Color.DarkGreen;
                view.Appearance.HeaderPanel.ForeColor = Color.White;
                view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                view.Appearance.HeaderPanel.Options.UseBackColor = true;
                view.Appearance.HeaderPanel.Options.UseForeColor = true;
                view.Appearance.HeaderPanel.Options.UseFont = true;
                
                // ColumnHeader için de ayarla
                view.PaintStyleName = "Flat";
                
                // Seçim efektini kapat (mavi renk olmasın)
                view.OptionsSelection.EnableAppearanceFocusedCell = false;
                view.OptionsSelection.EnableAppearanceFocusedRow = false;
                view.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;

                // Her satıra farklı renk vermek için RowStyle event'i
                view.RowStyle -= View_RowStyle; // Önce kaldır (çift ekleme olmasın)
                view.RowStyle += View_RowStyle;

                // Silme butonu kolonu ekle (eğer yoksa)
                if (view.Columns["SilButonu"] == null)
                {
                    var silKolon = view.Columns.AddVisible("SilButonu", "SİL");
                    silKolon.UnboundType = DevExpress.Data.UnboundColumnType.String;
                    silKolon.Width = 60;
                    silKolon.AppearanceHeader.BackColor = Color.DarkRed;
                    silKolon.AppearanceHeader.ForeColor = Color.White;
                    silKolon.AppearanceHeader.Options.UseBackColor = true;
                    silKolon.AppearanceHeader.Options.UseForeColor = true;
                    
                    // Buton repository item
                    var btnSil = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
                    btnSil.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
                    btnSil.Buttons[0].Caption = "X";
                    btnSil.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    btnSil.ButtonClick += BtnSil_ButtonClick;
                    gridControl1.RepositoryItems.Add(btnSil);
                    silKolon.ColumnEdit = btnSil;
                }

                view.BestFitColumns();
            }
            
            // Header moduna geç
            ShowFullScreenImage(false);
        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            // Ana paneli görünür yap
            panelHastaEkle.Visible = true;
            panelHastaEkle.BringToFront();
            
            // Hasta ekleme formunu göster, listeyi gizle
            layoutControl1.Visible = true;
            panelHastaListele.Visible = false;
            
            panelHastaEkle.Parent.Controls.SetChildIndex(panelHastaEkle, 0);
            
            // Header moduna geç
            ShowFullScreenImage(false);
        }


        
        // Görsel modunu değiştir: True = Tam Ekran (Anasayfa), False = Header (Sayfalar)
        // Görsel modunu değiştir: KULLANIM DIŞI
        // Görsel modunu değiştir: True = Tam Ekran (Anasayfa), False = Gizli (Sayfalar)
        private void ShowFullScreenImage(bool isFull)
        {
            picHome.Visible = isFull;
            if (isFull)
            {
                panelHastaEkle.Visible = false;
                // panelContent içindeki diğer özel formları da gizle/kapat
                foreach (Control ctrl in panelContent.Controls)
                {
                    if (ctrl is Form) ctrl.Hide();
                }
                
                // Dashboard paneli göster
                ShowDashboard();
            }
        }
        
        // Dashboard Panel - Anasayfa Özet Bilgileri
        private Panel dashboardPanel;
        
        private void ShowDashboard()
        {
            // Varsa kaldır
            if (dashboardPanel != null)
            {
                panelContent.Controls.Remove(dashboardPanel);
                dashboardPanel.Dispose();
            }
            
            // Yeni dashboard panel oluştur
            dashboardPanel = new Panel();
            dashboardPanel.Size = new Size(700, 400);
            dashboardPanel.Location = new Point((panelContent.Width - 700) / 2, 30);
            dashboardPanel.BackColor = Color.Transparent;
            
            // Hoşgeldin yazısı
            Label lblHosgeldin = new Label();
            lblHosgeldin.Text = LoginForm.AdminMi ? "HOŞGELDİN ADMİN" : "HOŞGELDİNİZ";
            lblHosgeldin.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblHosgeldin.ForeColor = Color.DarkGreen;
            lblHosgeldin.AutoSize = true;
            lblHosgeldin.Location = new Point(0, 0);
            dashboardPanel.Controls.Add(lblHosgeldin);
            
            // Kullanıcı adı
            Label lblKullanici = new Label();
            lblKullanici.Text = "Giriş Yapan: " + LoginForm.GirisYapanKullanici;
            lblKullanici.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            lblKullanici.ForeColor = Color.DarkSlateGray;
            lblKullanici.AutoSize = true;
            lblKullanici.Location = new Point(0, 50);
            dashboardPanel.Controls.Add(lblKullanici);
            
            // ========== İSTATİSTİK KUTULARI ==========
            int boxY = 100;
            
            // Hasta Sayısı Kutusu
            Panel boxHasta = CreateStatBox("TOPLAM HASTA", HastaListesi.Count.ToString(), Color.FromArgb(0, 150, 136), 0, boxY);
            dashboardPanel.Controls.Add(boxHasta);
            
            // Randevu Sayısı Kutusu
            int randevuSayisi = FrmRandevu.RandevuListesi.Count;
            Panel boxRandevu = CreateStatBox("TOPLAM RANDEVU", randevuSayisi.ToString(), Color.FromArgb(33, 150, 243), 230, boxY);
            dashboardPanel.Controls.Add(boxRandevu);
            
            // Bekleyen Randevu Sayısı
            int bekleyenSayisi = 0;
            foreach (var r in FrmRandevu.RandevuListesi)
            {
                if (r.Durum == "Bekliyor") bekleyenSayisi++;
            }
            Panel boxBekleyen = CreateStatBox("BEKLEYEN RANDEVU", bekleyenSayisi.ToString(), Color.FromArgb(255, 152, 0), 460, boxY);
            dashboardPanel.Controls.Add(boxBekleyen);
            
            // Tarih/Saat
            Label lblTarih = new Label();
            lblTarih.Text = DateTime.Now.ToString("dd MMMM yyyy - HH:mm");
            lblTarih.Font = new Font("Segoe UI", 14F);
            lblTarih.ForeColor = Color.Gray;
            lblTarih.AutoSize = true;
            lblTarih.Location = new Point(0, 280);
            dashboardPanel.Controls.Add(lblTarih);
            
            // Admin Bilgisi
            if (LoginForm.AdminMi)
            {
                Label lblAdmin = new Label();
                lblAdmin.Text = "✓ Admin yetkileriniz aktif";
                lblAdmin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                lblAdmin.ForeColor = Color.DarkGreen;
                lblAdmin.AutoSize = true;
                lblAdmin.Location = new Point(0, 320);
                dashboardPanel.Controls.Add(lblAdmin);
            }
            
            panelContent.Controls.Add(dashboardPanel);
            dashboardPanel.BringToFront();
        }
        
        private Panel CreateStatBox(string baslik, string deger, Color renk, int x, int y)
        {
            Panel box = new Panel();
            box.Size = new Size(210, 150);
            box.Location = new Point(x, y);
            box.BackColor = renk;
            
            Label lblBaslik = new Label();
            lblBaslik.Text = baslik;
            lblBaslik.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBaslik.ForeColor = Color.White;
            lblBaslik.Location = new Point(15, 15);
            lblBaslik.AutoSize = true;
            box.Controls.Add(lblBaslik);
            
            Label lblDeger = new Label();
            lblDeger.Text = deger;
            lblDeger.Font = new Font("Segoe UI", 48F, FontStyle.Bold);
            lblDeger.ForeColor = Color.White;
            lblDeger.Location = new Point(15, 50);
            lblDeger.AutoSize = true;
            box.Controls.Add(lblDeger);
            
            return box;
        }

        private void accCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void accMenu_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Boş alan kontrolleri
            if (string.IsNullOrWhiteSpace(txtAd.Text))
            {
                MessageBox.Show("Ad alanı boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAd.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSoyad.Text))
            {
                MessageBox.Show("Soyad alanı boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoyad.Focus();
                return;
            }

            // Değerleri al
            string ad = txtAd.Text.Trim();
            string soyad = txtSoyad.Text.Trim();
            DateTime dogumTarihi = dateDogumTarihi.EditValue != null ? dateDogumTarihi.DateTime : DateTime.Now;
            string cinsiyet = !string.IsNullOrWhiteSpace(cmbCinsiyet.Text) ? cmbCinsiyet.Text : "Bilinmiyor";
            string tur = !string.IsNullOrWhiteSpace(cmbTur.Text) ? cmbTur.Text : "Bilinmiyor";

            // Kaydet
            HastaListesi.Add(new Hasta
            {
                SahibiAd = ad,
                SahibiSoyad = soyad,
                HayvanAd = "Bilinmiyor",
                Tur = tur,
                DogumTarihi = dogumTarihi,
                Cinsiyet = cinsiyet,
                ResimYolu = null
            });

            // İstenen çıktı formatı
            string mesaj = string.Format("Hasta Başarıyla Kaydedildi!\n\n" +
                           "AD SOYAD: {0} {1}\n" +
                           "DOĞUM TARİHİ: {2:dd.MM.yyyy}\n" +
                           "CİNSİYET: {3}\n" +
                           "TÜR: {4}\n" +
                           "KAYITLI HASTA SAYISI: {5}",
                           ad, soyad,
                           dogumTarihi,
                           cinsiyet,
                           tur,
                           HastaListesi.Count);
            // Dosyaya kaydet
            HastaVeriYonetimi.Kaydet(HastaListesi);

            MessageBox.Show(mesaj, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FormuTemizle();
        }

        private void FormuTemizle()
        {
            txtAd.Text = "";
            txtSoyad.Text = "";
            dateDogumTarihi.EditValue = null;
            cmbCinsiyet.SelectedIndex = -1;
            cmbTur.SelectedIndex = -1;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Başlangıçta tam ekran görsel (Anasayfa modu)
            ShowFullScreenImage(true);

            // Önce listeyi temizle (tekrar açılırsa çift kayıt olmasın)
            HastaListesi.Clear();

            // Dosyadan hastaları yükle
            var kayitliHastalar = HastaVeriYonetimi.Yukle();
            foreach (var h in kayitliHastalar)
            {
                HastaListesi.Add(h);
            }

            gridControl1.DataSource = HastaListesi;

            var view = gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView;

            view.OptionsBehavior.AutoPopulateColumns = true;
            view.PopulateColumns();
            view.OptionsView.ShowGroupPanel = false;
            view.BestFitColumns();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Kapatmadan önce hastaları kaydet
            HastaVeriYonetimi.Kaydet(HastaListesi);
        }

        // Her satıra farklı renk veren event handler
        private void View_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                // Pastel renkler dizisi
                Color[] renkler = new Color[] {
                    Color.FromArgb(255, 230, 230), // Açık kırmızı/pembe
                    Color.FromArgb(230, 255, 230), // Açık yeşil
                    Color.FromArgb(230, 230, 255), // Açık mavi
                    Color.FromArgb(255, 255, 200), // Açık sarı
                    Color.FromArgb(255, 220, 255), // Açık mor
                    Color.FromArgb(220, 255, 255), // Açık turkuaz
                    Color.FromArgb(255, 235, 205), // Açık turuncu
                    Color.FromArgb(245, 245, 220)  // Açık bej
                };

                int renkIndex = e.RowHandle % renkler.Length;
                e.Appearance.BackColor = renkler[renkIndex];
                e.Appearance.ForeColor = Color.Black; // Yazı siyah, okunaklı
            }
        }

        // Silme butonu click event handler
        private void BtnSil_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var view = gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view != null)
            {
                var focusedRow = view.GetFocusedRow() as Hasta;
                if (focusedRow != null)
                {
                    var sonuc = MessageBox.Show(
                        string.Format("{0} {1} silinsin mi?", focusedRow.SahibiAd, focusedRow.SahibiSoyad), 
                        "Hasta Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (sonuc == DialogResult.Yes)
                    {
                        HastaListesi.Remove(focusedRow);
                        HastaVeriYonetimi.Kaydet(HastaListesi);
                        gridControl1.RefreshDataSource();
                    }
                }
            }
        }


    }
}
