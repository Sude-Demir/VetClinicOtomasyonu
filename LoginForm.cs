using System;
using System.Drawing;
using System.Windows.Forms;

using DevExpress.XtraEditors;

namespace VetClinic.UI1
{
    public partial class LoginForm : XtraForm
    {
        public static bool AdminMi = false;
        public static string GirisYapanKullanici = "";
        

        public LoginForm()
        {
            InitializeComponent();
            
            // Arka planı ayarla
            string bgPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "new_login_bg.jpg");
            if (System.IO.File.Exists(bgPath))
            {
                picBackground.Image = Image.FromFile(bgPath);
                picBackground.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            // Merkezi cam paneli (glass card) ayarla
            panelSide.BackColor = Color.FromArgb(210, 255, 255, 255); // Daha saydam cam efekti
            panelSide.BorderStyle = BorderStyle.None;
            
            this.Resize += (s, e) => {
                panelSide.Location = new Point((this.ClientSize.Width - panelSide.Width) / 2, (this.ClientSize.Height - panelSide.Height) / 2);
            };
            
            // Başlangıçta ortala
            panelSide.Location = new Point((this.ClientSize.Width - panelSide.Width) / 2, (this.ClientSize.Height - panelSide.Height) / 2);

            // Buton Stilleri (Resimdeki gibi)
            StyleRoleButton(btnAdminEntry, Color.FromArgb(180, 60, 10), "🛡️ YÖNETİCİ GİRİŞİ");
            StyleRoleButton(btnCustomerEntry, Color.FromArgb(220, 140, 30), "🐾 MÜŞTERİ GİRİŞİ");
            
            // Logo ve Hoşgeldin yazısı stilleri
            lblWelcome.Text = "SU VETERİNER\nKLİNİĞİ";
            lblWelcome.Font = new Font("Segoe UI Black", 24F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(100, 70, 40);
            
            lblLoginType.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblLoginType.Text = "Hoş Geldiniz!\nLütfen Giriş Yapın.";
            lblLoginType.Visible = true;
            lblLoginType.Location = new Point(0, 180);
            
            lblLine.Visible = false; // Çizgiye gerek yok, resimde yok
        }

        private void StyleRoleButton(SimpleButton btn, Color backColor, string text)
        {
            btn.Text = text;
            btn.Appearance.BackColor = backColor;
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Options.UseFont = true;
            btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            btn.Cursor = Cursors.Hand;
            
            // Hover efektleri
            btn.MouseEnter += (s, e) => { btn.Appearance.BackColor = Color.FromArgb(Math.Min(255, backColor.R + 30), Math.Min(255, backColor.G + 30), Math.Min(255, backColor.B + 30)); };
            btn.MouseLeave += (s, e) => { btn.Appearance.BackColor = backColor; };
        }
        
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                // UI'ı ilk haline döndür
                btnBack_Click(null, null);
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
        }

        private void btnAdminEntry_Click(object sender, EventArgs e)
        {
            SetLoginUI(true);
        }

        private void btnCustomerEntry_Click(object sender, EventArgs e)
        {
            SetLoginUI(false);
        }

        private void SetLoginUI(bool isAdmin)
        {
            AdminMi = isAdmin;
            lblLoginType.Text = isAdmin ? "🛡️ Yönetici Girişi" : "🐾 Müşteri Girişi";
            lblLoginType.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            
            // Seçim butonlarını gizle
            btnAdminEntry.Visible = false;
            btnCustomerEntry.Visible = false;

            // Giriş alanlarını göster
            lblLoginType.Visible = true;
            txtUsername.Visible = true;
            txtPassword.Visible = true;
            btnLogin.Visible = true;
            btnGoToRegister.Visible = true;
            btnBack.Visible = true;
            lblUserTitle.Visible = true;
            lblPassTitle.Visible = true;

            txtUsername.Focus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Giriş alanlarını gizle
            txtUsername.Visible = false;
            txtPassword.Visible = false;
            btnLogin.Visible = false;
            btnGoToRegister.Visible = false;
            btnBack.Visible = false;
            lblUserTitle.Visible = false;
            lblPassTitle.Visible = false;

            // Ana yazıları geri getir
            lblLoginType.Text = "Hoş Geldiniz!\nLütfen Giriş Yapın.";
            lblLoginType.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblLoginType.Visible = true;

            // Seçim butonlarını göster
            btnAdminEntry.Visible = true;
            btnCustomerEntry.Visible = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                XtraMessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kalıcı veriden kullanıcıyı kontrol et
            var kullanici = KullaniciVeriYonetimi.GirisYap(user, pass);

            if (kullanici != null)
            {
                // Giriş yapanın tipini (admin/customer) doğrula veya uyar
                // Eğer yönetici girişindeyse ve kullanıcı admin değilse uyar (Veya direkt giriş yaptır)
                if (AdminMi && !kullanici.IsAdmin)
                {
                    XtraMessageBox.Show("Bu hesap yönetici yetkisine sahip değil!", "Yetki Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                GirisYapanKullanici = user;
                AdminMi = kullanici.IsAdmin; // Yetkiyi veritabanındaki değere göre güncelle
                OpenMainForm();
            }
            else
            {
                XtraMessageBox.Show("Hatalı kullanıcı adı veya şifre! Lütfen önce kayıt olun veya bilgilerinizi kontrol edin.", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGoToRegister_Click(object sender, EventArgs e)
        {
            using (RegisterForm regForm = new RegisterForm())
            {
                if (regForm.ShowDialog(this) == DialogResult.OK)
                {
                    // Kayıt başarılı, RegisterForm içinde dosyaya kaydedildi.
                    // Giriş alanlarını temizle (kullanıcı manuel girsin)
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                }
            }
        }

        private void OpenMainForm()
        {
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }
    }
}
