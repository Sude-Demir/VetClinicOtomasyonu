using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetClinic.UI1;
using System.Net.Mail;



namespace VetClinic.UI1
{
    public partial class RegisterForm : DevExpress.XtraEditors.XtraForm
    {
        public RegisterForm()
        {
            InitializeComponent();

            this.Resize += (s, e) => {
                panelCenter.Location = new Point(
                    (this.ClientSize.Width - panelCenter.Width) / 2,
                    (this.ClientSize.Height - panelCenter.Height) / 2
                );
            };
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string email = txtRegEmail.Text.Trim();
            string pass1 = txtRegPassword.Text.Trim();
            string pass2 = txtRegPassword2.Text.Trim();

            if (email == "" || pass1 == "" || pass2 == "")
            {
                MessageBox.Show("Tüm alanları doldurun");
                return;
            }

            if (pass1 != pass2)
            {
                MessageBox.Show("Şifreler uyuşmuyor");
                return;
            }

            // Kalıcı olarak kaydet
            KullaniciVeriYonetimi.KullaniciEkle(email, pass1, LoginForm.AdminMi);

            MessageBox.Show("Kayıt başarılı, giriş yapabilirsiniz");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public string RegisteredEmail { get; private set; }
        public string RegisteredPassword { get; private set; }

    }
}
