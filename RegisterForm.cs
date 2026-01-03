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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;



namespace VetClinic.UI1
{
    public partial class RegisterForm : DevExpress.XtraEditors.XtraForm
    {
        public RegisterForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(1200, 675);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Arka plan resmini yükle
            string bgPath = GetImagePath("login_main_bg.jpg");
            if (!string.IsNullOrEmpty(bgPath))
            {
                picBackground.Image = Image.FromFile(bgPath);
                picBackground.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            // --- ESTETİK PANEL TASARIMI (Glassmorphism with Tint) ---
            if (this.Controls.Contains(panelCenter)) this.Controls.Remove(panelCenter);
            picBackground.Controls.Add(panelCenter);

            panelCenter.BackColor = Color.White; // Solid white to cover artwork text fully
            panelCenter.Size = new Size(500, 600); // Larger to ensure coverage
            
            // Başlık Stili
            lblTitle.Text = "🐾 YENİ KAYIT";
            lblTitle.Font = new Font("Segoe UI Black", 26F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTitle.Location = new Point((panelCenter.Width - lblTitle.Width) / 2, 35);

            // Alt yazılar ve ikonlar
            StyleFieldHeader(lblAdSoyad, "👤 AD SOYAD", 100);
            StyleFieldHeader(lblEmail, "📧 KULLANICI ADI (E-POSTA)", 185);
            StyleFieldHeader(lblPass, "🔑 ŞİFRE", 270);
            StyleFieldHeader(lblPass2, "🔒 ŞİFRE TEKRAR", 355);

            // Input Alanları (Modern ve Temiz)
            StyleInput(txtRegName, 125);
            StyleInput(txtRegEmail, 210);
            StyleInput(txtRegPassword, 295);
            StyleInput(txtRegPassword2, 380);

            // KAYIT OL BUTONU (Vibrant and Premium)
            btnRegister.Text = "KAYIT OLMAYI TAMAMLA";
            btnRegister.Size = new Size(350, 55);
            btnRegister.Location = new Point(50, 460);
            btnRegister.Appearance.BackColor = Color.FromArgb(180, 60, 10); // Logo Turuncusu/Kahvesi
            btnRegister.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnRegister.Appearance.ForeColor = Color.White;
            btnRegister.Appearance.Options.UseBackColor = true;
            btnRegister.Appearance.Options.UseFont = true;
            btnRegister.Appearance.Options.UseForeColor = true;
            btnRegister.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            btnRegister.Cursor = Cursors.Hand;
            
            // Hover efekti
            btnRegister.MouseEnter += (s, e) => { btnRegister.Appearance.BackColor = Color.FromArgb(210, 80, 20); };
            btnRegister.MouseLeave += (s, e) => { btnRegister.Appearance.BackColor = Color.FromArgb(180, 60, 10); };

            // GERİ DÖN BUTONU (Minimalist)
            btnClose.Text = "← Vazgeç ve Giriş Ekranına Dön";
            btnClose.Size = new Size(350, 30);
            btnClose.Location = new Point(50, 530);
            btnClose.Appearance.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Underline);
            btnClose.Appearance.ForeColor = Color.DimGray;
            btnClose.Appearance.Options.UseFont = true;
            btnClose.Appearance.Options.UseForeColor = true;
            btnClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;

            this.Resize += (s, e) => CenterPanel();
            CenterPanel();
        }

        private void StyleFieldHeader(LabelControl lbl, string text, int y)
        {
            lbl.Text = text;
            lbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(100, 70, 40); // Kahve tonu
            lbl.Location = new Point(50, y);
        }

        private void StyleInput(TextEdit txt, int y)
        {
            txt.Size = new Size(350, 38);
            txt.Location = new Point(50, y);
            txt.Properties.Appearance.Font = new Font("Segoe UI", 12F);
            txt.Properties.Appearance.Options.UseFont = true;
            txt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
        }

        private void CenterPanel()
        {
            panelCenter.Location = new Point(
                (this.ClientSize.Width - panelCenter.Width) / 2,
                (this.ClientSize.Height - panelCenter.Height) / 2
            );
        }

        private string GetImagePath(string fileName)
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 5; i++)
            {
                string checkPath = System.IO.Path.Combine(currentDir, "Resources", fileName);
                if (System.IO.File.Exists(checkPath)) return checkPath;
                var parent = System.IO.Directory.GetParent(currentDir);
                if (parent == null) break;
                currentDir = parent.FullName;
            }
            return null;
        }



        private void btnRegister_Click(object sender, EventArgs e)
        {
            string adSoyad = txtRegName.Text.Trim();
            string email = txtRegEmail.Text.Trim();
            string pass1 = txtRegPassword.Text.Trim();
            string pass2 = txtRegPassword2.Text.Trim();

            if (adSoyad == "" || email == "" || pass1 == "" || pass2 == "")
            {
                MessageBox.Show("Tüm alanları doldurun");
                return;
            }

            if (pass1 != pass2)
            {
                MessageBox.Show("Şifreler uyuşmuyor");
                return;
            }

            try
            {
                // Müşteri kaydı - her zaman false
                KullaniciVeriYonetimi.KullaniciEkle(email, pass1, adSoyad, false);

                MessageBox.Show("✅ Kayıt başarılı!\n\nAynı email ve şifre ile giriş yapabilirsiniz.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt hatası:\n{ex.Message}\n\n{ex.InnerException?.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string RegisteredEmail { get; private set; }
        public string RegisteredPassword { get; private set; }

    }
}
