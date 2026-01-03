using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VetClinic.UI1.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using DevExpress.XtraBars.Docking2010;

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
            this.Shown += MainForm_Shown;
            this.FormClosing += MainForm_FormClosing;
            
            // Pati (🐾) Kapatma Butonu - Sidebar Altında
            this.ControlBox = false; // Standart butonları gizle
            
            // 1. Sağ Üst Köşe Pati Butonu
            var btnPatiCloseTop = new DevExpress.XtraEditors.SimpleButton();
            btnPatiCloseTop.Text = "🐾";
            btnPatiCloseTop.Size = new Size(50, 40);
            btnPatiCloseTop.Location = new Point(this.ClientSize.Width - 50, 0); 
            btnPatiCloseTop.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnPatiCloseTop.Appearance.Font = new Font("Segoe UI", 22F, FontStyle.Bold); 
            btnPatiCloseTop.Appearance.ForeColor = Color.FromArgb(230, 80, 0); 
            btnPatiCloseTop.Appearance.BackColor = Color.Transparent;
            btnPatiCloseTop.Appearance.Options.UseFont = true;
            btnPatiCloseTop.Appearance.Options.UseForeColor = true;
            btnPatiCloseTop.Appearance.Options.UseBackColor = true;
            btnPatiCloseTop.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            btnPatiCloseTop.Cursor = Cursors.Hand;
            btnPatiCloseTop.Click += (s, e) => { Application.Exit(); };
            this.Controls.Add(btnPatiCloseTop);
            btnPatiCloseTop.BringToFront();

            // 2. Sidebar ve Alt Pati Butonu
            Panel pnlSidebar = new Panel();
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Width = accMenu.Width;
            pnlSidebar.BackColor = Color.FromArgb(235, 248, 255); // Yeni menu rengiyle uyumlu
            this.Controls.Add(pnlSidebar);
            
            accMenu.Parent = pnlSidebar;
            accMenu.Dock = DockStyle.Fill;
            
            var btnPatiCloseMenu = new DevExpress.XtraEditors.SimpleButton();
            btnPatiCloseMenu.Text = "🐾";
            btnPatiCloseMenu.Size = new Size(pnlSidebar.Width, 60);
            btnPatiCloseMenu.Dock = DockStyle.Bottom;
            btnPatiCloseMenu.Appearance.Font = new Font("Segoe UI", 28F, FontStyle.Bold); 
            btnPatiCloseMenu.Appearance.ForeColor = Color.FromArgb(230, 80, 0); 
            btnPatiCloseMenu.Appearance.BackColor = Color.FromArgb(235, 248, 255); 
            btnPatiCloseMenu.Appearance.Options.UseFont = true;
            btnPatiCloseMenu.Appearance.Options.UseForeColor = true;
            btnPatiCloseMenu.Appearance.Options.UseBackColor = true;
            btnPatiCloseMenu.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            btnPatiCloseMenu.Cursor = Cursors.Hand;
            btnPatiCloseMenu.Click += (s, e) => { Application.Exit(); };
            
            pnlSidebar.Controls.Add(btnPatiCloseMenu);
            btnPatiCloseMenu.BringToFront();
            
            // Menüleri Oluştur
            InitializeAdminMenu();
            InitializeCustomerMenu();

            // Eski text bazlı kapatma butonu SİLİNDİ
            
            panelHastaEkle.Visible = false;
            panelHastaListele.Visible = false;

            // DevExpress temasını devre dışı bırak, kendi renklerimi kullan
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LookAndFeel.UseWindowsXPTheme = false;
            
            // AccordionControl (Menü) - Estetik ve Renk Ayarları
            accMenu.LookAndFeel.UseDefaultLookAndFeel = false;
            accMenu.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat; 
            accMenu.BackColor = Color.White;
            accMenu.Appearance.AccordionControl.BackColor = Color.FromArgb(235, 248, 255); // Çok açık mavi
            accMenu.Appearance.AccordionControl.Options.UseBackColor = true;
            
            // Font Ayarları
            Font headerFont = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            Font itemFont = new Font("Segoe UI", 11F, FontStyle.Bold);

            // RENK PALETİ - ARKA PLANLA UYUMLU
            Color themeOrange = Color.FromArgb(233, 163, 116); // #E9A374
            Color normalBack = Color.FromArgb(235, 248, 255);
            Color normalFore = themeOrange; 
            Color hoverBack = Color.FromArgb(210, 235, 255);
            Color hoverFore = Color.FromArgb(180, 110, 70);   // Biraz daha koyu turuncu hover aktifliği için
            Color pressBack = Color.FromArgb(70, 130, 180);  // Steel Blue (Seçili)
            Color pressFore = Color.White;

            // 1. ITEM (Normal, Hover, Pressed)
            accMenu.Appearance.Item.Normal.BackColor = normalBack;
            accMenu.Appearance.Item.Normal.ForeColor = normalFore;
            accMenu.Appearance.Item.Normal.Font = itemFont;
            accMenu.Appearance.Item.Normal.Options.UseBackColor = true;
            accMenu.Appearance.Item.Normal.Options.UseForeColor = true;
            accMenu.Appearance.Item.Normal.Options.UseFont = true;

            accMenu.Appearance.Item.Hovered.BackColor = hoverBack;
            accMenu.Appearance.Item.Hovered.ForeColor = hoverFore;
            accMenu.Appearance.Item.Hovered.Font = itemFont;
            accMenu.Appearance.Item.Hovered.Options.UseBackColor = true;
            accMenu.Appearance.Item.Hovered.Options.UseForeColor = true;
            accMenu.Appearance.Item.Hovered.Options.UseFont = true;

            accMenu.Appearance.Item.Pressed.BackColor = pressBack;
            accMenu.Appearance.Item.Pressed.ForeColor = pressFore;
            accMenu.Appearance.Item.Pressed.Font = itemFont;
            accMenu.Appearance.Item.Pressed.Options.UseBackColor = true;
            accMenu.Appearance.Item.Pressed.Options.UseForeColor = true;
            accMenu.Appearance.Item.Pressed.Options.UseFont = true;

            // 2. GROUP (Normal, Hover, Pressed)
            accMenu.Appearance.Group.Normal.BackColor = normalBack;
            accMenu.Appearance.Group.Normal.ForeColor = themeOrange;
            accMenu.Appearance.Group.Normal.Font = headerFont;
            accMenu.Appearance.Group.Normal.Options.UseBackColor = true;
            accMenu.Appearance.Group.Normal.Options.UseForeColor = true;
            accMenu.Appearance.Group.Normal.Options.UseFont = true;

            accMenu.Appearance.Group.Hovered.BackColor = hoverBack;
            accMenu.Appearance.Group.Hovered.ForeColor = hoverFore;
            accMenu.Appearance.Group.Hovered.Options.UseBackColor = true;
            accMenu.Appearance.Group.Hovered.Options.UseForeColor = true;

            // TÜM MEVCUT ELEMANLARA (Designer'dan gelenler dahil) UYGULA
            foreach (DevExpress.XtraBars.Navigation.AccordionControlElement el in accMenu.Elements)
            {
                ApplyThemeToElement(el, themeOrange);
            }

            accMenu.Appearance.Group.Pressed.BackColor = pressBack;
            accMenu.Appearance.Group.Pressed.ForeColor = pressFore;
            accMenu.Appearance.Group.Pressed.Options.UseBackColor = true;
            accMenu.Appearance.Group.Pressed.Options.UseForeColor = true;

            // Ana içerik paneline güzel bir arka plan rengi ver
            panelContent.BackColor = Color.White;

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
                "💳 ÖDEME & FATURA BİLGİLERİ",
                "❓ SORU & CEVAP"
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
                item.Appearance.Normal.ForeColor = Color.FromArgb(233, 163, 116);
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
            adminPanel.BackColor = Color.Transparent; // Şeffaf yaparak yeni arka planın önünü açtık

            // Bilgi alanlarını oluştur
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
            else if (header.Contains("SORU & CEVAP"))
            {
                 BuildQandAUI(adminPanel, header, true);
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
                         string checkPath = System.IO.Path.Combine(currentDir, "Resources", "su_vet_admin_global_bg.png");
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
                // Başlık payı bırakarak ortalıyoruz (YUKARI TAŞINDI - KIRPILDI)
                pnlBody.Padding = new System.Windows.Forms.Padding(100, 40, 100, 150); 
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

                // Verileri Ekle (Statik Veri)
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

                // TEMA RENGİ (#FF6B35)
                Color themeOrange = Color.FromArgb(255, 107, 53);
                Color titleColor = Color.FromArgb(233, 163, 116);

                viewOdeme.Appearance.HeaderPanel.BackColor = themeOrange;
                viewOdeme.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.White;
                viewOdeme.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                viewOdeme.Appearance.HeaderPanel.Options.UseBackColor = true;
                viewOdeme.Appearance.HeaderPanel.Options.UseForeColor = true;
                viewOdeme.Appearance.HeaderPanel.Options.UseFont = true;

                viewOdeme.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 10F);
                viewOdeme.Appearance.Row.Options.UseFont = true;

                // Zebra
                viewOdeme.OptionsView.EnableAppearanceEvenRow = true;
                viewOdeme.Appearance.EvenRow.BackColor = Color.FromArgb(255, 248, 245);
                viewOdeme.Appearance.EvenRow.Options.UseBackColor = true;
                
                // Kolon Başlıklarını Düzenle
                viewOdeme.ViewCaption = "SON ÖDEME HAREKETLERİ";
                viewOdeme.OptionsView.ShowViewCaption = true;
                viewOdeme.Appearance.ViewCaption.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
                viewOdeme.Appearance.ViewCaption.ForeColor = titleColor;
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
            // Arkaplan - Sadece boşsa ayarla
            if (parentPanel.BackgroundImage == null)
            {
                try
                {
                    string bgPath = string.Empty;
                    string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                    for (int i = 0; i < 5; i++)
                    {
                        string checkPath = System.IO.Path.Combine(currentDir, "Resources", "su_vet_admin_global_bg.png");
                        if (System.IO.File.Exists(checkPath)) { bgPath = checkPath; break; }
                        var parent = System.IO.Directory.GetParent(currentDir);
                        if (parent == null) break;
                        currentDir = parent.FullName;
                    }
                    if (!string.IsNullOrEmpty(bgPath))
                    {
                        parentPanel.BackgroundImage = Image.FromFile(bgPath);
                        parentPanel.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                catch { parentPanel.BackColor = Color.FromArgb(245, 250, 248); }
            }

            // Bilgi Kartı (Glass Effect)
            System.Windows.Forms.Panel card = new System.Windows.Forms.Panel();
            card.Size = new Size(1000, 520);
            card.BackColor = System.Drawing.Color.FromArgb(245, 255, 255, 255); 
            card.Padding = new System.Windows.Forms.Padding(30);
            
            // Ortalama Fonksiyonu
            Action centerCard = () => {
                if (parentPanel.Width > 0 && parentPanel.Height > 0)
                {
                    card.Location = new Point(
                        (parentPanel.Width - card.Width) / 2,
                        (parentPanel.Height - card.Height) / 2
                    );
                }
            };

            parentPanel.Controls.Add(card);
            centerCard();
            parentPanel.SizeChanged += (s, e) => centerCard();

            // Görsel Ekleme (Sağ Taraf) - Kartın içine alıyoruz ki beraber hareket etsinler
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

        private void BuildQandAUI(System.Windows.Forms.Panel parentPanel, string title, bool isAdmin = false)
        {
            // Arkaplan
            if (parentPanel.BackgroundImage == null)
            {
                try
                {
                    string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                    string resPath = string.Empty;
                    for (int i = 0; i < 5; i++)
                    {
                        string checkPath = System.IO.Path.Combine(currentDir, "Resources", "su_vet_admin_global_bg.png");
                        if (System.IO.File.Exists(checkPath)) { resPath = checkPath; break; }
                        currentDir = System.IO.Directory.GetParent(currentDir)?.FullName;
                        if (currentDir == null) break;
                    }
                    if (!string.IsNullOrEmpty(resPath))
                    {
                        parentPanel.BackgroundImage = Image.FromFile(resPath);
                        parentPanel.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
                catch { }
            }

            // Glass/White Panel
            System.Windows.Forms.Panel card = new System.Windows.Forms.Panel();
            card.Size = new Size(1100, 650); 
            card.BackColor = Color.FromArgb(245, 255, 255, 255);
            card.Padding = new Padding(40);
            
            Action center = () => {
                if (parentPanel.Width > 0 && parentPanel.Height > 0)
                    card.Location = new Point((parentPanel.Width - card.Width) / 2, (parentPanel.Height - card.Height) / 2);
            };
            parentPanel.Controls.Add(card);
            center();
            parentPanel.SizeChanged += (s, e) => center();

            // Başlık
            LabelControl lblTitle = new LabelControl();
            lblTitle.Text = isAdmin ? "💬 GELEN SORULAR VE CEVAPLAMA" : "❓ SIKÇA SORULAN SORULAR & HEKİME DANIŞ";
            lblTitle.Appearance.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            lblTitle.Appearance.ForeColor = Color.DarkOrange;
            lblTitle.Location = new Point(40, 20);
            card.Controls.Add(lblTitle);

            if (!isAdmin)
            {
                // --- MÜŞTERİ GÖRÜNÜMÜ ---
                // 1. SOL TARAF: SSS Listesi (Kaydırılabilir)
                XtraScrollableControl scroll = new XtraScrollableControl();
                scroll.Location = new Point(40, 80);
                scroll.Size = new Size(500, 530); 
                scroll.Appearance.BackColor = Color.Transparent;
                card.Controls.Add(scroll);

                int currentY = 10;
                
                // Sabit SSS Başlığı
                LabelControl lblFAQHeader = new LabelControl();
                lblFAQHeader.Text = "📖 Sıkça Sorulan Sorular";
                lblFAQHeader.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                lblFAQHeader.Appearance.ForeColor = Color.DarkSlateBlue;
                lblFAQHeader.Location = new Point(5, currentY);
                scroll.Controls.Add(lblFAQHeader);
                currentY += 35;

                string[,] qa = {
                    { "Kliniğiniz hangi saatler arasında hizmet veriyor?", "Hafta içi ve Cumartesi günleri 09:00 - 19:00 saatleri arasındayız." },
                    { "Acil bir durumda ne yapmalıyım?", "7/24 aktif olan +90 (530) 601 44 02 acil hattımızdan ulaşın." },
                    { "Aşı takibi yapıyor musunuz?", "Evet, dijital sistemimizle otomatik hatırlatma yapıyoruz." }
                };

                for (int i = 0; i < qa.GetLength(0); i++)
                {
                    LabelControl q = new LabelControl();
                    q.Text = "● " + qa[i, 0];
                    q.Appearance.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                    q.Appearance.ForeColor = Color.FromArgb(70, 70, 70);
                    q.Location = new Point(5, currentY);
                    q.AutoSizeMode = LabelAutoSizeMode.Vertical;
                    q.Width = 460;
                    scroll.Controls.Add(q);
                    currentY += 30;

                    LabelControl a = new LabelControl();
                    a.Text = qa[i, 1];
                    a.Appearance.Font = new Font("Segoe UI", 10F);
                    a.Appearance.ForeColor = Color.Gray;
                    a.Location = new Point(25, currentY);
                    a.AutoSizeMode = LabelAutoSizeMode.Vertical;
                    a.Width = 440;
                    scroll.Controls.Add(a);
                    currentY += 45;
                }

                currentY += 20;
                // Ayırıcı Çizgi
                Panel line = new Panel();
                line.Size = new Size(460, 1);
                line.BackColor = Color.LightGray;
                line.Location = new Point(5, currentY);
                scroll.Controls.Add(line);
                currentY += 20;

                // "Sorularım ve Cevaplar" Başlığı
                LabelControl lblMyQuestions = new LabelControl();
                lblMyQuestions.Text = "✉️ Sorularım ve Yanıtlar";
                lblMyQuestions.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                lblMyQuestions.Appearance.ForeColor = Color.DarkOrange;
                lblMyQuestions.Location = new Point(5, currentY);
                scroll.Controls.Add(lblMyQuestions);
                currentY += 40;

                using (var db = new VetClinicContext())
                {
                    var myQuestions = db.Sorular
                        .Where(s => s.MusteriEmail.ToLower() == LoginForm.GirisYapanKullanici.ToLower().Trim())
                        .OrderByDescending(s => s.Tarih)
                        .ToList();

                    if (myQuestions.Count == 0)
                    {
                        LabelControl lblNone = new LabelControl();
                        lblNone.Text = "Henüz bir soru sormadınız.";
                        lblNone.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
                        lblNone.Appearance.ForeColor = Color.Gray;
                        lblNone.Location = new Point(25, currentY);
                        scroll.Controls.Add(lblNone);
                        currentY += 30;
                    }

                    foreach (var s in myQuestions)
                    {
                        // Soru
                        LabelControl q = new LabelControl();
                        q.Text = "❓ Sorunuz: " + s.Soru;
                        q.Appearance.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
                        q.Appearance.ForeColor = Color.FromArgb(50, 50, 50);
                        q.Location = new Point(5, currentY);
                        q.AutoSizeMode = LabelAutoSizeMode.Vertical;
                        q.Width = 460;
                        scroll.Controls.Add(q);
                        currentY += (int)q.Height + 5;

                        // Tarih ve Hekim Bilgisi
                        LabelControl info = new LabelControl();
                        info.Text = $"{s.Tarih.ToString("dd.MM.yyyy HH:mm")} - Hekim: {s.HekimAd}";
                        info.Appearance.Font = new Font("Segoe UI", 8F);
                        info.Appearance.ForeColor = Color.DimGray;
                        info.Location = new Point(25, currentY);
                        scroll.Controls.Add(info);
                        currentY += 20;

                        // Cevap
                        LabelControl a = new LabelControl();
                        if (s.Cevaplandi)
                        {
                            a.Text = "✅ CEVAP: " + s.Cevap;
                            a.Appearance.ForeColor = Color.SeaGreen;
                            a.Appearance.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
                        }
                        else
                        {
                            a.Text = "⏳ Bu soru henüz hocalalarımız tarafından yanıtlanmadı.";
                            a.Appearance.ForeColor = Color.OrangeRed;
                            a.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
                        }
                        a.Location = new Point(25, currentY);
                        a.AutoSizeMode = LabelAutoSizeMode.Vertical;
                        a.Width = 440;
                        scroll.Controls.Add(a);
                        currentY += (int)a.Height + 35;
                    }
                }

                // 2. SAĞ TARAF: Hekime Soru Sor
                Panel pnlAsk = new Panel();
                pnlAsk.Location = new Point(570, 80);
                pnlAsk.Size = new Size(480, 530);
                pnlAsk.BackColor = Color.FromArgb(20, 233, 163, 116);
                card.Controls.Add(pnlAsk);

                LabelControl lblAskTitle = new LabelControl();
                lblAskTitle.Text = "👨‍⚕️ Uzmana Soru Sor";
                lblAskTitle.Appearance.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
                lblAskTitle.Appearance.ForeColor = Color.DarkSlateGray;
                lblAskTitle.Location = new Point(20, 20);
                pnlAsk.Controls.Add(lblAskTitle);

                LabelControl lblHekim = new LabelControl();
                lblHekim.Text = "Soru Sormak İstediğiniz Hekim:";
                lblHekim.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                lblHekim.Location = new Point(20, 70);
                pnlAsk.Controls.Add(lblHekim);

                ComboBoxEdit cmbAskHekim = new ComboBoxEdit();
                cmbAskHekim.Location = new Point(20, 95);
                cmbAskHekim.Size = new Size(440, 30);
                cmbAskHekim.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                using (var db = new VetClinicContext())
                {
                    var hekimler = db.Personeller
                        .Where(p => p.Gorev.Contains("Hekim") || p.Yetki == "Veteriner")
                        .Select(p => p.AdSoyad).ToList();
                    cmbAskHekim.Properties.Items.AddRange(hekimler);
                }
                pnlAsk.Controls.Add(cmbAskHekim);

                LabelControl lblMsg = new LabelControl();
                lblMsg.Text = "Sorunuz:";
                lblMsg.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                lblMsg.Location = new Point(20, 140);
                pnlAsk.Controls.Add(lblMsg);

                MemoEdit txtAskMsg = new MemoEdit();
                txtAskMsg.Location = new Point(20, 165);
                txtAskMsg.Size = new Size(440, 250);
                txtAskMsg.Properties.NullValuePrompt = "Hekimimize sormak istediğiniz soruyu buraya detaylıca yazabilirsiniz...";
                pnlAsk.Controls.Add(txtAskMsg);

                SimpleButton btnSendAsk = new SimpleButton();
                btnSendAsk.Text = "SORUYU GÖNDER";
                btnSendAsk.Size = new Size(440, 50);
                btnSendAsk.Location = new Point(20, 440);
                btnSendAsk.Appearance.BackColor = Color.DarkOrange;
                btnSendAsk.Appearance.ForeColor = Color.White;
                btnSendAsk.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                btnSendAsk.Appearance.Options.UseBackColor = true;
                btnSendAsk.Appearance.Options.UseForeColor = true;
                btnSendAsk.Appearance.Options.UseFont = true;
                btnSendAsk.Click += (s, e) => {
                    if (string.IsNullOrEmpty(cmbAskHekim.Text)) {
                        MessageBox.Show("Lütfen bir hekim seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                    }
                    if (string.IsNullOrEmpty(txtAskMsg.Text.Trim())) {
                        MessageBox.Show("Lütfen sorunuzu yazınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                    }
                    
                    try {
                        using (var db = new VetClinicContext()) {
                            db.Sorular.Add(new DanismaSorusu {
                                MusteriEmail = LoginForm.GirisYapanKullanici,
                                HekimAd = cmbAskHekim.Text,
                                Soru = txtAskMsg.Text.Trim(),
                                Tarih = DateTime.Now,
                                Cevaplandi = false
                            });
                            db.SaveChanges();
                        }
                        MessageBox.Show($"Sorunuz Sayın {cmbAskHekim.Text} hocamıza iletildi. En kısa sürede yanıtlanacaktır.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAskMsg.Text = "";
                    } catch { MessageBox.Show("Soru gönderilirken hata oluştu."); }
                };
                pnlAsk.Controls.Add(btnSendAsk);
            }
            else
            {
                // --- ADMIN GÖRÜNÜMÜ ---
                // 1. SOL TARAF: Gelen Sorular Listesi (Grid)
                DevExpress.XtraGrid.GridControl gridSorular = new DevExpress.XtraGrid.GridControl();
                DevExpress.XtraGrid.Views.Grid.GridView viewSorular = new DevExpress.XtraGrid.Views.Grid.GridView();
                gridSorular.MainView = viewSorular;
                gridSorular.Location = new Point(40, 80);
                gridSorular.Size = new Size(500, 530);
                card.Controls.Add(gridSorular);

                // 2. SAĞ TARAF: Cevaplama Paneli
                Panel pnlReply = new Panel();
                pnlReply.Location = new Point(570, 80);
                pnlReply.Size = new Size(480, 530);
                pnlReply.BackColor = Color.FromArgb(20, 70, 130, 180); // Hafif Mavi Arkaplan
                card.Controls.Add(pnlReply);

                LabelControl lblReplyTitle = new LabelControl();
                lblReplyTitle.Text = "✍️ Soruyu Cevapla";
                lblReplyTitle.Appearance.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
                lblReplyTitle.Appearance.ForeColor = Color.DarkSlateGray;
                lblReplyTitle.Location = new Point(20, 20);
                pnlReply.Controls.Add(lblReplyTitle);

                MemoEdit txtQuestionDetail = new MemoEdit();
                txtQuestionDetail.Location = new Point(20, 70);
                txtQuestionDetail.Size = new Size(440, 150);
                txtQuestionDetail.Properties.ReadOnly = true;
                txtQuestionDetail.Properties.NullValuePrompt = "Lütfen soldan bir soru seçiniz...";
                pnlReply.Controls.Add(txtQuestionDetail);

                MemoEdit txtReplyMsg = new MemoEdit();
                txtReplyMsg.Location = new Point(20, 230);
                txtReplyMsg.Size = new Size(440, 185);
                txtReplyMsg.Properties.NullValuePrompt = "Cevabınızı buraya yazınız...";
                pnlReply.Controls.Add(txtReplyMsg);

                SimpleButton btnSendReply = new SimpleButton();
                btnSendReply.Text = "CEVABI GÖNDER";
                btnSendReply.Size = new Size(440, 50);
                btnSendReply.Location = new Point(20, 440);
                btnSendReply.Appearance.BackColor = Color.ForestGreen;
                btnSendReply.Appearance.ForeColor = Color.White;
                btnSendReply.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                btnSendReply.Appearance.Options.UseBackColor = true;
                btnSendReply.Appearance.Options.UseForeColor = true;
                btnSendReply.Appearance.Options.UseFont = true;
                pnlReply.Controls.Add(btnSendReply);

                // Veri Yükleme
                Action loadSorular = () => {
                   using (var db = new VetClinicContext()) {
                       var data = db.Sorular.OrderByDescending(s => s.Tarih).ToList();
                       gridSorular.DataSource = data;
                       viewSorular.PopulateColumns();
                       if (viewSorular.Columns["Id"] != null) {
                           viewSorular.Columns["Id"].Visible = false;
                           viewSorular.Columns["MusteriEmail"].Caption = "Müşteri";
                           viewSorular.Columns["HekimAd"].Caption = "Hekim";
                           viewSorular.Columns["Cevaplandi"].Caption = "D.";
                           viewSorular.Columns["Cevaplandi"].Width = 30;
                       }
                   }
                };
                loadSorular();

                viewSorular.FocusedRowChanged += (s, e) => {
                    var soru = viewSorular.GetFocusedRow() as DanismaSorusu;
                    if (soru != null) {
                        txtQuestionDetail.Text = $"KİM: {soru.MusteriEmail}\nHEKİM: {soru.HekimAd}\nSORU: {soru.Soru}";
                        txtReplyMsg.Text = soru.Cevap ?? "";
                    }
                };

                btnSendReply.Click += (s, e) => {
                    var soru = viewSorular.GetFocusedRow() as DanismaSorusu;
                    if (soru == null) return;
                    if (string.IsNullOrEmpty(txtReplyMsg.Text.Trim())) {
                        MessageBox.Show("Lütfen cevap yazınız."); return;
                    }
                    try {
                        using (var db = new VetClinicContext()) {
                            var dbSoru = db.Sorular.Find(soru.Id);
                            if (dbSoru != null) {
                                dbSoru.Cevap = txtReplyMsg.Text.Trim();
                                dbSoru.Cevaplandi = true;
                                db.SaveChanges();
                            }
                        }
                        MessageBox.Show("Cevap başarıyla kaydedildi.");
                        loadSorular();
                    } catch { MessageBox.Show("Cevap kaydedilirken hata oluştu."); }
                };
            }
        }

        private List<DevExpress.XtraBars.Navigation.AccordionControlElement> customerElements = new List<DevExpress.XtraBars.Navigation.AccordionControlElement>();

        private void InitializeCustomerMenu()
        {
            string[] headers = {
                "👤 KİŞİSEL PROFİL BİLGİLERİ",
                "🐾 HAYVANLARIM",
                "⚕️ SAĞLIK GEÇMİŞİ",
                "📅 RANDEVULARIM",
                "➕ RANDEVU OLUŞTUR",
                "💳 ÖDEME BİLGİLERİM",
                "🏢 KLİNİK BİLGİLERİ",
                "❓ SORU & CEVAP",
                "⭐ DOKTOR DEĞERLENDİR"
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
                item.Appearance.Normal.ForeColor = Color.FromArgb(233, 163, 116);
                item.Appearance.Normal.Options.UseFont = true;
                item.Appearance.Normal.Options.UseForeColor = true;

                item.Appearance.Hovered.ForeColor = Color.FromArgb(180, 110, 70);
                item.Appearance.Hovered.Options.UseForeColor = true;
                
                item.Appearance.Pressed.ForeColor = Color.White;
                item.Appearance.Pressed.BackColor = Color.FromArgb(70, 130, 180);
                item.Appearance.Pressed.Options.UseForeColor = true;
                item.Appearance.Pressed.Options.UseBackColor = true;

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
            customerPanel.AutoScroll = true;
            
            // Müşteri Arkaplanı
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
                    customerPanel.BackgroundImage = Image.FromFile(resPath);
                    customerPanel.BackgroundImageLayout = ImageLayout.Stretch;
                } else {
                    customerPanel.BackColor = Color.AntiqueWhite;
                }
            } catch { customerPanel.BackColor = Color.AntiqueWhite; }
            // Giriş yapan kullanıcıyı bul
            var tumKullanicilar = KullaniciVeriYonetimi.Yukle();
            var currentUser = tumKullanicilar.FirstOrDefault(k => k.Email != null && k.Email.Equals(LoginForm.GirisYapanKullanici, StringComparison.OrdinalIgnoreCase));

            bool isWhitePanelSection = header.Contains("KİŞİSEL") || header.Contains("HAYVANLARIM") || 
                                       header.Contains("SAĞLIK GEÇMİŞİ") || header.Contains("RANDEVULARIM") || 
                                       header.Contains("ÖDEME BİLGİLERİM") || header.Contains("DEĞERLENDİR");

            // RANDEVU OLUŞTUR için özel işlem
            if (header.Contains("RANDEVU OLUŞTUR"))
            {
                OpenFormInPanel(new FrmRandevu());
                return;
            }

            if (header.Contains("KLİNİK BİLGİLERİ") || header.Contains("SORU & CEVAP"))
            {
                if (header.Contains("KLİNİK BİLGİLERİ"))
                    BuildClinicInfoUI(customerPanel, header);
                else
                    BuildQandAUI(customerPanel, header);

                panelContent.Controls.Add(customerPanel);
                customerPanel.BringToFront();
                return;
            }

            if (isWhitePanelSection)
            {
                // Bilgiler için Merkezi Özel Tasarım (Dikey sıkıştırıldı ve aşağı kaydırıldı)
                Panel pnlWhiteArea = new Panel();
                pnlWhiteArea.Name = "pnlWhiteArea";
                
                // Yükseklik tam sığacak şekilde azaltıldı (boşluklar kesildi)
                int pnlW = header.Contains("DEĞERLENDİR") ? 660 : 700;
                int pnlH = header.Contains("DEĞERLENDİR") ? 490 : 480;
                pnlWhiteArea.Size = new Size(pnlW, pnlH);
                pnlWhiteArea.BackColor = Color.FromArgb(250, 255, 255, 255); 
                
                Action centerPanel = () => {
                    if (customerPanel.Width > 0 && customerPanel.Height > 0)
                    {
                        // Değerlendirme sayfası da diğerleri gibi yukarı kaydırıldı (biraz daha dengeli -20).
                        int yOffset = header.Contains("DEĞERLENDİR") ? -20 : -40; 
                        
                        pnlWhiteArea.Location = new Point(
                            (customerPanel.Width - pnlWhiteArea.Width) / 2, 
                            ((customerPanel.Height - pnlWhiteArea.Height) / 2) + yOffset
                        );
                    }
                };

                customerPanel.Controls.Add(pnlWhiteArea);
                centerPanel();
                customerPanel.SizeChanged += (s, e) => centerPanel();

                Label lblProfileTitle = new Label();
                string titleText = header;
                if (!header.Contains(" ")) // İkon yoksa ekle (fallback)
                {
                    if (header.Contains("RANDEVU")) titleText = "📅 RANDEVU BİLGİLERİM";
                    else if (header.Contains("ÖDEME")) titleText = "💳 ÖDEME VE FATURA BİLGİLERİ";
                }
                
                lblProfileTitle.Text = titleText;
                lblProfileTitle.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
                lblProfileTitle.ForeColor = header.Contains("SAĞLIK") ? Color.ForestGreen : 
                                           header.Contains("RANDEVU") ? Color.RoyalBlue :
                                           header.Contains("ÖDEME") ? Color.Goldenrod : Color.DarkOrange;
                lblProfileTitle.BackColor = Color.Transparent;
                lblProfileTitle.Location = new Point(45, 30);
                lblProfileTitle.AutoSize = true;
                pnlWhiteArea.Controls.Add(lblProfileTitle);

                string[] fields;
                if (header.Contains("KİŞİSEL"))
                    fields = new[] { "Ad Soyad:", "TC Kimlik No:", "Doğum Tarihi:", "Cinsiyet:", "Sahip Olduğu Hayvan:" };
                else if (header.Contains("HAYVANLARIM"))
                    fields = new[] { "Hayvan Adı:", "Tür – Cins:", "Yaş:", "Mikroçip:", "Alerji / Kronik Hastalık:" };
                else if (header.Contains("SAĞLIK GEÇMİŞİ"))
                    fields = new[] { "Geçmiş Aşılar:", "Uygulanan Tedaviler:", "Klinik Notlar:", "Son Kontrol Tarihi:", "Sıradaki Randevu:" };
                else if (header.Contains("RANDEVULARIM"))
                    fields = new[] { "Aktif Randevu:", "Geçmiş Randevu 1:", "Geçmiş Randevu 2:", "İptal Edilenler:", "Kurum Mesajı:" };
                else if (header.Contains("DEĞERLENDİR"))
                {
                    BuildDoktorDegerlendirmeUI(pnlWhiteArea);
                    panelContent.Controls.Add(customerPanel);
                    customerPanel.BringToFront();
                    return;
                }
                else // ÖDEME BİLGİLERİM
                    fields = new[] { "Toplam Borç:", "Son Ödeme:", "Ödeme Yöntemi:", "Fatura Durumu:", "Banka Bilgisi:" };

                int currentY = 110;
                Dictionary<string, Control> controls = new Dictionary<string, Control>();

                foreach (var field in fields)
                {
                    Label lblField = new Label();
                    lblField.Text = field;
                    lblField.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                    lblField.ForeColor = Color.DimGray;
                    lblField.BackColor = Color.Transparent;
                    lblField.Location = new Point(50, currentY);
                    lblField.Width = 200;
                    pnlWhiteArea.Controls.Add(lblField);

                    Control inputControl;

                    if (field.Contains("TC Kimlik"))
                    {
                        var te = new TextEdit();
                        te.Properties.MaxLength = 11;
                        te.KeyPress += (s, e) => {
                            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
                        };
                        inputControl = te;
                    }
                    else if (field.Contains("Doğum Tarihi") || field.Contains("Son Kontrol") || field.Contains("Sıradaki"))
                    {
                        var de = new DateEdit();
                        de.Properties.Mask.EditMask = "dd.MM.yyyy";
                        de.Properties.Mask.UseMaskAsDisplayFormat = true;
                        inputControl = de;
                    }
                    else if (field.Contains("Cinsiyet") || field.Contains("Sahip Olduğu Hayvan") || 
                             field.Contains("Tür") || field.Contains("Yaş") || 
                             field.Contains("Mikroçip") || field.Contains("Alerji") ||
                             field.Contains("Ödeme Yöntemi"))
                    {
                        var cb = new ComboBoxEdit();
                        if (field.Contains("Cinsiyet")) cb.Properties.Items.AddRange(new object[] { "Erkek", "Kadın", "Belirtmek İstemiyorum" });
                        else if (field.Contains("Sahip Olduğu Hayvan")) cb.Properties.Items.AddRange(new object[] { "Kedi", "Köpek", "Kuş", "Tavşan", "Hamster", "Kaplumbağa", "Papağan", "Egzotik", "Diğer" });
                        else if (field.Contains("Tür")) cb.Properties.Items.AddRange(new object[] { 
                            "Kedi (Tekir)", "Kedi (British Shorthair)", "Kedi (Scottish Fold)", "Kedi (Siyam)", "Kedi (Ankara)",
                            "Köpek (Golden Retriever)", "Köpek (Terrier)", "Köpek (Alman Kurdu)", "Köpek (Pug)", "Köpek (Pitbull)",
                            "Kuş (Muhabbet)", "Kuş (Kanarya)", "Kuş (Papağan)",
                            "Tavşan", "Hamster", "Ginepig", "Diğer"
                        });
                        else if (field.Contains("Yaş")) cb.Properties.Items.AddRange(new object[] { 
                            "0-1 (Bebek/Yavru)", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", 
                            "11", "12", "13", "14", "15", "16+", "Bilinmiyor" 
                        });
                        else if (field.Contains("Mikroçip")) cb.Properties.Items.AddRange(new object[] { "Mevcut", "Mevcut Değil", "Bilinmiyor" });
                        else if (field.Contains("Alerji")) cb.Properties.Items.AddRange(new object[] { 
                            "Yok", "Deri Alerjisi", "Gıda Hassasiyeti", "Böbrek Yetmezliği", 
                            "Kalp Rahatsızlığı", "Diyabet", "Eklem Problemleri", "Diğer" 
                        });
                        else if (field.Contains("Ödeme Yöntemi")) cb.Properties.Items.AddRange(new object[] { "Kredi Kartı", "Nakit", "Havale/EFT", "Mobil Ödeme" });
                        
                        cb.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                        inputControl = cb;
                    }
                    else
                    {
                        var te = new TextEdit();
                        te.Properties.NullText = "Bilgi girilmemiş...";
                        inputControl = te;
                    }

                    inputControl.Location = new Point(250, currentY - 3);
                    inputControl.Size = new Size(400, 32); // Genişlik geri verildi
                    inputControl.Font = new Font("Segoe UI", 11F);
                    pnlWhiteArea.Controls.Add(inputControl);

                    // Mevcut verilerle doldur (HATA DÜZELTİLDİ: DateEdit ve diğerleri direkt değerden yükleniyor)
                    if (currentUser != null)
                    {
                        if (header.Contains("KİŞİSEL"))
                        {
                            if (field.Contains("Ad Soyad")) inputControl.Text = currentUser.AdSoyad;
                            else if (field.Contains("TC Kimlik")) inputControl.Text = currentUser.TCKimlikNo;
                            else if (field.Contains("Doğum Tarihi")) 
                            { 
                                if (DateTime.TryParse(currentUser.DogumTarihi, out DateTime dt)) ((DateEdit)inputControl).DateTime = dt;
                            }
                            else if (field.Contains("Cinsiyet")) inputControl.Text = currentUser.Cinsiyet;
                            else if (field.Contains("Hayvan")) inputControl.Text = currentUser.SahipOlduguHayvan;
                        }
                        else if (header.Contains("HAYVANLARIM"))
                        {
                            if (field.Contains("Hayvan Adı")) inputControl.Text = currentUser.HayvanAdi;
                            else if (field.Contains("Tür")) inputControl.Text = currentUser.HayvanTurCins;
                            else if (field.Contains("Yaş")) inputControl.Text = currentUser.HayvanYasi;
                            else if (field.Contains("Mikroçip")) inputControl.Text = currentUser.HayvanMikrocip;
                            else if (field.Contains("Alerji")) inputControl.Text = currentUser.HayvanSaglikNotu;
                        }
                        else if (header.Contains("SAĞLIK GEÇMİŞİ"))
                        {
                            inputControl.Enabled = false;
                            inputControl.BackColor = Color.White;
                            if (field.Contains("Aşılar")) inputControl.Text = "Karma Aşı (Bitmiş), Kuduz (01/2026), Paraziter Uygulama";
                            else if (field.Contains("Tedaviler")) inputControl.Text = "Hafif Enfeksiyon Tedavisi (Piyoderma), Rutin Diş Temizliği";
                            else if (field.Contains("Klinik Notlar")) inputControl.Text = "Genel sağlık durumu gayet iyi. Beslenme düzenine uyulmalı.";
                            else if (field.Contains("Son Kontrol")) inputControl.Text = "24.12.2025 - Su Veteriner Kliniği";
                            else if (field.Contains("Sıradaki")) inputControl.Text = "24.03.2026 - Genel Check-up";
                        }
                        else if (header.Contains("RANDEVULARIM"))
                        {
                            inputControl.Enabled = false;
                            inputControl.BackColor = Color.White;
                            
                            // Giriş yapan kullanıcının e-postasına göre filtrele
                            string currentEmail = LoginForm.GirisYapanKullanici;

                            // Dinamik randevu verilerini RandevuListesi'nden çek ve filtrele
                            var userRandevular = FrmRandevu.RandevuListesi
                                .Where(r => r.HastaSoyad != null && r.HastaSoyad.Equals(currentEmail, StringComparison.OrdinalIgnoreCase))
                                .OrderByDescending(r => r.RandevuTarihi)
                                .ToList();

                            var aktifRandevular = userRandevular
                                .Where(r => r.Durum != "İptal Edildi" && r.Durum != "Tamamlandı")
                                .ToList();
                            var gecmisRandevular = userRandevular
                                .Where(r => r.Durum == "Tamamlandı")
                                .ToList();
                            var iptalRandevular = userRandevular
                                .Where(r => r.Durum == "İptal Edildi")
                                .ToList();

                            if (field.Contains("Aktif"))
                            {
                                if (aktifRandevular.Count > 0)
                                {
                                    var r = aktifRandevular.First();
                                    inputControl.Text = $"{r.RandevuTarihi:dd.MM.yyyy HH:mm} - {r.Hekim} ({r.Tur})";
                                }
                                else inputControl.Text = "Aktif randevunuz bulunmamaktadır.";
                            }
                            else if (field.Contains("Geçmiş Randevu 1"))
                            {
                                if (gecmisRandevular.Count > 0)
                                {
                                    var r = gecmisRandevular.First();
                                    inputControl.Text = $"{r.RandevuTarihi:dd.MM.yyyy HH:mm} - {r.Hekim} ({r.Tur})";
                                }
                                else inputControl.Text = "-";
                            }
                            else if (field.Contains("Geçmiş Randevu 2"))
                            {
                                if (gecmisRandevular.Count > 1)
                                {
                                    var r = gecmisRandevular[1];
                                    inputControl.Text = $"{r.RandevuTarihi:dd.MM.yyyy HH:mm} - {r.Hekim} ({r.Tur})";
                                }
                                else inputControl.Text = "-";
                            }
                            else if (field.Contains("İptal"))
                            {
                                inputControl.Text = iptalRandevular.Count > 0 ? $"{iptalRandevular.Count} adet iptal edilmiş randevu" : "Yok";
                            }
                            else if (field.Contains("Kurum")) inputControl.Text = "Randevunuza 15 dakika önce gelmeniz rica olunur.";
                        }
                        else if (header.Contains("ÖDEME BİLGİLERİM"))
                        {
                            inputControl.Enabled = false;
                            inputControl.BackColor = Color.White;
                            if (field.Contains("Toplam Borç")) inputControl.Text = "0,00 ₺ (Tüm borçlar ödenmiştir)";
                            else if (field.Contains("Son Ödeme")) inputControl.Text = "450,00 ₺ - 24.12.2025 (Kredi Kartı)";
                            else if (field.Contains("Ödeme Yöntemi")) inputControl.Text = "Kredi Kartı / Nakit / Havale";
                            else if (field.Contains("Fatura")) inputControl.Text = "E-Arşiv Faturası Oluşturuldu (E-Postanıza Gönderildi)";
                            else if (field.Contains("Banka")) inputControl.Text = "TR21 0006 2000 0000 1234 5678 90 (Su Vet Ltd. Şti.)";
                        }
                    }

                    controls[field] = inputControl;
                    currentY += 55; // Daha kompakt olması için aralık daraltıldı
                }

                bool isEditable = header.Contains("KİŞİSEL") || header.Contains("HAYVANLARIM");
                if (isEditable) 
                {
                    SimpleButton btnGuncelle = new SimpleButton();
                    btnGuncelle.Text = "DEĞİŞİKLİKLERİ KAYDET";
                    btnGuncelle.Size = new Size(600, 50); // Genişlik geri verildi
                    btnGuncelle.Location = new Point(50, currentY + 10);
                    btnGuncelle.Appearance.BackColor = Color.DarkOrange;
                    btnGuncelle.Appearance.ForeColor = Color.White;
                    btnGuncelle.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                    btnGuncelle.Appearance.Options.UseBackColor = true;
                    btnGuncelle.Appearance.Options.UseForeColor = true;
                    btnGuncelle.Appearance.Options.UseFont = true;
                    btnGuncelle.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
                    btnGuncelle.Click += (s, e) => {
                        if (currentUser != null)
                        {
                            if (header.Contains("KİŞİSEL"))
                            {
                                string tc = controls["TC Kimlik No:"].Text.Trim();
                                if (!string.IsNullOrEmpty(tc) && tc.Length != 11) {
                                    MessageBox.Show("TC Kimlik No 11 haneli olmalıdır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                currentUser.AdSoyad = controls["Ad Soyad:"].Text.Trim();
                                currentUser.TCKimlikNo = tc;
                                
                                // Tarih Verisi Güvenli Kayıt
                                if (controls["Doğum Tarihi:"] is DateEdit de && de.EditValue != null)
                                    currentUser.DogumTarihi = de.DateTime.ToString("dd.MM.yyyy");
                                else
                                    currentUser.DogumTarihi = controls["Doğum Tarihi:"].Text.Trim();

                                currentUser.Cinsiyet = controls["Cinsiyet:"].Text.Trim();
                                currentUser.SahipOlduguHayvan = controls["Sahip Olduğu Hayvan:"].Text.Trim();
                            }
                            else if (header.Contains("HAYVANLARIM"))
                            {
                                currentUser.HayvanAdi = controls["Hayvan Adı:"].Text.Trim();
                                currentUser.HayvanTurCins = controls["Tür – Cins:"].Text.Trim();
                                currentUser.HayvanYasi = controls["Yaş:"].Text.Trim();
                                currentUser.HayvanMikrocip = controls["Mikroçip:"].Text.Trim();
                                currentUser.HayvanSaglikNotu = controls["Alerji / Kronik Hastalık:"].Text.Trim();
                            }
                            
                            if (KullaniciVeriYonetimi.Kaydet(tumKullanicilar))
                            {
                                MessageBox.Show("Bilgileriniz başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Bilgiler kaydedilirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    };
                    pnlWhiteArea.Controls.Add(btnGuncelle);
                }
            }
            else
            {
                // ANASAYFA vb. için varsayılan boş bırak
            }

            panelContent.Controls.Add(customerPanel);
            customerPanel.BringToFront();
        }

        private void BuildDoktorDegerlendirmeUI(Panel parent)
        {
            int currentY = 80; // Üst boşluk daraltıldı
            int x = 40; // Sol boşluk hafif daraltıldı (660 genişliğe uyum)

            // 1. Hekim Seçimi
            Label lblHekim = new Label();
            lblHekim.Text = "👨‍⚕️ Değerlendirilecek Hekim:";
            lblHekim.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblHekim.ForeColor = Color.DimGray;
            lblHekim.Location = new Point(x, currentY);
            lblHekim.Width = 250;
            parent.Controls.Add(lblHekim);

            ComboBoxEdit cmbHekim = new ComboBoxEdit();
            cmbHekim.Location = new Point(x + 250, currentY - 3);
            cmbHekim.Size = new Size(350, 32);
            cmbHekim.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            
            using (var db = new VetClinicContext())
            {
                var hekimler = db.Personeller
                    .Where(p => p.Gorev.Contains("Hekim") || p.Yetki == "Veteriner")
                    .Select(p => p.AdSoyad)
                    .ToList();
                cmbHekim.Properties.Items.AddRange(hekimler);
            }
            parent.Controls.Add(cmbHekim);

            currentY += 38; // Daha fazla sıkıştırıldı

            // 2. Puanlama (1-5)
            Label lblPuan = new Label();
            lblPuan.Text = "⭐ Puanınız (1-5):";
            lblPuan.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblPuan.ForeColor = Color.DimGray;
            lblPuan.Location = new Point(x, currentY);
            lblPuan.Width = 250;
            parent.Controls.Add(lblPuan);

            RadioGroup rgPuan = new RadioGroup();
            rgPuan.Location = new Point(x + 250, currentY - 3);
            rgPuan.Size = new Size(350, 35);
            rgPuan.Properties.Items.AddRange(new[] {
                new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "1"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "2"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "3"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem(4, "4"),
                new DevExpress.XtraEditors.Controls.RadioGroupItem(5, "5")
            });
            rgPuan.EditValue = 5;
            parent.Controls.Add(rgPuan);

            currentY += 38; // Daha fazla sıkıştırıldı

            // 3. Yorum
            Label lblYorum = new Label();
            lblYorum.Text = "💬 Yorumunuz:";
            lblYorum.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblYorum.ForeColor = Color.DimGray;
            lblYorum.Location = new Point(x, currentY);
            lblYorum.Width = 250;
            parent.Controls.Add(lblYorum);

            DevExpress.XtraEditors.MemoEdit txtYorum = new DevExpress.XtraEditors.MemoEdit();
            txtYorum.Location = new Point(x + 250, currentY - 3);
            txtYorum.Size = new Size(350, 110); // Yükseklik azaltıldı
            txtYorum.Properties.NullValuePrompt = "Doktorumuz hakkındaki görüşlerinizi buraya yazabilirsiniz...";
            parent.Controls.Add(txtYorum);

            currentY += 110; // Daha fazla sıkıştırıldı

            // 4. Kaydet Butonu
            SimpleButton btnKaydet = new SimpleButton();
            btnKaydet.Text = "DEĞERLENDİRMEYİ GÖNDER";
            btnKaydet.Size = new Size(350, 45);
            btnKaydet.Location = new Point(x + 250, currentY);
            btnKaydet.Appearance.BackColor = Color.ForestGreen;
            btnKaydet.Appearance.ForeColor = Color.White;
            btnKaydet.Appearance.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnKaydet.Appearance.Options.UseBackColor = true;
            btnKaydet.Appearance.Options.UseForeColor = true;
            btnKaydet.Appearance.Options.UseFont = true;
            btnKaydet.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            parent.Controls.Add(btnKaydet);

            currentY += 50;

            // 5. Örnek Yorumlar Başlığı ve SIRALAMA FİLTRESİ
            Label lblOrnekBaslik = new Label();
            lblOrnekBaslik.Text = "📝 Son Değerlendirmeler";
            lblOrnekBaslik.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblOrnekBaslik.ForeColor = Color.DimGray;
            lblOrnekBaslik.Location = new Point(x, currentY);
            lblOrnekBaslik.AutoSize = true;
            parent.Controls.Add(lblOrnekBaslik);

            ComboBoxEdit cmbSira = new ComboBoxEdit();
            cmbSira.Properties.Items.AddRange(new string[] { "Yeniden Eskiye", "Eskiden Yeniye", "En Yüksek Puan", "En Düşük Puan" });
            cmbSira.SelectedIndex = 0;
            cmbSira.Size = new Size(160, 28);
            cmbSira.Location = new Point(parent.Width - 160 - x, currentY - 5);
            cmbSira.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            parent.Controls.Add(cmbSira);

            currentY += 30;

            // 6. Yorum Listesi (GridControl)
            DevExpress.XtraGrid.GridControl gridYorumlar = new DevExpress.XtraGrid.GridControl();
            gridYorumlar.Location = new Point(x, currentY);
            gridYorumlar.Size = new Size(parent.Width - (2 * x), 130); 
            gridYorumlar.LookAndFeel.UseDefaultLookAndFeel = false;
            gridYorumlar.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;

            DevExpress.XtraGrid.Views.Grid.GridView viewYorumlar = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridYorumlar.MainView = viewYorumlar;
            gridYorumlar.ViewCollection.Add(viewYorumlar);

            Action refreshGrid = () => {
                using (var db = new VetClinicContext())
                {
                    var query = db.Degerlendirmeler.AsQueryable();
                    
                    switch (cmbSira.Text)
                    {
                        case "Yeniden Eskiye": query = query.OrderByDescending(d => d.Tarih); break;
                        case "Eskiden Yeniye": query = query.OrderBy(d => d.Tarih); break;
                        case "En Yüksek Puan": query = query.OrderByDescending(d => d.Puan); break;
                        case "En Düşük Puan": query = query.OrderBy(d => d.Puan); break;
                        default: query = query.OrderByDescending(d => d.Tarih); break;
                    }

                    var liste = query.Select(d => new { d.HekimAdSoyad, d.Puan, d.Yorum, d.Tarih })
                                     .Take(20)
                                     .ToList();
                    gridYorumlar.DataSource = liste;
                }
                viewYorumlar.PopulateColumns();
                if (viewYorumlar.Columns["HekimAdSoyad"] != null) viewYorumlar.Columns["HekimAdSoyad"].Caption = "HEKİM";
                if (viewYorumlar.Columns["Puan"] != null) { viewYorumlar.Columns["Puan"].Caption = "PUAN"; viewYorumlar.Columns["Puan"].Width = 50; }
                if (viewYorumlar.Columns["Yorum"] != null) { viewYorumlar.Columns["Yorum"].Caption = "YORUM"; viewYorumlar.Columns["Yorum"].Width = 250; }
                if (viewYorumlar.Columns["Tarih"] != null) { viewYorumlar.Columns["Tarih"].Caption = "TARİH"; viewYorumlar.Columns["Tarih"].DisplayFormat.FormatString = "dd.MM.yyyy"; viewYorumlar.Columns["Tarih"].Width = 80; }
                viewYorumlar.OptionsView.ShowGroupPanel = false;
                viewYorumlar.OptionsBehavior.Editable = false;
            };

            cmbSira.SelectedIndexChanged += (s, e) => refreshGrid();
            parent.Controls.Add(gridYorumlar);
            refreshGrid();

            // Kaydet butonunda refreshGrid'i çağır
            btnKaydet.Click += (s, e) => {
                if (cmbHekim.SelectedIndex == -1) {
                    MessageBox.Show("Lütfen bir hekim seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtYorum.Text)) {
                    MessageBox.Show("Lütfen yorumunuzu yazınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var db = new VetClinicContext())
                {
                    db.Degerlendirmeler.Add(new DoktorDegerlendirme {
                        MusteriEmail = LoginForm.GirisYapanKullanici,
                        HekimAdSoyad = cmbHekim.Text,
                        Puan = (int)rgPuan.EditValue,
                        Yorum = txtYorum.Text.Trim(),
                        Tarih = DateTime.Now
                    });
                    db.SaveChanges();
                }
                MessageBox.Show("Değerlendirmeniz başarıyla iletildi. Teşekkür ederiz!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtYorum.Text = "";
                cmbHekim.SelectedIndex = -1;
                refreshGrid();
            };
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
            // Eğer isFull ise (Anasayfa modu) picHome'u gizle ki dashboardPanel öne gelsin
            // Sadece resim göstermek istediğimizde isFull=true ama dashboardPanel=null ise picHome açılır
            picHome.Visible = false; // Artık dashboardPanel kullanıyoruz
            
            if (isFull)
            {
                panelHastaEkle.Visible = false;
                panelHastaListele.Visible = false;
                // panelContent içindeki diğer özel formları da gizle/kapat
                foreach (Control ctrl in panelContent.Controls)
                {
                    if (ctrl is Form) ctrl.Hide();
                }
                
                // Dashboard paneli göster
                ShowDashboard();
            }
            else
            {
                if (dashboardPanel != null) dashboardPanel.Visible = false;
            }
        }
        
        // Dashboard Panel - Anasayfa Özet Bilgileri
        private Panel dashboardPanel;
        
        private async void ShowDashboard()
        {
            if (dashboardPanel != null)
            {
                panelContent.Controls.Remove(dashboardPanel);
                dashboardPanel.Dispose();
            }
            
            dashboardPanel = new Panel();
            dashboardPanel.Dock = DockStyle.Fill;
            
            // Modern Başlık
            DevExpress.XtraEditors.LabelControl lblHosgeldin = new DevExpress.XtraEditors.LabelControl();
            
            // Durumu yerel bir değişkene alarak daha güvenli hale getiriyoruz
            bool isSystemAdmin = LoginForm.AdminMi;

            // === DASHBOARD UI GÜNCELLEME ===
            lblHosgeldin.Visible = true;
            if (isSystemAdmin)
            {
                lblHosgeldin.Text = "✨ HOŞ GELDİNİZ ✨";
                lblHosgeldin.Font = new Font("Segoe UI Black", 48F, FontStyle.Bold);
                lblHosgeldin.Appearance.ForeColor = Color.FromArgb(40, 80, 120);
            }
            else
            {
                lblHosgeldin.Text = "SU VETERİNER KLİNİĞİNE\nHOŞ GELDİNİZ";
                lblHosgeldin.Font = new Font("Segoe UI", 42F, FontStyle.Bold); // Daha modern ve soft font
                lblHosgeldin.Appearance.ForeColor = Color.FromArgb(43, 87, 115); // Yumuşak bir geçiş hissi veren muted mavi
            }
            
            lblHosgeldin.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            lblHosgeldin.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            lblHosgeldin.AutoSizeMode = LabelAutoSizeMode.None;
            lblHosgeldin.Size = new Size(dashboardPanel.Width, isSystemAdmin ? 120 : 200);
            lblHosgeldin.BackColor = Color.Transparent;
            dashboardPanel.Controls.Add(lblHosgeldin);

            // Tarih Etiketi
            DevExpress.XtraEditors.LabelControl lblDate = new DevExpress.XtraEditors.LabelControl();
            lblDate.Text = DateTime.Now.ToString("dd MMMM yyyy, dddd").ToUpper();
            lblDate.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblDate.Appearance.ForeColor = Color.FromArgb(100, 120, 140); // Daha yumuşak gri-mavi tonu
            lblDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            lblDate.AutoSizeMode = LabelAutoSizeMode.None;
            lblDate.Size = new Size(dashboardPanel.Width, 40);
            lblDate.BackColor = Color.Transparent;
            dashboardPanel.Controls.Add(lblDate);

            DevExpress.XtraEditors.LabelControl lblSubTitle = null; 

            if (isSystemAdmin)
            {
                dashboardPanel.BackColor = Color.FromArgb(240, 250, 255); 
                lblSubTitle = new DevExpress.XtraEditors.LabelControl();
                lblSubTitle.Text = "Sistem Aktif. Tüm kontroller sizin elinizde.";
                lblSubTitle.Visible = true;
                lblSubTitle.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Italic);
                lblSubTitle.Appearance.ForeColor = Color.FromArgb(100, 150, 180);
                lblSubTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
                lblSubTitle.Size = new Size(800, 40); 
                lblSubTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                dashboardPanel.Controls.Add(lblSubTitle);
            }
            else
            {
                dashboardPanel.BackColor = Color.Honeydew;
            }

            Action centerTitle = () => {
                lblHosgeldin.Width = dashboardPanel.Width;
                lblHosgeldin.Top = isSystemAdmin ? 100 : 180; // Müşteri için biraz daha aşağı
                
                lblDate.Width = dashboardPanel.Width;
                lblDate.Top = lblHosgeldin.Bottom + (isSystemAdmin ? 5 : 10);

                if (lblSubTitle != null) {
                    lblSubTitle.Width = dashboardPanel.Width;
                    lblSubTitle.Top = lblDate.Bottom + 10;
                }
            };

            dashboardPanel.SizeChanged += (s, e) => centerTitle();
            centerTitle();

            // === ARKA PLAN YÜKLEME ===
            try {
                string fileName = isSystemAdmin ? "su_vet_admin_global_bg.png" : "customer_bg.png";
                string resPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", fileName);
                
                // Eğer doğrudan bulunamazsa üst klasörlerde ara
                if (!System.IO.File.Exists(resPath)) {
                     string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                     for (int i = 0; i < 5; i++) {
                         string checkPath = System.IO.Path.Combine(currentDir, "Resources", fileName);
                         if (System.IO.File.Exists(checkPath)) { resPath = checkPath; break; }
                         currentDir = System.IO.Directory.GetParent(currentDir)?.FullName;
                         if (currentDir == null) break;
                     }
                }

                if (System.IO.File.Exists(resPath)) {
                    dashboardPanel.BackgroundImage = Image.FromFile(resPath);
                    dashboardPanel.BackgroundImageLayout = ImageLayout.Stretch;
                }
                else {
                    dashboardPanel.BackColor = isSystemAdmin ? Color.FromArgb(245, 250, 255) : Color.Honeydew;
                }
            } catch { }

            if (isSystemAdmin)
            {
                // === YÖNETİCİ İSTATİSTİK KARTLARI ===
                int cardWidth = 260;
                int cardHeight = 160;
                int startX = 50;
                int startY = 320; // Kartları biraz daha yukarı taşıdık (Eski: 450)
                int gap = 30;

                // Daha canlı ve arka planla uyumlu renkler
                Color color1 = Color.FromArgb(0, 128, 128);   // Teal (Toplam Hasta)
                Color color2 = Color.FromArgb(70, 130, 180);  // Steel Blue (Bugün Bakılacak)
                Color color3 = Color.FromArgb(255, 127, 80);  // Coral (Bekleyenler)
                Color color4 = Color.FromArgb(147, 112, 219); // Medium Purple (Tamamlanan)

                var cardHasta = CreateModernCard("🐾 TOPLAM HASTA", "...", color1, startX, startY, cardWidth, cardHeight);
                var cardTodayTotal = CreateModernCard("📅 BUGÜN BAKILACAK", "...", color2, startX + cardWidth + gap, startY, cardWidth, cardHeight);
                var cardTodayBekleyen = CreateModernCard("⏳ BEKLEYENLER", "...", color3, startX + (cardWidth + gap) * 2, startY, cardWidth, cardHeight);
                var cardTodayTamamlanan = CreateModernCard("✅ TAMAMLANAN", "...", color4, startX + (cardWidth + gap) * 3, startY, cardWidth, cardHeight);

                dashboardPanel.Controls.AddRange(new Control[] { cardHasta, cardTodayTotal, cardTodayBekleyen, cardTodayTamamlanan });

                await System.Threading.Tasks.Task.Run(() => {
                    int toplamHasta = 0;
                    using (var db = new VetClinicContext()) { try { toplamHasta = db.Hastalar.Count(); } catch { toplamHasta = HastaListesi.Count; } }
                    
                    DateTime today = DateTime.Today;
                    var todayRandevular = FrmRandevu.RandevuListesi.Where(r => r.RandevuTarihi.Date == today).ToList();
                    
                    int gunlukHasta = todayRandevular.Count(r => r.Durum != "İptal Edildi");
                    int bekleyen = todayRandevular.Count(r => r.Durum != "Tamamlandı" && r.Durum != "İptal Edildi");
                    int tamamlanan = todayRandevular.Count(r => r.Durum == "Tamamlandı");

                    this.Invoke(new Action(() => {
                        UpdateCardValue(cardHasta, toplamHasta.ToString());
                        UpdateCardValue(cardTodayTotal, gunlukHasta.ToString());
                        UpdateCardValue(cardTodayBekleyen, bekleyen.ToString());
                        UpdateCardValue(cardTodayTamamlanan, tamamlanan.ToString());
                    }));
                });
            }

            panelContent.Controls.Add(dashboardPanel);
            dashboardPanel.BringToFront();
        }

        private void UpdateCardValue(Control card, string newValue)
        {
            foreach (Control c in card.Controls)
            {
                if (c is DevExpress.XtraEditors.LabelControl lbl && lbl.Appearance.Font.Size > 20)
                {
                    lbl.Text = newValue;
                }
            }
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
            // FrmRoleSelection'a geri dön
            FrmRoleSelection roleSelect = new FrmRoleSelection();
            roleSelect.Show();
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
            // Menü renklerini başlangıçta ayarla
            if (!LoginForm.AdminMi)
            {
                accMenu.Appearance.AccordionControl.BackColor = Color.FromArgb(255, 240, 220); // Açık turuncu ton
                accMenu.Appearance.AccordionControl.Options.UseBackColor = true;
                accordionControlElementAnasayfa.Appearance.Normal.ForeColor = Color.DarkOrange;
                accordionControlElementAnasayfa.Appearance.Normal.Options.UseForeColor = true;
                
                // Title bar rengini turuncu yap
                this.Appearance.BackColor = Color.FromArgb(210, 105, 30);
                this.Appearance.ForeColor = Color.White;
                this.Appearance.Options.UseBackColor = true;
                this.Appearance.Options.UseForeColor = true;
                this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            }

            // Menü görünürlüklerini ayarla
            foreach (var el in adminElements) el.Visible = LoginForm.AdminMi;
            foreach (var el in customerElements) el.Visible = !LoginForm.AdminMi;

            // ESKİ VE KARIŞIKLIĞA SEBEP OLAN MENÜ GRUPLARINI KESİN OLARAK GİZLE
            accordionControlElement2.Visible = false; // Müşteri Paneli (Eski)
            accordionControlElementRandevu.Visible = false; // Randevu Paneli (Eski)
            accordionControlElementSistem.Visible = false; // Sistem Ayarları (Eski)
            
            // Grupları da kontrol et (Müşteri için admin gruplarını, Admin için müşteri gruplarını kapat)
            if (LoginForm.AdminMi) {
                if (accMenu.Elements["accordionControlElementMusteriRoot"] != null) 
                    accMenu.Elements["accordionControlElementMusteriRoot"].Visible = false;
            } else {
                 if (accMenu.Elements["accordionControlElementAdminRoot"] != null) 
                    accMenu.Elements["accordionControlElementAdminRoot"].Visible = false;
            }

            if (!accMenu.Elements.Contains(accCikis))
            {
                accMenu.Elements.Add(accCikis);
                accCikis.Text = "🚪 GÜVENLİ ÇIKIŞ";
                accCikis.Appearance.Normal.ForeColor = LoginForm.AdminMi ? Color.DarkRed : Color.DarkOrange;
            }
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            // Form gösterildiğinde ağır işlemleri başlat
            await System.Threading.Tasks.Task.Run(() => {
                var kayitliHastalar = HastaVeriYonetimi.Yukle();
                this.Invoke(new Action(() => {
                    HastaListesi.Clear();
                    foreach (var h in kayitliHastalar) HastaListesi.Add(h);
                    gridControl1.DataSource = HastaListesi;
                }));
            });

            ShowFullScreenImage(true);
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
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "su_vet_admin_global_bg.png");
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

            // Başlık (Premium Stil)
            System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label();
            lblTitle.Text = title.ToUpper();
            lblTitle.Font = new Font("Segoe UI Black", 28F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(233, 163, 116); // #E9A374
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Location = new Point(50, 30);
            lblTitle.AutoSize = true;
            parentPanel.Controls.Add(lblTitle);

            // Alt Süsleme Çizgisi
            System.Windows.Forms.Label line = new System.Windows.Forms.Label();
            line.BackColor = Color.FromArgb(233, 163, 116); // #E9A374
            line.Size = new Size(100, 5);
            line.Location = new Point(55, lblTitle.Bottom - 5);
            parentPanel.Controls.Add(line);

            // Bilgilendirme Notu
            System.Windows.Forms.Label lblInfo = new System.Windows.Forms.Label();
            lblInfo.Text = "ℹ️ Detayları görüntülemek için personelin üzerine tıklayınız.";
            lblInfo.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblInfo.ForeColor = System.Drawing.Color.DimGray;
            lblInfo.BackColor = Color.Transparent;
            lblInfo.Location = new Point(55, 85);
            lblInfo.AutoSize = true;
            parentPanel.Controls.Add(lblInfo);

            // Grid Control (Yüksekliği içeriğe göre ayarlandı)
            DevExpress.XtraGrid.GridControl grid = new DevExpress.XtraGrid.GridControl();
            grid.Location = new Point(50, 110);
            grid.Size = new Size(1000, 460);
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
            view.Appearance.HeaderPanel.BackColor = Color.FromArgb(255, 107, 53); // #FF6B35 Orange
            view.Appearance.HeaderPanel.ForeColor = Color.White;
            view.Appearance.HeaderPanel.Options.UseBackColor = true;
            view.Appearance.HeaderPanel.Options.UseForeColor = true;
            view.Appearance.HeaderPanel.Options.UseFont = true;

            // Satır Stili
            view.Appearance.Row.Font = new Font("Segoe UI", 11F);
            view.Appearance.Row.Options.UseFont = true;

            // Zebra Effect
            view.OptionsView.EnableAppearanceEvenRow = true;
            view.Appearance.EvenRow.BackColor = Color.FromArgb(255, 248, 245); // Çok açık turuncu/şeftali tonu
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
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "su_vet_admin_global_bg.png");
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
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "su_vet_admin_global_bg.png");
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

            // Başlık (Premium Stil)
            System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label();
            lblTitle.Text = title.ToUpper();
            lblTitle.Font = new Font("Segoe UI Black", 28F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(233, 163, 116); // #E9A374
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Location = new Point(50, 30);
            lblTitle.AutoSize = true;
            parentPanel.Controls.Add(lblTitle);

            // Alt Süsleme Çizgisi
            System.Windows.Forms.Label line = new System.Windows.Forms.Label();
            line.BackColor = Color.FromArgb(233, 163, 116); // #E9A374
            line.Size = new Size(100, 5);
            line.Location = new Point(55, lblTitle.Bottom - 5);
            parentPanel.Controls.Add(line);

            // Grid Control (Yüksekliği azaltıldı)
            DevExpress.XtraGrid.GridControl grid = new DevExpress.XtraGrid.GridControl();
            grid.Location = new Point(50, 100);
            grid.Size = new Size(1000, 400);
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
            view.Appearance.HeaderPanel.BackColor = Color.FromArgb(255, 107, 53); // #FF6B35 Orange
            view.Appearance.HeaderPanel.ForeColor = Color.White;
            view.Appearance.HeaderPanel.Options.UseBackColor = true;
            view.Appearance.HeaderPanel.Options.UseForeColor = true;
            view.Appearance.HeaderPanel.Options.UseFont = true;

            // Satır Stili
            view.Appearance.Row.Font = new Font("Segoe UI", 11F);
            view.Appearance.Row.Options.UseFont = true;

            // Zebra Effect
            view.OptionsView.EnableAppearanceEvenRow = true;
            view.Appearance.EvenRow.BackColor = Color.FromArgb(255, 248, 245);
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
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "su_vet_admin_global_bg.png");
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

            parentPanel.Controls.Clear();

            // Başlık (Premium Stil)
            System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label();
            lblTitle.Text = title.ToUpper();
            lblTitle.Font = new Font("Segoe UI Black", 28F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(233, 163, 116); // #E9A374
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Location = new Point(50, 30);
            lblTitle.AutoSize = true;
            parentPanel.Controls.Add(lblTitle);

            // Alt Süsleme Çizgisi
            System.Windows.Forms.Label line = new System.Windows.Forms.Label();
            line.BackColor = Color.FromArgb(233, 163, 116); // #E9A374
            line.Size = new Size(100, 5);
            line.Location = new Point(55, lblTitle.Bottom - 5);
            parentPanel.Controls.Add(line);


            // FlowLayoutPanel (Kartlar için konteyner - Yüksekliği azaltıldı)
            System.Windows.Forms.FlowLayoutPanel flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            flowPanel.Location = new Point(50, 110);
            flowPanel.Size = new Size(1150, 520); // Daha fazla kart görünmesi için boyu uzatıldı
            flowPanel.AutoScroll = true;
            flowPanel.BackColor = Color.Transparent; 
            parentPanel.Controls.Add(flowPanel);

            // Verileri Hazırla
            List<HastaDetay> hastaListesi;
            using (var db = new VetClinicContext())
            {
                 db.EnsureSeeded();
                 hastaListesi = db.Hastalar.ToList();
            }

            // ANA TEMA RENGİ (#FF6B35)
            Color themeOrange = Color.FromArgb(255, 107, 53);

            foreach (var h in hastaListesi)
            {
                Panel card = new Panel();
                card.Size = new Size(220, 320);
                card.BackColor = Color.White;
                card.Margin = new Padding(10);
                card.BorderStyle = BorderStyle.None;
                card.Paint += (s, e) => 
                {
                    int thickness = 2;
                    using (Pen p = new Pen(themeOrange, thickness))
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
                lblName.ForeColor = themeOrange; // Artık yeni turuncu rengimizi kullanıyor
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
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "su_vet_admin_global_bg.png");
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

            parentPanel.Controls.Clear();
            
            // Başlık (Premium Stil)
            System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label();
            lblTitle.Text = title.ToUpper();
            lblTitle.Font = new Font("Segoe UI Black", 28F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(233, 163, 116); // #E9A374
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Location = new Point(50, 30);
            lblTitle.AutoSize = true;
            parentPanel.Controls.Add(lblTitle);

            // Alt Süsleme Çizgisi
            System.Windows.Forms.Label line = new System.Windows.Forms.Label();
            line.BackColor = Color.FromArgb(233, 163, 116); // #E9A374
            line.Size = new Size(100, 5);
            line.Location = new Point(55, lblTitle.Bottom - 5);
            parentPanel.Controls.Add(line);

            // Grid Control (Konumu başlığa göre ayarla - Yüksekliği azaltıldı)
            DevExpress.XtraGrid.GridControl grid = new DevExpress.XtraGrid.GridControl();
            grid.Location = new Point(50, 110);
            grid.Size = new Size(1100, 430);
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
            view.Appearance.HeaderPanel.BackColor = Color.FromArgb(255, 107, 53); // #FF6B35 Orange
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
                    string checkPath = System.IO.Path.Combine(currentDir, "Resources", "su_vet_admin_global_bg.png");
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

            // Sayfa Başlığı (Premium Stil)
            Label lblPageTitle = new Label();
            lblPageTitle.Text = title.ToUpper(); // Parametreden gelen başlığı kullan
            lblPageTitle.Font = new Font("Segoe UI Black", 28F, FontStyle.Bold);
            lblPageTitle.ForeColor = Color.FromArgb(233, 163, 116); // #E9A374
            lblPageTitle.BackColor = Color.Transparent;
            lblPageTitle.AutoSize = true;
            lblPageTitle.Location = new Point(50, 20); 
            pnlHeader.Controls.Add(lblPageTitle);

            // Alt Süsleme Çizgisi
            Label line = new Label();
            line.BackColor = Color.FromArgb(233, 163, 116); // #E9A374
            line.Size = new Size(100, 5);
            line.Location = new Point(55, lblPageTitle.Bottom - 5);
            pnlHeader.Controls.Add(line);

            // 3. İçerik Taşıyıcı (Padding ile Ortalamayı Sağlar)
            Panel pnlBody = new Panel();
            pnlBody.Dock = DockStyle.Fill;
            pnlBody.BackColor = Color.Transparent;
            // KENARLARDAN BOŞLUK BIRAKARAK ORTALIYORUZ (ALTI KIRPILDI)
            pnlBody.Padding = new Padding(150, 10, 150, 180); 
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
            view.Appearance.HeaderPanel.BackColor = Color.FromArgb(255, 107, 53); // #FF6B35 Orange
            view.Appearance.HeaderPanel.ForeColor = Color.White;
            view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
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
            view.Appearance.EvenRow.BackColor = Color.FromArgb(255, 248, 245);
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

        private void ApplyThemeToElement(DevExpress.XtraBars.Navigation.AccordionControlElement el, Color themeColor)
        {
            el.Appearance.Normal.ForeColor = themeColor;
            el.Appearance.Normal.Options.UseForeColor = true;
            
            // Alt öğeler varsa onlara da (Recursive)
            foreach (DevExpress.XtraBars.Navigation.AccordionControlElement subEl in el.Elements)
            {
                ApplyThemeToElement(subEl, themeColor);
            }
        }
    }


}
