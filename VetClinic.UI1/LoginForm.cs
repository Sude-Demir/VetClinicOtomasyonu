using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetClinic.UI1;

namespace VetClinic.UI1
{
    public partial class LoginForm : Form
    {
        public static bool AdminMi = false;
        public static string GirisYapanKullanici = "";
        public LoginForm()
        {
            InitializeComponent();

            panelLogin.BackColor = Color.White;
            panelLogin.BorderStyle = BorderStyle.FixedSingle;

        }

        private void picBackground_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (email == "" || password == "")
            {
                MessageBox.Show("E-mail ve şifre boş olamaz");
                return;
            }

            // GİRİŞ BAŞARILI → ANA EKRAN
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm frm = new RegisterForm();
            frm.ShowDialog();

            // Login ekranı AYNEN kalsın

        }

    }
}
