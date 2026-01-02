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
            
            // Yan paneli sağda sabit tutmak için
            this.Resize += (s, e) => {
                panelSide.Location = new Point(this.ClientSize.Width - panelSide.Width, 0);
                panelSide.Height = this.ClientSize.Height;
            };
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
            lblUserIcon.Visible = true;
            lblPassIcon.Visible = true;
            lblUserTitle.Visible = true;
            lblPassTitle.Visible = true;

            txtUsername.Focus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Giriş alanlarını gizle
            lblLoginType.Visible = false;
            txtUsername.Visible = false;
            txtPassword.Visible = false;
            btnLogin.Visible = false;
            btnGoToRegister.Visible = false;
            btnBack.Visible = false;
            lblUserIcon.Visible = false;
            lblPassIcon.Visible = false;
            lblUserTitle.Visible = false;
            lblPassTitle.Visible = false;

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
