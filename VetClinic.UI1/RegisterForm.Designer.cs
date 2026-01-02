namespace VetClinic.UI1
{
    partial class RegisterForm
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
            this.panelCenter = new System.Windows.Forms.Panel();
            this.lblPass2 = new DevExpress.XtraEditors.LabelControl();
            this.lblPass = new DevExpress.XtraEditors.LabelControl();
            this.lblEmail = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.txtRegEmail = new DevExpress.XtraEditors.TextEdit();
            this.txtRegPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtRegPassword2 = new DevExpress.XtraEditors.TextEdit();
            this.btnRegister = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).BeginInit();
            this.panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegPassword2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // picBackground
            // 
            this.picBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBackground.Image = global::VetClinic.UI1.Properties.Resources.su_vet_bg_v3;
            this.picBackground.Location = new System.Drawing.Point(0, 0);
            this.picBackground.Name = "picBackground";
            this.picBackground.Size = new System.Drawing.Size(1000, 600);
            this.picBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBackground.TabIndex = 0;
            this.picBackground.TabStop = false;
            // 
            // panelCenter
            // 
            this.panelCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panelCenter.Controls.Add(this.lblPass2);
            this.panelCenter.Controls.Add(this.lblPass);
            this.panelCenter.Controls.Add(this.lblEmail);
            this.panelCenter.Controls.Add(this.lblTitle);
            this.panelCenter.Controls.Add(this.txtRegEmail);
            this.panelCenter.Controls.Add(this.txtRegPassword);
            this.panelCenter.Controls.Add(this.txtRegPassword2);
            this.panelCenter.Controls.Add(this.btnRegister);
            this.panelCenter.Controls.Add(this.btnClose);
            this.panelCenter.Location = new System.Drawing.Point(300, 75);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(400, 450);
            this.panelCenter.TabIndex = 1;
            // 
            // lblPass2
            // 
            this.lblPass2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPass2.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblPass2.Appearance.Options.UseFont = true;
            this.lblPass2.Appearance.Options.UseForeColor = true;
            this.lblPass2.Location = new System.Drawing.Point(50, 240);
            this.lblPass2.Name = "lblPass2";
            this.lblPass2.Size = new System.Drawing.Size(68, 15);
            this.lblPass2.TabIndex = 5;
            this.lblPass2.Text = "Şifre Tekrar";
            // 
            // lblPass
            // 
            this.lblPass.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPass.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblPass.Appearance.Options.UseFont = true;
            this.lblPass.Appearance.Options.UseForeColor = true;
            this.lblPass.Location = new System.Drawing.Point(50, 170);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(28, 15);
            this.lblPass.TabIndex = 3;
            this.lblPass.Text = "Şifre";
            // 
            // lblEmail
            // 
            this.lblEmail.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblEmail.Appearance.Options.UseFont = true;
            this.lblEmail.Appearance.Options.UseForeColor = true;
            this.lblEmail.Location = new System.Drawing.Point(50, 100);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(75, 15);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "Kullanıcı Adı";
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(100, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "YENİ KAYIT";
            // 
            // txtRegEmail
            // 
            this.txtRegEmail.Location = new System.Drawing.Point(50, 120);
            this.txtRegEmail.Name = "txtRegEmail";
            this.txtRegEmail.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtRegEmail.Properties.Appearance.Options.UseFont = true;
            this.txtRegEmail.Size = new System.Drawing.Size(300, 35);
            this.txtRegEmail.TabIndex = 2;
            // 
            // txtRegPassword
            // 
            this.txtRegPassword.Location = new System.Drawing.Point(50, 190);
            this.txtRegPassword.Name = "txtRegPassword";
            this.txtRegPassword.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtRegPassword.Properties.Appearance.Options.UseFont = true;
            this.txtRegPassword.Properties.PasswordChar = '●';
            this.txtRegPassword.Size = new System.Drawing.Size(300, 35);
            this.txtRegPassword.TabIndex = 4;
            // 
            // txtRegPassword2
            // 
            this.txtRegPassword2.Location = new System.Drawing.Point(50, 260);
            this.txtRegPassword2.Name = "txtRegPassword2";
            this.txtRegPassword2.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtRegPassword2.Properties.Appearance.Options.UseFont = true;
            this.txtRegPassword2.Properties.PasswordChar = '●';
            this.txtRegPassword2.Size = new System.Drawing.Size(300, 35);
            this.txtRegPassword2.TabIndex = 6;
            // 
            // btnRegister
            // 
            this.btnRegister.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnRegister.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRegister.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Appearance.Options.UseBackColor = true;
            this.btnRegister.Appearance.Options.UseFont = true;
            this.btnRegister.Appearance.Options.UseForeColor = true;
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.Location = new System.Drawing.Point(50, 330);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(300, 45);
            this.btnRegister.TabIndex = 7;
            this.btnRegister.Text = "KAYIT OL";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Underline);
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(50, 385);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(300, 30);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "İptal Et ve Geri Dön";
            this.btnClose.Click += new System.EventHandler((s, e) => this.Close());
            // 
            // RegisterForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.picBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Yeni Kayıt";
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).EndInit();
            this.panelCenter.ResumeLayout(false);
            this.panelCenter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRegPassword2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBackground;
        private System.Windows.Forms.Panel panelCenter;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.TextEdit txtRegEmail;
        private DevExpress.XtraEditors.TextEdit txtRegPassword;
        private DevExpress.XtraEditors.TextEdit txtRegPassword2;
        private DevExpress.XtraEditors.SimpleButton btnRegister;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl lblEmail;
        private DevExpress.XtraEditors.LabelControl lblPass;
        private DevExpress.XtraEditors.LabelControl lblPass2;
    }
}