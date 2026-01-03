using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VetClinic.UI1
{
    public partial class CustomerLoginForm : XtraForm
    {
        public CustomerLoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Müşteri Girişi";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            
            this.Appearance.BackColor = Color.FromArgb(245, 250, 248);
            
            lblTitle.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(220, 140, 30);
            
            StyleButton(btnLogin, Color.FromArgb(220, 140, 30));
            StyleButton(btnRegister, Color.FromArgb(60, 120, 60));
            StyleButton(btnBack, Color.Gray);
        }

        private void StyleButton(SimpleButton btn, Color backColor)
        {
            btn.Appearance.BackColor = backColor;
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Options.UseFont = true;
            btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            btn.Cursor = Cursors.Hand;
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

            try
            {
                var kullanici = KullaniciVeriYonetimi.GirisYap(user, pass);

                if (kullanici != null)
                {
                    LoginForm.GirisYapanKullanici = kullanici.Email;
                    LoginForm.AdminMi = kullanici.IsAdmin;
                    
                    this.DialogResult = DialogResult.OK;
                    CustomerPanelForm customerPanel = new CustomerPanelForm();
                    customerPanel.Show();
                    this.Hide();
                }
                else
                {
                    XtraMessageBox.Show("Hatalı kullanıcı adı veya şifre! Lütfen bilgilerinizi kontrol edin.", 
                        "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Sistemsel bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            LoginForm.AdminMi = false; // RegisterForm'un doğru rolü anlaması için
            using (RegisterForm regForm = new RegisterForm())
            {
                if (regForm.ShowDialog(this) == DialogResult.OK)
                {
                    // Kayıt başarılı.
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
