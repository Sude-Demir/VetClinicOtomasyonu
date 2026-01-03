using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace VetClinic.UI1
{
    public partial class FrmRoleSelection : XtraForm
    {
        private PictureBox picBackground;
        private SimpleButton btnAdmin;
        private SimpleButton btnCustomer;

        public FrmRoleSelection()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1200, 675); // 16:9 Aspect ratio

            picBackground = new PictureBox();
            picBackground.Dock = DockStyle.Fill;
            picBackground.SizeMode = PictureBoxSizeMode.StretchImage;
            
            string bgPath = GetImagePath("login_main_bg.jpg");
            if (!string.IsNullOrEmpty(bgPath))
            {
                picBackground.Image = Image.FromFile(bgPath);
            }
            this.Controls.Add(picBackground);

            // Admin Button
            btnAdmin = CreateTransparentButton();
            picBackground.Controls.Add(btnAdmin);
            btnAdmin.Click += (s, e) => {
                AdminLoginForm adminLogin = new AdminLoginForm();
                if (adminLogin.ShowDialog() == DialogResult.OK)
                {
                    this.Hide(); // Giriş başarılıysa seçim ekranını gizle
                }
            };

            // Customer Button
            btnCustomer = CreateTransparentButton();
            picBackground.Controls.Add(btnCustomer);
            btnCustomer.Click += (s, e) => {
                CustomerLoginForm customerLogin = new CustomerLoginForm();
                if (customerLogin.ShowDialog() == DialogResult.OK)
                {
                    this.Hide(); // Giriş başarılıysa seçim ekranını gizle
                }
            };

            // Exit Button (Small X at top right for convenience)
            SimpleButton btnExit = new SimpleButton();
            btnExit.Text = "X";
            btnExit.Size = new Size(30, 30);
            btnExit.Location = new Point(this.Width - 40, 10);
            btnExit.Appearance.BackColor = Color.Transparent;
            btnExit.Appearance.ForeColor = Color.White;
            btnExit.Appearance.Options.UseBackColor = true;
            btnExit.Appearance.Options.UseForeColor = true;
            btnExit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            btnExit.Click += (s, e) => Application.Exit();
            picBackground.Controls.Add(btnExit);

            this.Resize += FrmRoleSelection_Resize;
            
            // Masking unwanted artwork text (Şifremi Unuttum? ve Yardım)
            AddArtworkMasks();
            
            PositionButtons();
            PositionMasks(); // İlk konumlandırma
        }

        private void AddArtworkMasks()
        {
            // Tek bir buton ile yazıları kapatıyoruz
            SimpleButton btnSlogan = new SimpleButton();
            btnSlogan.Name = "btnSlogan";
            btnSlogan.Text = "🐾 Tek tıkla sağlıklı patiler.";
            btnSlogan.Appearance.BackColor = Color.FromArgb(242, 241, 236);
            btnSlogan.Appearance.ForeColor = Color.FromArgb(120, 80, 40);
            btnSlogan.Appearance.Font = new Font("Segoe UI", 11F, FontStyle.Italic);
            btnSlogan.Appearance.Options.UseBackColor = true;
            btnSlogan.Appearance.Options.UseForeColor = true;
            btnSlogan.Appearance.Options.UseFont = true;
            btnSlogan.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            btnSlogan.Cursor = Cursors.Default;
            picBackground.Controls.Add(btnSlogan);
        }

        private void PositionMasks()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            foreach (Control c in picBackground.Controls)
            {
                if (c.Name == "btnSlogan")
                {
                    // Tek buton, her iki yazıyı da kapatacak genişlikte
                    c.Size = new Size((int)(w * 0.30), (int)(h * 0.045));
                    c.Top = (int)(h * 0.720); // Yazıların üzerine gelecek şekilde yukarı taşındı
                    c.Left = (int)(w * 0.35); // Ortalanmış
                }
            }
        }

        private SimpleButton CreateTransparentButton()
        {
            SimpleButton btn = new SimpleButton();
            btn.Text = ""; // Text resimde var
            btn.Appearance.BackColor = Color.Transparent;
            btn.Appearance.Options.UseBackColor = true;
            btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            btn.Cursor = Cursors.Hand;
            
            // Hover effect
            btn.MouseEnter += (s, e) => {
                btn.Appearance.BackColor = Color.FromArgb(40, 255, 255, 255);
            };
            btn.MouseLeave += (s, e) => {
                btn.Appearance.BackColor = Color.Transparent;
            };
            
            return btn;
        }

        private void FrmRoleSelection_Resize(object sender, EventArgs e)
        {
            PositionButtons();
            PositionMasks();
        }

        private void PositionButtons()
        {
            if (btnAdmin == null || btnCustomer == null) return;

            // Bu koordinatlar görseldeki butonların yerlerine göre ayarlanmıştır (yaklaşık)
            // Görseldeki card merkezde. Butonlar bottom-middle'da.
            
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            // Yönetici Girişi (Soldaki Buton)
            btnAdmin.Left = (int)(w * 0.353);
            btnAdmin.Top = (int)(h * 0.584);
            btnAdmin.Width = (int)(w * 0.141);
            btnAdmin.Height = (int)(h * 0.116);

            // Müşteri Girişi (Sağdaki Buton)
            btnCustomer.Left = (int)(w * 0.506);
            btnCustomer.Top = (int)(h * 0.584);
            btnCustomer.Width = (int)(w * 0.141);
            btnCustomer.Height = (int)(h * 0.116);
        }

        private string GetImagePath(string fileName)
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 5; i++)
            {
                string checkPath = Path.Combine(currentDir, "Resources", fileName);
                if (File.Exists(checkPath)) return checkPath;
                var parent = Directory.GetParent(currentDir);
                if (parent == null) break;
                currentDir = parent.FullName;
            }
            return null;
        }
    }
}
