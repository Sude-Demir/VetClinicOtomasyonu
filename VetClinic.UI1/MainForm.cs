using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VetClinic.UI1.Data;
using Microsoft.EntityFrameworkCore;
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
            
            // Menüleri Oluştur
            InitializeAdminMenu();
            InitializeCustomerMenu();

            panelHastaEkle.Visible = false;
            panelHastaListele.Visible = false;

            // DevExpress temasını devre dışı bırak, kendi renklerimi kullan
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LookAndFeel.UseWindowsXPTheme = false;
            
            // AccordionControl (Menü) - Estetik ve Renk Ayarları
            accMenu.LookAndFeel.UseDefaultLookAndFeel = false;
            accMenu.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat; // Flat stil
            accMenu.BackColor = Color.Honeydew;
            accMenu.Appearance.AccordionControl.BackColor = Color.Honeydew;
            accMenu.Appearance.AccordionControl.Options.UseBackColor = true;
            
            // Font Ayarları
            Font headerFont = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            Font itemFont = new Font("Segoe UI", 11F, FontStyle.Bold);

            // RENK PALETİ
            Color normalBack = Color.Honeydew;
            Color normalFore = Color.DarkSlateGray;
            Color hoverBack = Color.FromArgb(220, 245, 220); // Açık Yeşil Hover
            Color hoverFore = Color.DarkGreen;
            Color pressBack = Color.SeaGreen; // Mavi yerine Estetik Yeşil
            Color pressFore = Color.White;

            // 1. ITEM (Normal, Hover, Pressed)
            // Normal
            accMenu.Appearance.Item.Normal.BackColor = normalBack;
            accMenu.Appearance.Item.Normal.ForeColor = normalFore;
            accMenu.Appearance.Item.Normal.Font = itemFont;
            accMenu.Appearance.Item.Normal.Options.UseBackColor = true;
            accMenu.Appearance.Item.Normal.Options.UseForeColor = true;
            accMenu.Appearance.Item.Normal.Options.UseFont = true;

            // Hover
            accMenu.Appearance.Item.Hovered.BackColor = hoverBack;
            accMenu.Appearance.Item.Hovered.ForeColor = hoverFore;
            accMenu.Appearance.Item.Hovered.Font = itemFont;
            accMenu.Appearance.Item.Hovered.Options.UseBackColor = true;
            accMenu.Appearance.Item.Hovered.Options.UseForeColor = true;
            accMenu.Appearance.Item.Hovered.Options.UseFont = true;

            // Pressed (Seçili)
            accMenu.Appearance.Item.Pressed.BackColor = pressBack;
            accMenu.Appearance.Item.Pressed.ForeColor = pressFore;
            accMenu.Appearance.Item.Pressed.Font = itemFont;
            accMenu.Appearance.Item.Pressed.Options.UseBackColor = true;
            accMenu.Appearance.Item.Pressed.Options.UseForeColor = true;
            accMenu.Appearance.Item.Pressed.Options.UseFont = true;

            // 2. GROUP (Normal, Hover, Pressed)
            // Normal
            accMenu.Appearance.Group.Normal.BackColor = normalBack;
            accMenu.Appearance.Group.Normal.ForeColor = Color.DarkOliveGreen; // Gruplar biraz daha farklı ton
            accMenu.Appearance.Group.Normal.Font = headerFont;
            accMenu.Appearance.Group.Normal.Options.UseBackColor = true;
            accMenu.Appearance.Group.Normal.Options.UseForeColor = true;
            accMenu.Appearance.Group.Normal.Options.UseFont = true;

            // Hover
            accMenu.Appearance.Group.Hovered.BackColor = hoverBack;
            accMenu.Appearance.Group.Hovered.ForeColor = hoverFore;
            accMenu.Appearance.Group.Hovered.Options.UseBackColor = true;
            accMenu.Appearance.Group.Hovered.Options.UseForeColor = true;

            // Pressed (Aktif Grup)
            accMenu.Appearance.Group.Pressed.BackColor = pressBack;
            accMenu.Appearance.Group.Pressed.ForeColor = pressFore;
            accMenu.Appearance.Group.Pressed.Options.UseBackColor = true;
            accMenu.Appearance.Group.Pressed.Options.UseForeColor = true;

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

        private List<DevExpress.XtraBars.Navigation.AccordionControlElement> adminElements = new List<DevExpress.XtraBars.Navigation.AccordionControlElement>();

        private void InitializeAdminMenu()
        {
            string[] headers = {
                "🏥 KLİNİK BİLGİLERİ",
                "👥 PERSONEL BİLGİLERİ",
                "👤 MÜŞTERİ (HAYVAN SAHİBİ) BİLGİLERİ",
                "🐾 HASTA (HAYVAN) BİLGİLERİ",
                "⚕️ TEDAVİ VE SAĞLIK GEÇMİŞİ",
                "📅 RANDEVU BİLGİLERİ",
                "💳 ÖDEME & FATURA BİLGİLERİ"
            };

            int insertIndex = 1; // Anasayfa'dan sonra başla
            foreach (var header in headers)
            {
                var item = new DevExpress.XtraBars.Navigation.AccordionControlElement();
                item.Name = "accAdmin_" + header.Replace(" ", "");
                item.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
                item.Text = header;
                item.Visible = false; // Load'da kontrol edilecek
                
                // Tasarım uyumu: Başlıklar büyük ve kalın
                item.Appearance.Normal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                item.Appearance.Normal.ForeColor = Color.DarkGreen;
                item.Appearance.Normal.Options.UseFont = true;
                item.Appearance.Normal.Options.UseForeColor = true;

                item.Click += (s, e) => { ShowAdminSubPanel(header); };

                adminElements.Add(item);
                accMenu.Elements.Insert(insertIndex++, item);
            }
        }

        private void ShowAdminSubPanel(string header)
        {
            ShowFullScreenImage(false);
            panelHastaEkle.Visible = false;
            panelHastaListele.Visible = false;

            // Varsa eski admin panelini temizle
            var oldPanel = panelContent.Controls["adminPanel"];
            if (oldPanel != null) panelContent.Controls.Remove(oldPanel);

            Panel adminPanel = new Panel();
            adminPanel.Name = "adminPanel";
            adminPanel.Dock = DockStyle.Fill;
            adminPanel.BackColor = Color.Honeydew;

            Label lblTitle = new Label();
            lblTitle.Text = header;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkGreen;
            lblTitle.Location = new Point(50, 40);
            lblTitle.AutoSize = true;
            adminPanel.Controls.Add(lblTitle);

            // Bilgi alanlarını oluştur
            int currentY = 120;
            string[] fields = null;

            if (header.Contains("KLİNİK BİLGİLERİ"))
            {
                // Özel Tasarım Klinik Bilgi Kartı
                BuildClinicInfoUI(adminPanel, header);
                panelContent.Controls.Add(adminPanel);
                adminPanel.BringToFront();
                return; // Standart döngüye girmeden çık
            }

            if (header.Contains("PERSONEL BİLGİLERİ"))
            {
                // Personel Listesi Arayüzü
                BuildPersonelListUI(adminPanel, header);
                panelContent.Controls.Add(adminPanel);
                adminPanel.BringToFront();
                return;
            }
            
            if (header.Contains("MÜŞTERİ"))
            {
                 // Müşteri Listesi Arayüzü
                 BuildMusteriListUI(adminPanel, header);
                 panelContent.Controls.Add(adminPanel);
                 adminPanel.BringToFront();
                 return;
            }
            
            if (header.Contains("HASTA"))
            {
                // Hasta (Hayvan) Listesi Arayüzü - Kart Görünümü
                BuildHastaListUI(adminPanel, header);
                panelContent.Controls.Add(adminPanel);
                adminPanel.BringToFront();
                return;
            }
            
            if (header.Contains("TEDAVİ"))
            {
                 // Tedavi Geçmişi Arayüzü
                 BuildTedaviGecmisiUI(adminPanel, header);
                 panelContent.Controls.Add(adminPanel);
                 adminPanel.BringToFront();
                 return;
            }
            else if (header.Contains("RANDEVU"))
            {
                 // Randevu Bilgileri Arayüzü
                 BuildRandevuBilgileriUI(adminPanel, header);
                 panelContent.Controls.Add(adminPanel);
                 adminPanel.BringToFront();
                 return;
            }

            else if (header.Contains("ÖDEME"))
            {
                // Ödeme Listesi Grid Kontrolü
                DevExpress.XtraGrid.GridControl gridOdeme = new DevExpress.XtraGrid.GridControl();
                DevExpress.XtraGrid.Views.Grid.GridView viewOdeme = new DevExpress.XtraGrid.Views.Grid.GridView();

                // Arkaplan Resmi Ayarla (Diğer menülerle aynı: clinic_info_bg.png)
                try
                {
                     string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                     string bgPath = string.Empty;
                     for (int i = 0; i < 5; i++)
                     {
                         string checkPath = System.IO.Path.Combine(currentDir, "Resources", "clinic_info_bg.png");
                         if (System.IO.File.Exists(checkPath))
                         {
                             bgPath = checkPath;
                             break;
                         }
                         var parent = System.IO.Directory.GetParent(currentDir);
                         if (parent == null) break;
                         currentDir = parent.FullName;
                     }

                     if (!string.IsNullOrEmpty(bgPath))
                     {
                         adminPanel.BackgroundImage = System.Drawing.Image.FromFile(bgPath);
                         adminPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                     }
                }
                catch { }
                
                gridOdeme.MainView = viewOdeme;
                gridOdeme.ViewCollection.Add(viewOdeme);
                
                // Ortalamak için Body Panel
                System.Windows.Forms.Panel pnlBody = new System.Windows.Forms.Panel();
                pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
                pnlBody.BackColor = System.Drawing.Color.Transparent;
                // Başlık payı bırakarak ortalıyoruz
                pnlBody.Padding = new System.Windows.Forms.Padding(100, 100, 100, 50); 
                adminPanel.Controls.Add(pnlBody);
                pnlBody.BringToFront();

                // Kart
                System.Windows.Forms.Panel card = new System.Windows.Forms.Panel();
                card.Dock = System.Windows.Forms.DockStyle.Fill;
                card.BackColor = System.Drawing.Color.FromArgb(240, 255, 255, 255);
                card.Padding = new System.Windows.Forms.Padding(10);
                pnlBody.Controls.Add(card);

                gridOdeme.Dock = System.Windows.Forms.DockStyle.Fill;
                card.Controls.Add(gridOdeme);

                // Veri Tablosu Oluştur
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("OdemeID", typeof(int));
                dt.Columns.Add("Hasta", typeof(string));
                dt.Columns.Add("Sahip", typeof(string));
                dt.Columns.Add("IslemTuru", typeof(string));
                dt.Columns.Add("Tutar", typeof(string));
                dt.Columns.Add("Yontem", typeof(string));
                dt.Columns.Add("Durum", typeof(string));
                dt.Columns.Add("Tarih", typeof(string));

                // Verileri Ekle
                dt.Rows.Add(1, "Pamuk", "Ayşe Yılmaz", "Muayene", "350 ₺", "Nakit", "Ödendi", "10.01.2026");
                dt.Rows.Add(2, "Boncuk", "Sude Demir", "Aşı", "450 ₺", "Kredi Kartı", "Ödendi", "10.01.2026");
                dt.Rows.Add(3, "Leo", "Mehmet Kaya", "Ameliyat", "7.500 ₺", "Havale", "Beklemede", "09.01.2026");
                dt.Rows.Add(4, "Karabaş", "Ali Çetin", "Tedavi", "1.200 ₺", "Kredi Kartı", "Ödendi", "08.01.2026");
                dt.Rows.Add(5, "Maviş", "Elif Arslan", "Muayene", "300 ₺", "Nakit", "İptal", "11.01.2026");

                gridOdeme.DataSource = dt;
                
                // CRITICAL: Columns must be populated before accessing them
                viewOdeme.PopulateColumns();

                // Tasarım Ayarları
                gridOdeme.LookAndFeel.UseDefaultLookAndFeel = false;
                gridOdeme.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;

                viewOdeme.Appearance.HeaderPanel.BackColor = System.Drawing.Color.ForestGreen;
                viewOdeme.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
                viewOdeme.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                viewOdeme.Appearance.HeaderPanel.Options.UseBackColor = true;
                viewOdeme.Appearance.HeaderPanel.Options.UseForeColor = true;
                viewOdeme.Appearance.HeaderPanel.Options.UseFont = true;

                viewOdeme.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 10F);
                viewOdeme.Appearance.Row.Options.UseFont = true;
                
                // Kolon Başlıklarını Düzenle
                viewOdeme.ViewCaption = "SON ÖDEME HAREKETLERİ";
                viewOdeme.OptionsView.ShowViewCaption = true;
                viewOdeme.Appearance.ViewCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
                viewOdeme.Appearance.ViewCaption.ForeColor = System.Drawing.Color.DarkGreen;
                viewOdeme.Appearance.ViewCaption.Options.UseFont = true;
                viewOdeme.Appearance.ViewCaption.Options.UseForeColor = true;

                // Renkli İşaretler (Basit ve Güvenli Yöntem)
                viewOdeme.CustomColumnDisplayText += (s, e) =>
                {
                    if (e.Column.FieldName == "Durum" && e.Value != null)
                    {
                        e.DisplayText = "● " + e.Value.ToString();
                    }
                };

                viewOdeme.RowCellStyle += (s, e) => 
                {
                    if (e.Column.FieldName == "Durum")
                    {
                        // Değer "● Ödendi" şeklinde gelebilir,Contains ile kontrol et
                        string val = e.CellValue?.ToString();
                        if (val == "Ödendi") e.Appearance.ForeColor = System.Drawing.Color.LimeGreen;
                        else if (val == "Beklemede") e.Appearance.ForeColor = System.Drawing.Color.DarkOrange;
                        else if (val == "İptal") e.Appearance.ForeColor = System.Drawing.Color.Red;
                        
                        e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, System.Drawing.FontStyle.Bold);
                    }
                };

                // Kolon ayarlarını yap (Güvenli Erişim)
                if (viewOdeme.Columns["OdemeID"] != null)
                {
                    viewOdeme.Columns["OdemeID"].Caption = "ID";
                    viewOdeme.Columns["IslemTuru"].Caption = "İŞLEM TÜRÜ";
                    viewOdeme.Columns["Tutar"].Caption = "TUTAR";
                    viewOdeme.Columns["Yontem"].Caption = "ÖDEME YÖNTEMİ";
                    viewOdeme.Columns["Durum"].Caption = "DURUM";
                    viewOdeme.Columns["Tarih"].Caption = "TARİH";
                    viewOdeme.BestFitColumns();
                }
            }
            else
            {
                Label lblInfo = new Label();
                lblInfo.Text = "Bu modül (" + header + ") henüz içeriklendirilmemiştir.";
                lblInfo.Font = new Font("Segoe UI", 14F);
                lblInfo.Location = new Point(50, 120);
                lblInfo.AutoSize = true;
                adminPanel.Controls.Add(lblInfo);
            }

            panelContent.Controls.Add(adminPanel);
            adminPanel.BringToFront();
        }

        private void BuildClinicInfoUI(System.Windows.Forms.Panel parentPanel, string title)
        {
            // Arkaplan - Görsel veya Renk
            string bgPath = string.Empty;
            try
            {
                // Resources klasörünü bulmak için yukarı doğru tara
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                for (int i = 0; i < 5; i++)
                {
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "clinic_info_bg.png");
                    if (System.IO.File.Exists(checkPath))
                    {
                        bgPath = checkPath;
                        break;
                    }
                    var parent = System.IO.Directory.GetParent(currentDir);
                    if (parent == null) break;
                    currentDir = parent.FullName;
                }
                
                if (!string.IsNullOrEmpty(bgPath) && System.IO.File.Exists(bgPath))
                {
                    parentPanel.BackgroundImage = Image.FromFile(bgPath);
                    parentPanel.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    parentPanel.BackColor = System.Drawing.Color.FromArgb(245, 250, 248);
                }
            }
            catch 
            {
                parentPanel.BackColor = System.Drawing.Color.FromArgb(245, 250, 248);
            }

            // Sayfa Başlığı (Standart Label - Transparent destekli)
            System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.DarkSlateGray;
            lblTitle.BackColor = System.Drawing.Color.Transparent;
            lblTitle.Location = new Point(50, 40);
            lblTitle.AutoSize = true;
            parentPanel.Controls.Add(lblTitle);

            // Bilgi Kartı (Standart Panel - Yarı saydam arka plan için)
            System.Windows.Forms.Panel card = new System.Windows.Forms.Panel();
            card.Location = new Point(50, 100);
            card.Size = new Size(1000, 550);
            // Glass Effect: Yarı saydam beyaz
            card.BackColor = System.Drawing.Color.FromArgb(240, 255, 255, 255); 
            card.Padding = new System.Windows.Forms.Padding(20);
            parentPanel.Controls.Add(card);

            // Görsel Ekleme (Sağ Taraf)
            try
            {
                // Aynı logic ile diğer resmi de bul
                string imgPath = string.Empty;
                 string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                for (int i = 0; i < 5; i++)
                {
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "su_vet_animals_v2.png");
                    if (System.IO.File.Exists(checkPath))
                    {
                        imgPath = checkPath;
                        break;
                    }
                    var parent = System.IO.Directory.GetParent(currentDir);
                    if (parent == null) break;
                    currentDir = parent.FullName;
                }

                if (!string.IsNullOrEmpty(imgPath) && System.IO.File.Exists(imgPath))
                {
                    System.Windows.Forms.PictureBox pic = new System.Windows.Forms.PictureBox();
                    pic.Image = Image.FromFile(imgPath);
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    pic.Size = new Size(400, 400); 
                    pic.Location = new Point(550, 75); 
                    card.Controls.Add(pic);
                }
            }
            catch { }

            // İçerik Koordinatları
            int x = 40;
            int y = 40;

            // 1. Klinik Adı (DevExpress LabelControl)
            LabelControl lblName = new LabelControl();
            lblName.Text = "SU Hayvan Sağlığı Merkezi";
            lblName.Appearance.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblName.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray;
            lblName.Location = new Point(x, y);
            card.Controls.Add(lblName);

            y += 55;

            // 2. Ruhsat No
            LabelControl lblRuhsat = new LabelControl();
            lblRuhsat.Text = "Ruhsat No: TR-VET-2022-1193";
            lblRuhsat.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            lblRuhsat.Appearance.ForeColor = System.Drawing.Color.Gray;
            lblRuhsat.Location = new Point(x, y);
            card.Controls.Add(lblRuhsat);

            y += 30;

            // Ayırıcı Çizgi (Standart Panel)
            System.Windows.Forms.Panel sep = new System.Windows.Forms.Panel();
            sep.Size = new Size(500, 2);
            sep.BackColor = System.Drawing.Color.LightGray;
            sep.Location = new Point(x, y + 10);
            card.Controls.Add(sep);

            y += 40;

            // Bilgiler
            AddInfoRow(card, "📍", "ADRES", "Cumhuriyet Mah. Atatürk Bulv. No:88\nMerkez / Elazığ", x, ref y);
            AddInfoRow(card, "📞", "TELEFON", "+90 (424) 233 90 21", x, ref y);
            AddInfoRow(card, "📧", "E-POSTA", "iletisim@suvetlife.com", x, ref y);
            AddInfoRow(card, "⏰", "ÇALIŞMA SAATLERİ", "09:00 – 19:00 (Haftanın 6 günü)", x, ref y);

            y += 20;

            // Acil Durum Özel Vurgulu
            LabelControl lblAcilTitle = new LabelControl();
            lblAcilTitle.Text = "🚨 ACİL DURUM HATTI";
            lblAcilTitle.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblAcilTitle.Appearance.ForeColor = System.Drawing.Color.Crimson;
            lblAcilTitle.Location = new Point(x, y);
            card.Controls.Add(lblAcilTitle);

            y += 35;

            LabelControl lblAcilVal = new LabelControl();
            lblAcilVal.Text = "+90 (530) 601 44 02";
            lblAcilVal.Appearance.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblAcilVal.Appearance.ForeColor = System.Drawing.Color.Red;
            lblAcilVal.Location = new Point(x, y);
            card.Controls.Add(lblAcilVal);
        }

        private void AddInfoRow(Control parent, string icon, string title, string value, int x, ref int y)
        {
            // İkon + Başlık
            LabelControl lblHead = new LabelControl();
            lblHead.Text = icon + " " + title;
            lblHead.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHead.Appearance.ForeColor = Color.SeaGreen;
            lblHead.Location = new Point(x, y);
            parent.Controls.Add(lblHead);

            // Değer
            LabelControl lblVal = new LabelControl();
            lblVal.Text = value;
            lblVal.Appearance.Font = new Font("Segoe UI", 13F);
            lblVal.Appearance.ForeColor = Color.FromArgb(64, 64, 64);
            // Başlık genişliği kadar sağa kaydır
            lblVal.Location = new Point(x + 200, y - 5); 
            parent.Controls.Add(lblVal);

            // Yüksekliği ayarla (çok satırlı ise artır)
            y += value.Contains("\n") ? 60 : 45;


        }

        private List<DevExpress.XtraBars.Navigation.AccordionControlElement> customerElements = new List<DevExpress.XtraBars.Navigation.AccordionControlElement>();

        private void InitializeCustomerMenu()
        {
            string[] headers = {
                "👤 KİŞİSEL PROFİL BİLGİLERİ",
                "🐾 HAYVANLARIM",
                "⚕️ SAĞLIK GEÇMİŞİ",
                "📅 RANDEVULARIM",
                "💳 ÖDEME BİLGİLERİM"
            };

            int insertIndex = 1; 
            foreach (var header in headers)
            {
                var item = new DevExpress.XtraBars.Navigation.AccordionControlElement();
                item.Name = "accCustomer_" + header.Replace(" ", "");
                item.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
                item.Text = header;
                item.Visible = false; 
                
                item.Appearance.Normal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                item.Appearance.Normal.ForeColor = Color.DarkGreen;
                item.Appearance.Normal.Options.UseFont = true;
                item.Appearance.Normal.Options.UseForeColor = true;

                item.Click += (s, e) => { ShowCustomerSubPanel(header); };

                customerElements.Add(item);
                accMenu.Elements.Insert(insertIndex++, item);
            }
        }

        private void ShowCustomerSubPanel(string header)
        {
            ShowFullScreenImage(false);
            panelHastaEkle.Visible = false;
            panelHastaListele.Visible = false;

            var oldPanel = panelContent.Controls["adminPanel"];
            if (oldPanel != null) panelContent.Controls.Remove(oldPanel);

            Panel customerPanel = new Panel();
            customerPanel.Name = "adminPanel"; 
            customerPanel.Dock = DockStyle.Fill;
            customerPanel.BackColor = Color.Honeydew;

            Label lblTitle = new Label();
            lblTitle.Text = header;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkGreen;
            lblTitle.Location = new Point(50, 40);
            lblTitle.AutoSize = true;
            customerPanel.Controls.Add(lblTitle);

            int currentY = 120;
            string[] fields = null;
            bool isReadOnly = false;

            if (header.Contains("KİŞİSEL")) fields = new[] { "Ad Soyad:", "Telefon:", "E-posta:", "Adres:", "Şifre Değiştir:" };
            else if (header.Contains("HAYVANLARIM")) fields = new[] { "Hayvan Adı:", "Tür – Cins:", "Yaş:", "Mikroçip:", "Alerji / Kronik Hastalık:" };
            else if (header.Contains("SAĞLIK GEÇMİŞİ")) { fields = new[] { "Aşılar:", "Tedaviler:", "Doktor Notları:" }; isReadOnly = true; }
            else if (header.Contains("RANDEVULARIM")) fields = new[] { "Geçmiş Randevular:", "Aktif Randevular:", "Yeni Randevu Talebi:" };
            else if (header.Contains("ÖDEME BİLGİLERİM")) fields = new[] { "Ödenenler:", "Bekleyenler:", "Fatura Görüntüleme:" };

            if (fields != null)
            {
                foreach (var field in fields)
                {
                    Label lblField = new Label();
                    lblField.Text = field;
                    lblField.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                    lblField.ForeColor = Color.DimGray;
                    lblField.Location = new Point(60, currentY);
                    lblField.Width = 230;
                    customerPanel.Controls.Add(lblField);

                    TextEdit txtValue = new TextEdit();
                    txtValue.Properties.NullText = isReadOnly ? "Sadece Görüntülenebilir..." : "Bilgi girilmemiş...";
                    txtValue.Properties.ReadOnly = isReadOnly;
                    txtValue.Location = new Point(300, currentY - 3);
                    txtValue.Size = new Size(400, 26);
                    txtValue.Properties.Appearance.Font = new Font("Segoe UI", 10F);
                    customerPanel.Controls.Add(txtValue);

                    currentY += 45;
                }

                if (!isReadOnly)
                {
                    SimpleButton btnGuncelle = new SimpleButton();
                    btnGuncelle.Text = "DEĞİŞİKLİKLERİ KAYDET";
                    btnGuncelle.Size = new Size(200, 40);
                    btnGuncelle.Location = new Point(300, currentY + 10);
                    btnGuncelle.Appearance.BackColor = Color.DarkGreen;
                    btnGuncelle.Appearance.ForeColor = Color.White;
                    btnGuncelle.Appearance.Options.UseBackColor = true;
                    btnGuncelle.Appearance.Options.UseForeColor = true;
                    customerPanel.Controls.Add(btnGuncelle);
                }
            }

            panelContent.Controls.Add(customerPanel);
            customerPanel.BringToFront();
        }

        private void ShowAdminPanel()
        {
            // Varsayılan Admin Dashboard
            ShowAdminSubPanel("🛡️ YÖNETİCİ PANELİ");
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
            if (dashboardPanel != null)
            {
                panelContent.Controls.Remove(dashboardPanel);
                dashboardPanel.Dispose();
            }
            
            dashboardPanel = new Panel();
            dashboardPanel.Dock = DockStyle.Fill;
            dashboardPanel.BackColor = Color.Honeydew;
            
            // Modern Başlık
            DevExpress.XtraEditors.LabelControl lblHosgeldin = new DevExpress.XtraEditors.LabelControl();
            lblHosgeldin.Text = (LoginForm.AdminMi ? "🛡️ YÖNETİCİ KONTROL MERKEZİ" : "👋 HOŞGELDİNİZ, " + LoginForm.GirisYapanKullanici.ToUpper());
            lblHosgeldin.Appearance.Font = new Font("Segoe UI Semibold", 26F, FontStyle.Bold);
            lblHosgeldin.Appearance.ForeColor = Color.DarkGreen;
            lblHosgeldin.Location = new Point(50, 40);
            dashboardPanel.Controls.Add(lblHosgeldin);

            DevExpress.XtraEditors.LabelControl lblSubTitle = new DevExpress.XtraEditors.LabelControl();
            lblSubTitle.Text = "Veteriner Kliniği Otomasyon Sistemine Hoşgeldiniz. Bugün yapacak çok işimiz var!";
            lblSubTitle.Appearance.Font = new Font("Segoe UI", 12F);
            lblSubTitle.Appearance.ForeColor = Color.DimGray;
            lblSubTitle.Location = new Point(55, 90);
            dashboardPanel.Controls.Add(lblSubTitle);

            // ========== İSTATİSTİK PANELİ (FlowLayout gibi yan yana) ==========
            int cardWidth = 260;
            int cardHeight = 160;
            int startX = 50;
            int startY = 160;
            int gap = 30;

            // 1. Kart: Toplam Hasta (Veritabanından)
            int toplamHasta = 0;
            using (var db = new VetClinicContext())
            {
                try { toplamHasta = db.Hastalar.Count(); } catch { toplamHasta = HastaListesi.Count; }
            }
            dashboardPanel.Controls.Add(CreateModernCard("🐾 TOPLAM HASTA", toplamHasta.ToString(), Color.SeaGreen, startX, startY, cardWidth, cardHeight));
            
            // 2. Kart: Randevular
            int totalRandevu = FrmRandevu.RandevuListesi.Count;
            dashboardPanel.Controls.Add(CreateModernCard("📅 TÜM RANDEVULAR", totalRandevu.ToString(), Color.SteelBlue, startX + cardWidth + gap, startY, cardWidth, cardHeight));

            // 3. Kart: Bekleyen Randevu
            int bekleyen = FrmRandevu.RandevuListesi.Count(r => r.Durum == "Beklemede");
            dashboardPanel.Controls.Add(CreateModernCard("⏳ BEKLEYENLER", bekleyen.ToString(), Color.DarkOrange, startX + (cardWidth + gap) * 2, startY, cardWidth, cardHeight));

            // 4. Kart: Tamamlanan Randevu
            int tamamlanan = FrmRandevu.RandevuListesi.Count(r => r.Durum == "Tamamlandı");
            dashboardPanel.Controls.Add(CreateModernCard("✅ TAMAMLANAN", tamamlanan.ToString(), Color.MediumPurple, startX + (cardWidth + gap) * 3, startY, cardWidth, cardHeight));

            // Alt Bilgi Bölümü
            DevExpress.XtraEditors.LabelControl lblDateTime = new DevExpress.XtraEditors.LabelControl();
            lblDateTime.Text = "📅 " + DateTime.Now.ToString("dd MMMM yyyy, dddd");
            lblDateTime.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Italic);
            lblDateTime.Appearance.ForeColor = Color.Gray;
            lblDateTime.Location = new Point(55, 360);
            dashboardPanel.Controls.Add(lblDateTime);

            panelContent.Controls.Add(dashboardPanel);
            dashboardPanel.BringToFront();
        }

        private DevExpress.XtraEditors.PanelControl CreateModernCard(string title, string value, Color baseColor, int x, int y, int w, int h)
        {
            DevExpress.XtraEditors.PanelControl card = new DevExpress.XtraEditors.PanelControl();
            card.Size = new Size(w, h);
            card.Location = new Point(x, y);
            card.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            card.BackColor = Color.White;
            card.Appearance.BackColor = Color.White;
            card.Appearance.Options.UseBackColor = true;

            // Kart Üst Şeridi (Renk)
            Panel topBar = new Panel();
            topBar.Dock = DockStyle.Top;
            topBar.Height = 10;
            topBar.BackColor = baseColor;
            card.Controls.Add(topBar);

            DevExpress.XtraEditors.LabelControl lblTitle = new DevExpress.XtraEditors.LabelControl();
            lblTitle.Text = title;
            lblTitle.Appearance.Font = new Font("Segoe UI Bold", 10F, FontStyle.Bold);
            lblTitle.Appearance.ForeColor = Color.Gray;
            lblTitle.Location = new Point(20, 25);
            card.Controls.Add(lblTitle);

            DevExpress.XtraEditors.LabelControl lblValue = new DevExpress.XtraEditors.LabelControl();
            lblValue.Text = value;
            lblValue.Appearance.Font = new Font("Segoe UI", 42F, FontStyle.Bold);
            lblValue.Appearance.ForeColor = baseColor;
            lblValue.Location = new Point(20, 50);
            card.Controls.Add(lblValue);

            return card;
        }

        private void accCikis_Click(object sender, EventArgs e)
        {
            // Mevcut açık olan LoginForm'u bul
            Form login = Application.OpenForms["LoginForm"];
            if (login != null)
            {
                login.Show();
            }
            else
            {
                login = new LoginForm();
                login.Show();
            }
            this.Close();
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
            // Önce verileri yükle (Dashboard istatistikleri için gerekli)
            HastaListesi.Clear();
            var kayitliHastalar = HastaVeriYonetimi.Yukle();
            foreach (var h in kayitliHastalar)
            {
                HastaListesi.Add(h);
            }
            gridControl1.DataSource = HastaListesi;
            var view = gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            if(view != null) 
            {
               view.OptionsBehavior.AutoPopulateColumns = true;
               view.PopulateColumns();
               view.OptionsView.ShowGroupPanel = false;
               view.BestFitColumns();
            }
            // Menü görünürlüklerini ayarla
            foreach (var el in adminElements) el.Visible = LoginForm.AdminMi;
            foreach (var el in customerElements) el.Visible = !LoginForm.AdminMi;

            // Eski müşteri menülerini tamamen gizle (yeni yapı geldi)
            accordionControlElement2.Visible = false;
            accordionControlElementRandevu.Visible = false;

            // Sistem kısmını gizle ve Çıkış butonunu sona taşı
            accordionControlElementSistem.Visible = false;
            if (!accMenu.Elements.Contains(accCikis))
            {
                accMenu.Elements.Add(accCikis); // En sona ekle
                accCikis.Text = "🚪 GÜVENLİ ÇIKIŞ";
                accCikis.Appearance.Normal.ForeColor = Color.DarkRed;
                accCikis.Appearance.Normal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            }

            if (LoginForm.AdminMi)
            {
                // Yönetici ise başlangıçta yönetici panelini aç
                // Yönetici ise başlangıçta ANASAYFA (Dashboard)
                ShowFullScreenImage(true);
            }
            else
            {
                // Müşteri ise başlangıçta tam ekran görsel (Anasayfa modu)
                ShowFullScreenImage(true);
            }


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




        private void BuildPersonelListUI(System.Windows.Forms.Panel parentPanel, string title)
        {
            // Arkaplan - Ortak Resim
            string bgPath = string.Empty;
            try
            {
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                for (int i = 0; i < 5; i++)
                {
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "clinic_info_bg.png");
                    if (System.IO.File.Exists(checkPath))
                    {
                        bgPath = checkPath;
                        break;
                    }
                    var parent = System.IO.Directory.GetParent(currentDir);
                    if (parent == null) break;
                    currentDir = parent.FullName;
                }
                
                if (!string.IsNullOrEmpty(bgPath))
                {
                    parentPanel.BackgroundImage = Image.FromFile(bgPath);
                    parentPanel.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    parentPanel.BackColor = System.Drawing.Color.FromArgb(245, 250, 248);
                }
            }
            catch 
            {
                parentPanel.BackColor = System.Drawing.Color.FromArgb(245, 250, 248);
            }

            // Başlık
            System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.DarkBlue; // Arka plan üzerinde görünür olsun
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Location = new Point(50, 40);
            lblTitle.AutoSize = true;
            parentPanel.Controls.Add(lblTitle);

            // Bilgilendirme Notu
            System.Windows.Forms.Label lblInfo = new System.Windows.Forms.Label();
            lblInfo.Text = "ℹ️ Detayları görüntülemek için personelin üzerine tıklayınız.";
            lblInfo.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblInfo.ForeColor = System.Drawing.Color.DimGray;
            lblInfo.BackColor = Color.Transparent;
            lblInfo.Location = new Point(55, 85);
            lblInfo.AutoSize = true;
            parentPanel.Controls.Add(lblInfo);

            // Grid Control
            DevExpress.XtraGrid.GridControl grid = new DevExpress.XtraGrid.GridControl();
            grid.Location = new Point(50, 110);
            grid.Size = new Size(1000, 500);
            grid.LookAndFeel.UseDefaultLookAndFeel = false;
            grid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            // Grid şeffaf olamaz ama parent'ı olabilir mi? Hayır, standart grid.

            DevExpress.XtraGrid.Views.Grid.GridView view = new DevExpress.XtraGrid.Views.Grid.GridView();
            grid.MainView = view;
            grid.ViewCollection.Add(view);

            // Verileri Hazırla
            List<Personel> personelListesi;
            using (var db = new VetClinicContext())
            {
                 db.EnsureSeeded();
                 personelListesi = db.Personeller.ToList();
            }

            grid.DataSource = personelListesi;
            parentPanel.Controls.Add(grid);

            // Grid Ayarları
            view.OptionsBehavior.Editable = false;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowIndicator = false;
            view.RowHeight = 40;
            
            // Kolon Başlık Stili
            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            view.Appearance.HeaderPanel.BackColor = Color.SeaGreen;
            view.Appearance.HeaderPanel.ForeColor = Color.White;
            view.Appearance.HeaderPanel.Options.UseBackColor = true;
            view.Appearance.HeaderPanel.Options.UseForeColor = true;
            view.Appearance.HeaderPanel.Options.UseFont = true;

            // Satır Stili
            view.Appearance.Row.Font = new Font("Segoe UI", 11F);
            view.Appearance.Row.Options.UseFont = true;

            // Zebra Effect
            view.OptionsView.EnableAppearanceEvenRow = true;
            view.Appearance.EvenRow.BackColor = Color.FromArgb(240, 255, 250); // Hafif yeşil ton
            view.Appearance.EvenRow.Options.UseBackColor = true;

            // Seçim Ayarları (Tıklama için önemli)
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.Appearance.FocusedRow.BackColor = Color.LightSkyBlue;
            view.Appearance.FocusedRow.Options.UseBackColor = true;

            // Tıklama Olayı
            view.RowClick += (s, e) => 
            {
                var row = view.GetRow(e.RowHandle) as Personel;
                if (row != null)
                {
                    ShowPersonelDetay(parentPanel, row);
                }
            };

            // Kolonlar
            // Kolonları Temizle (Emin olmak için)
            view.Columns.Clear();

            // Sadece İstenen Kolonları Manuel Ekle
            DevExpress.XtraGrid.Columns.GridColumn colAdSoyad = view.Columns.AddVisible("AdSoyad", "AD SOYAD");
            colAdSoyad.Width = 200;

            DevExpress.XtraGrid.Columns.GridColumn colGorev = view.Columns.AddVisible("Gorev", "GÖREV");
            colGorev.Width = 150;

            DevExpress.XtraGrid.Columns.GridColumn colUzmanlik = view.Columns.AddVisible("Uzmanlik", "UZMANLIK");
            colUzmanlik.Width = 150;

            DevExpress.XtraGrid.Columns.GridColumn colYetki = view.Columns.AddVisible("Yetki", "YETKİ");
            colYetki.Width = 100;

            // Diğer alanlar otomatik gelmeyecek, sadece yukarıdakiler görünecek.
            
            // Kolon başlıklarını ortaya
            foreach(DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
            {
               col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
        }

        private void ShowPersonelDetay(Control parent, Personel p)
        {
            // Varsa eski detayı kapat
            var old = parent.Controls.Find("pnlDetailOverlay", true).FirstOrDefault();
            if (old != null) parent.Controls.Remove(old);

            // Detay Paneli (Overlay - Dış Çerçeve)
            Panel pnlDetail = new Panel();
            pnlDetail.Name = "pnlDetailOverlay";
            pnlDetail.Size = new Size(500, 600);
            pnlDetail.Location = new Point((parent.Width - pnlDetail.Width) / 2, (parent.Height - pnlDetail.Height) / 2);
            // Sınır Çizgisi
            pnlDetail.BorderStyle = BorderStyle.FixedSingle;
            
            // Arkaplan Resmi Yükleme
            try
            {
                string bgPath = string.Empty;
                 string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                for (int i = 0; i < 5; i++)
                {
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "clinic_info_bg.png");
                    if (System.IO.File.Exists(checkPath))
                    {
                        bgPath = checkPath;
                        break;
                    }
                    var parentDir = System.IO.Directory.GetParent(currentDir);
                    if (parentDir == null) break;
                    currentDir = parentDir.FullName;
                }

                if (!string.IsNullOrEmpty(bgPath))
                {
                    pnlDetail.BackgroundImage = Image.FromFile(bgPath);
                    pnlDetail.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    pnlDetail.BackColor = Color.White;
                }
            }
            catch 
            {
                pnlDetail.BackColor = Color.White;
            }

            // İçerik Paneli (Glass Effect - Yarı Saydam)
            Panel pnlContent = new Panel();
            pnlContent.Location = new Point(20, 20);
            pnlContent.Size = new Size(460, 560);
            // Yarı Saydam Beyaz
            pnlContent.BackColor = Color.FromArgb(235, 255, 255, 255); 
            pnlDetail.Controls.Add(pnlContent);

            // Kapatma butonu - İç panelin sağ üst köşesi
            SimpleButton btnClose = new SimpleButton();
            btnClose.Text = "X";
            btnClose.Size = new Size(40, 40);
            btnClose.Location = new Point(pnlContent.Width - 45, 5);
            btnClose.Appearance.BackColor = Color.Crimson;
            btnClose.Appearance.ForeColor = Color.White;
            btnClose.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnClose.Appearance.Options.UseBackColor = true;
            btnClose.Appearance.Options.UseForeColor = true;
            btnClose.Appearance.Options.UseFont = true;
            btnClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            btnClose.Click += (s, e) => { parent.Controls.Remove(pnlDetail); };
            pnlContent.Controls.Add(btnClose);

            // Başlık Alanı
            Label lblHeader = new Label();
            lblHeader.Text = "🧾 Personel Detay Bilgisi";
            lblHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblHeader.ForeColor = Color.DarkSlateGray;
            lblHeader.Location = new Point(20, 20);
            lblHeader.AutoSize = true;
            pnlContent.Controls.Add(lblHeader);

            Label lblSub = new Label();
            lblSub.Text = "🔹 " + p.AdSoyad;
            lblSub.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSub.ForeColor = Color.SeaGreen;
            lblSub.Location = new Point(20, 60);
            lblSub.AutoSize = true;
            pnlContent.Controls.Add(lblSub);

            // Çizgi
            Panel line = new Panel();
            line.BackColor = Color.LightGray;
            line.Size = new Size(420, 2); // Genişlik ayarlandı
            line.Location = new Point(20, 95);
            pnlContent.Controls.Add(line);

            // Detaylar
            int y = 110;
            // AddDetailRow artık pnlContent (iç panel) üzerinden çalışacak
            AddDetailRow(pnlContent, "Ad Soyad:", p.AdSoyad, ref y);
            AddDetailRow(pnlContent, "Görev:", p.Gorev, ref y);
            AddDetailRow(pnlContent, "Uzmanlık Alanı:", p.Uzmanlik, ref y);
            AddDetailRow(pnlContent, "Telefon:", p.Telefon, ref y);
            AddDetailRow(pnlContent, "E-posta:", p.Eposta, ref y);
            AddDetailRow(pnlContent, "Çalışma Saatleri:", p.CalismaSaatleri, ref y);
            AddDetailRow(pnlContent, "Çalışma Günleri:", p.CalismaGunleri, ref y);
            AddDetailRow(pnlContent, "Sisteme Giriş Yetkisi:", p.Yetki, ref y);
            AddDetailRow(pnlContent, "Durum:", p.Durum, ref y, p.Durum == "Aktif" ? Color.Green : Color.Red);

            parent.Controls.Add(pnlDetail);
            pnlDetail.BringToFront();
        }

        private void AddDetailRow(Control parent, string label, string value, ref int y, Color? valColor = null)
        {
            Label lbl = new Label();
            lbl.Text = label;
            lbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lbl.ForeColor = Color.Gray;
            lbl.Location = new Point(30, y);
            lbl.Width = 150;
            parent.Controls.Add(lbl);

            Label val = new Label();
            val.Text = value;
            val.Font = new Font("Segoe UI", 11F);
            val.ForeColor = valColor ?? Color.Black;
            val.Location = new Point(190, y - 2);
            val.AutoSize = true;
            parent.Controls.Add(val);

            y += 40;
        }

        private void BuildMusteriListUI(System.Windows.Forms.Panel parentPanel, string title)
        {
            // Arkaplan
           string bgPath = string.Empty;
            try
            {
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                for (int i = 0; i < 5; i++)
                {
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "clinic_info_bg.png");
                    if (System.IO.File.Exists(checkPath))
                    {
                        bgPath = checkPath;
                        break;
                    }
                    var parent = System.IO.Directory.GetParent(currentDir);
                    if (parent == null) break;
                    currentDir = parent.FullName;
                }
                
                if (!string.IsNullOrEmpty(bgPath))
                {
                    parentPanel.BackgroundImage = Image.FromFile(bgPath);
                    parentPanel.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    parentPanel.BackColor = System.Drawing.Color.FromArgb(245, 250, 248);
                }
            }
            catch 
            {
                parentPanel.BackColor = System.Drawing.Color.FromArgb(245, 250, 248);
            }

            // Başlık
            System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.DarkBlue; 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Location = new Point(50, 40);
            lblTitle.AutoSize = true;
            parentPanel.Controls.Add(lblTitle);

            // Grid Control
            DevExpress.XtraGrid.GridControl grid = new DevExpress.XtraGrid.GridControl();
            grid.Location = new Point(50, 100);
            grid.Size = new Size(1000, 500);
            grid.LookAndFeel.UseDefaultLookAndFeel = false;
            grid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;

            DevExpress.XtraGrid.Views.Grid.GridView view = new DevExpress.XtraGrid.Views.Grid.GridView();
            grid.MainView = view;
            grid.ViewCollection.Add(view);

            // Verileri Hazırla
            List<Musteri> musteriListesi;
            using (var db = new VetClinicContext())
            {
                 db.EnsureSeeded();
                 musteriListesi = db.Musteriler.ToList();
            }

            grid.DataSource = musteriListesi;
            parentPanel.Controls.Add(grid);

            // Grid Ayarları
            view.OptionsBehavior.Editable = false;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowIndicator = false;
            view.RowHeight = 40;
            
            // Kolon Başlık Stili
            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            view.Appearance.HeaderPanel.BackColor = Color.SeaGreen;
            view.Appearance.HeaderPanel.ForeColor = Color.White;
            view.Appearance.HeaderPanel.Options.UseBackColor = true;
            view.Appearance.HeaderPanel.Options.UseForeColor = true;
            view.Appearance.HeaderPanel.Options.UseFont = true;

            // Satır Stili
            view.Appearance.Row.Font = new Font("Segoe UI", 11F);
            view.Appearance.Row.Options.UseFont = true;

            // Zebra Effect
            view.OptionsView.EnableAppearanceEvenRow = true;
            view.Appearance.EvenRow.BackColor = Color.FromArgb(240, 255, 250);
            view.Appearance.EvenRow.Options.UseBackColor = true;

             // Seçim Ayarları
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.Appearance.FocusedRow.BackColor = Color.LightSkyBlue;
            view.Appearance.FocusedRow.Options.UseBackColor = true;

            // Kolonlar
            view.PopulateColumns();
            if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;

            if (view.Columns["AdSoyad"] != null) { view.Columns["AdSoyad"].Caption = "AD SOYAD"; view.Columns["AdSoyad"].Width = 200; }
            if (view.Columns["Telefon"] != null) { view.Columns["Telefon"].Caption = "TELEFON"; view.Columns["Telefon"].Width = 150; }
            if (view.Columns["Eposta"] != null) { view.Columns["Eposta"].Caption = "E-POSTA"; view.Columns["Eposta"].Width = 250; }
            if (view.Columns["Hayvanlar"] != null) { view.Columns["Hayvanlar"].Caption = "SAHİP OLDUĞU HAYVAN(LAR)"; view.Columns["Hayvanlar"].Width = 300; }
            
            view.BestFitColumns();
        }



        private void BuildHastaListUI(System.Windows.Forms.Panel parentPanel, string title)
        {
             // Arkaplan
           string bgPath = string.Empty;
            try
            {
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                for (int i = 0; i < 5; i++)
                {
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "clinic_info_bg.png");
                    if (System.IO.File.Exists(checkPath))
                    {
                        bgPath = checkPath;
                        break;
                    }
                    var parent = System.IO.Directory.GetParent(currentDir);
                    if (parent == null) break;
                    currentDir = parent.FullName;
                }
                
                if (!string.IsNullOrEmpty(bgPath))
                {
                    parentPanel.BackgroundImage = Image.FromFile(bgPath);
                    parentPanel.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    parentPanel.BackColor = System.Drawing.Color.FromArgb(245, 250, 248);
                }
            }
            catch 
            {
                parentPanel.BackColor = System.Drawing.Color.FromArgb(245, 250, 248);
            }

            // Başlık (Tek Satır, Estetik)
            // Başlık kaldırıldı


            // FlowLayoutPanel (Kartlar için konteyner)
            System.Windows.Forms.FlowLayoutPanel flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            flowPanel.Location = new Point(50, 90);
            flowPanel.Size = new Size(1000, 600);
            flowPanel.AutoScroll = true;
            flowPanel.BackColor = Color.Transparent; // Arkaplanı görsün
            parentPanel.Controls.Add(flowPanel);

            // Verileri Hazırla
            List<HastaDetay> hastaListesi;
            using (var db = new VetClinicContext())
            {
                 db.EnsureSeeded();
                 hastaListesi = db.Hastalar.ToList();
            }

            foreach (var h in hastaListesi)
            {
                Panel card = new Panel();
                card.Size = new Size(220, 320);
                card.BackColor = Color.White;
                card.Margin = new Padding(10);
                // Gölge niyetine basit border SİLİNDİ, yerine Custom Paint
                card.BorderStyle = BorderStyle.None;
                card.Paint += (s, e) => 
                {
                    int thickness = 2;
                    using (Pen p = new Pen(Color.Orange, thickness))
                    {
                        e.Graphics.DrawRectangle(p, new Rectangle(thickness/2, thickness/2, card.Width - thickness, card.Height - thickness));
                    }
                };

                // Resim
                PictureBox pic = new PictureBox();
                pic.Size = new Size(200, 150);
                pic.Location = new Point(10, 10);
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                
                // Cins/Türe göre resim seçimi (ÖZELLEŞTİRİLDİ)
                string imgName = "pet_cat.png"; // Varsayılan

                if (h.Cins.Contains("Pitbull")) imgName = "pet_dog_pitbull.png";
                else if (h.Cins.Contains("Ooodle") || h.Cins.Contains("Poodle")) imgName = "pet_dog_poodle.png";
                else if (h.Cins.Contains("Tekir")) imgName = "pet_cat_tabby.png";
                else if (h.Cins.Contains("British")) imgName = "pet_cat_british.png";
                else if (h.Cins.Contains("Scottish") || h.HayvanAdi == "Luna") imgName = "pet_cat.png"; // Mevcut Scottish
                else if (h.Cins.Contains("Golden") || h.HayvanAdi == "Max") imgName = "pet_dog.png"; // Mevcut Golden
                else if (h.Tur == "Kuş") imgName = "pet_bird.png"; // Mevcut Kuş
                
                // Resim yükleme
                 string imgPath = string.Empty;
                 try
                {
                    string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                    for (int i = 0; i < 5; i++)
                    {
                        string checkPath = System.IO.Path.Combine(currentDir, "Resources", imgName);
                        if (System.IO.File.Exists(checkPath))
                        {
                            imgPath = checkPath;
                            break;
                        }
                        var parent = System.IO.Directory.GetParent(currentDir);
                        if (parent == null) break;
                        currentDir = parent.FullName;
                    }
                    if (!string.IsNullOrEmpty(imgPath)) pic.Image = Image.FromFile(imgPath);
                } catch {}

                card.Controls.Add(pic);

                // İsim
                Label lblName = new Label();
                lblName.Text = h.HayvanAdi;
                lblName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                lblName.ForeColor = Color.Orange;
                lblName.AutoSize = false;
                lblName.TextAlign = ContentAlignment.MiddleCenter;
                lblName.Width = 200;
                lblName.Location = new Point(10, 165);
                card.Controls.Add(lblName);

                // Detaylar (Alt alta)
                int y = 200;
                AddCardLabel(card, "Tür: " + h.Tur, y); y+=20;
                AddCardLabel(card, "Cins: " + h.Cins, y); y+=20;
                AddCardLabel(card, "Yaş: " + h.Yas, y); y+=20;
                AddCardLabel(card, "Cinsiyet: " + h.Cinsiyet, y); y+=20;
                AddCardLabel(card, "Sahibi: " + h.Sahibi, y);

                flowPanel.Controls.Add(card);
            }
            }

        private void BuildTedaviGecmisiUI(System.Windows.Forms.Panel parentPanel, string title)
        {
             // Arkaplan
           string bgPath = string.Empty;
            try
            {
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                for (int i = 0; i < 5; i++)
                {
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "clinic_info_bg.png");
                    if (System.IO.File.Exists(checkPath))
                    {
                        bgPath = checkPath;
                        break;
                    }
                    var parent = System.IO.Directory.GetParent(currentDir);
                    if (parent == null) break;
                    currentDir = parent.FullName;
                }
                
                if (!string.IsNullOrEmpty(bgPath))
                {
                    parentPanel.BackgroundImage = Image.FromFile(bgPath);
                    parentPanel.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    parentPanel.BackColor = System.Drawing.Color.FromArgb(245, 250, 248);
                }
            }
            catch 
            {
                parentPanel.BackColor = System.Drawing.Color.FromArgb(245, 250, 248);
            }

            // Başlık
            // Başlık kaldırıldı

            // Grid Control
            DevExpress.XtraGrid.GridControl grid = new DevExpress.XtraGrid.GridControl();
            grid.Location = new Point(50, 90);
            grid.Size = new Size(1100, 550);
            grid.LookAndFeel.UseDefaultLookAndFeel = false;
            grid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;

            DevExpress.XtraGrid.Views.Grid.GridView view = new DevExpress.XtraGrid.Views.Grid.GridView();
            grid.MainView = view;
            grid.ViewCollection.Add(view);

            // Verileri Hazırla
            List<TedaviKaydi> tedaviListesi;
            using (var db = new VetClinicContext())
            {
                 db.EnsureSeeded();
                 tedaviListesi = db.Tedaviler.ToList();
            }

            grid.DataSource = tedaviListesi;
            parentPanel.Controls.Add(grid);
            
            // Grid UI Ayarları
            view.OptionsBehavior.Editable = false;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowIndicator = false;
            view.RowHeight = 35;
            
            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            view.Appearance.HeaderPanel.BackColor = Color.SeaGreen; // Daha estetik yeşil ton
            view.Appearance.HeaderPanel.ForeColor = Color.White;
            view.Appearance.HeaderPanel.Options.UseBackColor = true;
            view.Appearance.HeaderPanel.Options.UseForeColor = true;

            view.Appearance.Row.Font = new Font("Segoe UI", 10F);
            view.Appearance.EvenRow.BackColor = Color.FromArgb(255, 245, 245); // Hafif kırmızımsı zebra
            view.OptionsView.EnableAppearanceEvenRow = true;

             // Kolon Ayarları
            view.PopulateColumns();
            if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;

            if (view.Columns["Tarih"] != null) { view.Columns["Tarih"].Caption = "TARİH"; view.Columns["Tarih"].Width = 100; }
            if (view.Columns["HastaAdi"] != null) { view.Columns["HastaAdi"].Caption = "HASTA ADI"; view.Columns["HastaAdi"].Width = 100; }
            if (view.Columns["Tur"] != null) { view.Columns["Tur"].Caption = "TÜR"; view.Columns["Tur"].Width = 80; }
            if (view.Columns["Sahip"] != null) { view.Columns["Sahip"].Caption = "HAYVAN SAHİBİ"; view.Columns["Sahip"].Width = 150; }
            if (view.Columns["Sikayet"] != null) { view.Columns["Sikayet"].Caption = "ŞİKAYET / TANI"; view.Columns["Sikayet"].Width = 200; }
            if (view.Columns["Islem"] != null) { view.Columns["Islem"].Caption = "YAPILAN İŞLEM / TEDAVİ"; view.Columns["Islem"].Width = 250; }
            if (view.Columns["Hekim"] != null) { view.Columns["Hekim"].Caption = "İLGİLENEN HEKİM"; view.Columns["Hekim"].Width = 150; }

            view.BestFitColumns();
        }


        private void AddCardLabel(Panel p, string text, int y)
        {
            Label l = new Label();
            l.Text = text;
            l.Font = new Font("Segoe UI", 9F);
            l.ForeColor = Color.Gray;
            l.AutoSize = true;
            // Ortalı olması için basit hesap
            l.Location = new Point(15, y); 
            p.Controls.Add(l);
        }


        private void BuildRandevuBilgileriUI(System.Windows.Forms.Panel parentPanel, string title)
        {
            // 1. Temizlik
            parentPanel.Controls.Clear();
            
            // Arkaplan Logic
            string bgPath = string.Empty;
            try
            {
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                for (int i = 0; i < 5; i++)
                {
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "clinic_info_bg.png");
                    if (System.IO.File.Exists(checkPath))
                    {
                        bgPath = checkPath;
                        break;
                    }
                    var parent = System.IO.Directory.GetParent(currentDir);
                    if (parent == null) break;
                    currentDir = parent.FullName;
                }
                
                if (!string.IsNullOrEmpty(bgPath) && System.IO.File.Exists(bgPath))
                {
                    parentPanel.BackgroundImage = Image.FromFile(bgPath);
                    parentPanel.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    parentPanel.BackColor = System.Drawing.Color.FromArgb(245, 250, 248);
                }
            }
            catch 
            {
                parentPanel.BackColor = System.Drawing.Color.FromArgb(245, 250, 248);
            }

            // 2. Üst Header Paneli (Başlığı Ortalamak için)
            Panel pnlHeader = new Panel();
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 80; // Başlık alanı
            pnlHeader.BackColor = Color.Transparent;
            parentPanel.Controls.Add(pnlHeader);

            Label lblPageTitle = new Label();
            lblPageTitle.Text = "RANDEVU LİSTESİ";
            lblPageTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblPageTitle.ForeColor = Color.DarkOrange;
            lblPageTitle.AutoSize = true;
            // Ortalanması için basit matematik yerine Anchor/Dock hilesi veya Resize eventi gerekir
            // Ancak burada statik olarak ortalı görünmesi için tahmini location veya flow kullanabiliriz.
            // En temizi: Label'ı tam ortalamak zordur, sol üstte bırakalım veya manuel ortalayalım.
            // Kullanıcı "BEYAZ PANEL ORTALANSIN" dedi, başlık da ortalı şık durur.
            lblPageTitle.Location = new Point((parentPanel.Width - 300) / 2, 20); 
            lblPageTitle.Anchor = AnchorStyles.Top; // Resize'da yukarıda kalsın
            pnlHeader.Controls.Add(lblPageTitle);

            // 3. İçerik Taşıyıcı (Padding ile Ortalamayı Sağlar)
            Panel pnlBody = new Panel();
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.BackColor = Color.Transparent;
            // KENARLARDAN BOŞLUK BIRAKARAK ORTALIYORUZ
            pnlBody.Padding = new Padding(150, 10, 150, 80); 
            parentPanel.Controls.Add(pnlBody);
            pnlBody.BringToFront(); // Header'ın altında kalmasın gerçi Dock Fill, Header Top

            // 4. Beyaz Kart (Grid'in Zemin Paneli)
            Panel card = new Panel();
            card.Dock = DockStyle.Fill; // Padding sınırlarına yayılır
            card.BackColor = Color.White; 
            // Hafif gölgelik efekti WinForms'da zordur ama Panel border ile belirginleştirebiliriz
            // card.BorderStyle = BorderStyle.FixedSingle; // İsteğe bağlı
            pnlBody.Controls.Add(card);

            // Grid
            DevExpress.XtraGrid.GridControl grid = new DevExpress.XtraGrid.GridControl();
            grid.Dock = DockStyle.Fill;
            grid.LookAndFeel.UseDefaultLookAndFeel = false;
            grid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;

            DevExpress.XtraGrid.Views.Grid.GridView view = new DevExpress.XtraGrid.Views.Grid.GridView();
            grid.MainView = view;
            grid.ViewCollection.Add(view);
            grid.DataSource = FrmRandevu.RandevuListesi;
            
            // Grid Stil
            view.Appearance.HeaderPanel.BackColor = Color.DarkOrange;
            view.Appearance.HeaderPanel.ForeColor = Color.White;
            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 11F, FontStyle.Bold); // Fontu biraz büyüttük
            view.Appearance.HeaderPanel.Options.UseBackColor = true;
            view.Appearance.HeaderPanel.Options.UseForeColor = true;
            view.Appearance.HeaderPanel.Options.UseFont = true;
            
            view.Appearance.Row.Font = new Font("Segoe UI", 10F);
            view.Appearance.Row.Options.UseFont = true;
            view.RowHeight = 40; // Satırlar biraz daha ferah
            
            view.OptionsBehavior.Editable = false;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowIndicator = false;
            view.OptionsView.EnableAppearanceEvenRow = true;
            view.Appearance.EvenRow.BackColor = Color.FromArgb(255, 250, 245);
            view.Appearance.EvenRow.Options.UseBackColor = true;

            // Kolonlar
            view.PopulateColumns();
            if (view.Columns["Id"] != null) { view.Columns["Id"].Caption = "ID"; view.Columns["Id"].Width = 50; }
            if (view.Columns["Tur"] != null) { view.Columns["Tur"].Caption = "TÜR"; }
            if (view.Columns["HastaAd"] != null) { view.Columns["HastaAd"].Caption = "HASTA ADI"; }
            if (view.Columns["HastaSoyad"] != null) { view.Columns["HastaSoyad"].Caption = "HASTA SOYADI"; }
            if (view.Columns["RandevuTarihi"] != null) 
            { 
                view.Columns["RandevuTarihi"].Caption = "TARİH"; 
                view.Columns["RandevuTarihi"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["RandevuTarihi"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            }
            if (view.Columns["Aciklama"] != null) { view.Columns["Aciklama"].Caption = "AÇIKLAMA"; }
            if (view.Columns["Hekim"] != null) { view.Columns["Hekim"].Caption = "VETERİNER"; }
            if (view.Columns["Durum"] != null) { view.Columns["Durum"].Caption = "DURUM"; }

            // Renkli İşaretler (Durum Kolonu)
            view.CustomColumnDisplayText += (s, e) =>
            {
                if (e.Column.FieldName == "Durum" && e.Value != null)
                {
                    e.DisplayText = "● " + e.Value.ToString();
                }
            };

            view.RowCellStyle += (s, e) => 
            {
                if (e.Column.FieldName == "Durum")
                {
                    string val = e.CellValue?.ToString();
                    if (val == "Onaylandı") e.Appearance.ForeColor = System.Drawing.Color.LimeGreen;
                    else if (val == "Tamamlandı") e.Appearance.ForeColor = System.Drawing.Color.DodgerBlue;
                    else if (val == "Beklemede") e.Appearance.ForeColor = System.Drawing.Color.DarkOrange;
                    else if (val == "İptal Edildi") e.Appearance.ForeColor = System.Drawing.Color.Red;
                    
                    e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, System.Drawing.FontStyle.Bold);
                }
            };

            view.BestFitColumns();
            card.Controls.Add(grid);
        }

    }


}
