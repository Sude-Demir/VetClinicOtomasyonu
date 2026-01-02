namespace VetClinic.UI1
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.picBackground = new System.Windows.Forms.PictureBox();
            this.panelSide = new System.Windows.Forms.Panel();
            this.lblPassTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblUserTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblLoginType = new DevExpress.XtraEditors.LabelControl();
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            this.btnGoToRegister = new DevExpress.XtraEditors.SimpleButton();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.lblPassIcon = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.lblUserIcon = new DevExpress.XtraEditors.LabelControl();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.lblWelcome = new DevExpress.XtraEditors.LabelControl();
            this.lblLine = new DevExpress.XtraEditors.LabelControl();
            this.btnAdminEntry = new DevExpress.XtraEditors.SimpleButton();
            this.btnCustomerEntry = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).BeginInit();
            this.panelSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // picBackground
            // 
            this.picBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBackground.Image = global::VetClinic.UI1.Properties.Resources.su_vet_bg_v3;
            this.picBackground.Location = new System.Drawing.Point(0, 0);
            this.picBackground.Name = "picBackground";
            this.picBackground.Size = new System.Drawing.Size(1100, 700);
            this.picBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBackground.TabIndex = 0;
            this.picBackground.TabStop = false;
            // 
            // panelSide
            // 
            this.panelSide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panelSide.Controls.Add(this.lblPassTitle);
            this.panelSide.Controls.Add(this.lblUserTitle);
            this.panelSide.Controls.Add(this.lblLoginType);
            this.panelSide.Controls.Add(this.btnBack);
            this.panelSide.Controls.Add(this.btnGoToRegister);
            this.panelSide.Controls.Add(this.btnLogin);
            this.panelSide.Controls.Add(this.lblPassIcon);
            this.panelSide.Controls.Add(this.txtPassword);
            this.panelSide.Controls.Add(this.lblUserIcon);
            this.panelSide.Controls.Add(this.txtUsername);
            this.panelSide.Controls.Add(this.lblWelcome);
            this.panelSide.Controls.Add(this.lblLine);
            this.panelSide.Controls.Add(this.btnAdminEntry);
            this.panelSide.Controls.Add(this.btnCustomerEntry);
            this.panelSide.Location = new System.Drawing.Point(650, 0);
            this.panelSide.Name = "panelSide";
            this.panelSide.Size = new System.Drawing.Size(450, 700);
            this.panelSide.TabIndex = 1;
            // 
            // lblPassTitle
            // 
            this.lblPassTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblPassTitle.Appearance.Options.UseFont = true;
            this.lblPassTitle.Appearance.Options.UseForeColor = true;
            this.lblPassTitle.Location = new System.Drawing.Point(80, 370);
            this.lblPassTitle.Name = "lblPassTitle";
            this.lblPassTitle.Size = new System.Drawing.Size(28, 15);
            this.lblPassTitle.TabIndex = 13;
            this.lblPassTitle.Text = "Şifre";
            this.lblPassTitle.Visible = false;
            // 
            // lblUserTitle
            // 
            this.lblUserTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUserTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblUserTitle.Appearance.Options.UseFont = true;
            this.lblUserTitle.Appearance.Options.UseForeColor = true;
            this.lblUserTitle.Location = new System.Drawing.Point(80, 300);
            this.lblUserTitle.Name = "lblUserTitle";
            this.lblUserTitle.Size = new System.Drawing.Size(71, 15);
            this.lblUserTitle.TabIndex = 12;
            this.lblUserTitle.Text = "Kullanıcı Adı";
            this.lblUserTitle.Visible = false;
            // 
            // lblLoginType
            // 
            this.lblLoginType.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 16F);
            this.lblLoginType.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblLoginType.Appearance.Options.UseFont = true;
            this.lblLoginType.Appearance.Options.UseForeColor = true;
            this.lblLoginType.Appearance.Options.UseTextOptions = true;
            this.lblLoginType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblLoginType.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLoginType.Location = new System.Drawing.Point(0, 250);
            this.lblLoginType.Name = "lblLoginType";
            this.lblLoginType.Size = new System.Drawing.Size(450, 40);
            this.lblLoginType.TabIndex = 9;
            this.lblLoginType.Text = "Giriş";
            this.lblLoginType.Visible = false;
            // 
            // btnBack
            // 
            this.btnBack.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnBack.Appearance.Options.UseFont = true;
            this.btnBack.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnBack.Location = new System.Drawing.Point(10, 10);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(40, 40);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "←";
            this.btnBack.Visible = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnGoToRegister
            // 
            this.btnGoToRegister.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnGoToRegister.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Underline);
            this.btnGoToRegister.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnGoToRegister.Appearance.Options.UseBackColor = true;
            this.btnGoToRegister.Appearance.Options.UseFont = true;
            this.btnGoToRegister.Appearance.Options.UseForeColor = true;
            this.btnGoToRegister.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnGoToRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGoToRegister.Location = new System.Drawing.Point(50, 520);
            this.btnGoToRegister.Name = "btnGoToRegister";
            this.btnGoToRegister.Size = new System.Drawing.Size(350, 30);
            this.btnGoToRegister.TabIndex = 7;
            this.btnGoToRegister.Text = "Hesabınız yok mu? Kayıt Ol";
            this.btnGoToRegister.Visible = false;
            this.btnGoToRegister.Click += new System.EventHandler(this.btnGoToRegister_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnLogin.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Appearance.Options.UseBackColor = true;
            this.btnLogin.Appearance.Options.UseFont = true;
            this.btnLogin.Appearance.Options.UseForeColor = true;
            this.btnLogin.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Location = new System.Drawing.Point(50, 460);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(350, 50);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "GİRİŞ YAP";
            this.btnLogin.Visible = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblPassIcon
            // 
            this.lblPassIcon.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblPassIcon.Appearance.Options.UseFont = true;
            this.lblPassIcon.Location = new System.Drawing.Point(40, 390);
            this.lblPassIcon.Name = "lblPassIcon";
            this.lblPassIcon.Size = new System.Drawing.Size(18, 30);
            this.lblPassIcon.TabIndex = 11;
            this.lblPassIcon.Text = "🔒";
            this.lblPassIcon.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(80, 390);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.AutoHeight = false;
            this.txtPassword.Properties.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(320, 35);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.Visible = false;
            // 
            // lblUserIcon
            // 
            this.lblUserIcon.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblUserIcon.Appearance.Options.UseFont = true;
            this.lblUserIcon.Location = new System.Drawing.Point(40, 320);
            this.lblUserIcon.Name = "lblUserIcon";
            this.lblUserIcon.Size = new System.Drawing.Size(18, 30);
            this.lblUserIcon.TabIndex = 10;
            this.lblUserIcon.Text = "👤";
            this.lblUserIcon.Visible = false;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(80, 320);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtUsername.Properties.Appearance.Options.UseFont = true;
            this.txtUsername.Properties.AutoHeight = false;
            this.txtUsername.Size = new System.Drawing.Size(320, 35);
            this.txtUsername.TabIndex = 4;
            this.txtUsername.Visible = false;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Appearance.Font = new System.Drawing.Font("Segoe UI Black", 28F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblWelcome.Appearance.Options.UseFont = true;
            this.lblWelcome.Appearance.Options.UseForeColor = true;
            this.lblWelcome.Appearance.Options.UseTextOptions = true;
            this.lblWelcome.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblWelcome.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblWelcome.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblWelcome.Location = new System.Drawing.Point(0, 100);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(450, 120);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = "SU VETERİNER\nKLİNİĞİ";
            // 
            // lblLine
            // 
            this.lblLine.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblLine.Appearance.Options.UseBackColor = true;
            this.lblLine.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblLine.Location = new System.Drawing.Point(100, 230);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(250, 4);
            this.lblLine.TabIndex = 3;
            // 
            // btnAdminEntry
            // 
            this.btnAdminEntry.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnAdminEntry.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnAdminEntry.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnAdminEntry.Appearance.Options.UseBackColor = true;
            this.btnAdminEntry.Appearance.Options.UseFont = true;
            this.btnAdminEntry.Appearance.Options.UseForeColor = true;
            this.btnAdminEntry.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnAdminEntry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdminEntry.Location = new System.Drawing.Point(50, 320);
            this.btnAdminEntry.Name = "btnAdminEntry";
            this.btnAdminEntry.Size = new System.Drawing.Size(350, 60);
            this.btnAdminEntry.TabIndex = 0;
            this.btnAdminEntry.Text = "🛡️ YÖNETİCİ GİRİŞİ";
            this.btnAdminEntry.Click += new System.EventHandler(this.btnAdminEntry_Click);
            // 
            // btnCustomerEntry
            // 
            this.btnCustomerEntry.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnCustomerEntry.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnCustomerEntry.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnCustomerEntry.Appearance.Options.UseBackColor = true;
            this.btnCustomerEntry.Appearance.Options.UseFont = true;
            this.btnCustomerEntry.Appearance.Options.UseForeColor = true;
            this.btnCustomerEntry.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnCustomerEntry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCustomerEntry.Location = new System.Drawing.Point(50, 400);
            this.btnCustomerEntry.Name = "btnCustomerEntry";
            this.btnCustomerEntry.Size = new System.Drawing.Size(350, 60);
            this.btnCustomerEntry.TabIndex = 1;
            this.btnCustomerEntry.Text = "🐾 MÜŞTERİ GİRİŞİ";
            this.btnCustomerEntry.Click += new System.EventHandler(this.btnCustomerEntry_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.panelSide);
            this.Controls.Add(this.picBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SU VETERİNER KLİNİĞİ";
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).EndInit();
            this.panelSide.ResumeLayout(false);
            this.panelSide.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBackground;
        private System.Windows.Forms.Panel panelSide;
        private DevExpress.XtraEditors.SimpleButton btnAdminEntry;
        private DevExpress.XtraEditors.SimpleButton btnCustomerEntry;
        private DevExpress.XtraEditors.LabelControl lblWelcome;
        private DevExpress.XtraEditors.LabelControl lblLine;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.SimpleButton btnGoToRegister;
        private DevExpress.XtraEditors.SimpleButton btnBack;
        private DevExpress.XtraEditors.LabelControl lblLoginType;
        private DevExpress.XtraEditors.LabelControl lblUserIcon;
        private DevExpress.XtraEditors.LabelControl lblPassIcon;
        private DevExpress.XtraEditors.LabelControl lblUserTitle;
        private DevExpress.XtraEditors.LabelControl lblPassTitle;
    }
}