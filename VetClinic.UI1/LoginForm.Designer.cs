namespace VetClinic.UI1
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelLogin = new System.Windows.Forms.Panel();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.Label();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.textEmail = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.Label();
            this.picBackground = new System.Windows.Forms.PictureBox();
            this.panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLogin
            // 
            this.panelLogin.BackColor = System.Drawing.SystemColors.Menu;
            this.panelLogin.Controls.Add(this.btnRegister);
            this.panelLogin.Controls.Add(this.btnLogin);
            this.panelLogin.Controls.Add(this.txtPassword);
            this.panelLogin.Controls.Add(this.textPassword);
            this.panelLogin.Controls.Add(this.textEmail);
            this.panelLogin.Controls.Add(this.txtEmail);
            this.panelLogin.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.panelLogin.Location = new System.Drawing.Point(359, 143);
            this.panelLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(254, 178);
            this.panelLogin.TabIndex = 1;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.Chocolate;
            this.btnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRegister.Location = new System.Drawing.Point(122, 95);
            this.btnRegister.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(64, 27);
            this.btnRegister.TabIndex = 8;
            this.btnRegister.Text = "KAYIT OL";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Chocolate;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnLogin.Location = new System.Drawing.Point(49, 95);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(68, 27);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "GİRİŞ YAP";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.AutoSize = true;
            this.txtPassword.BackColor = System.Drawing.Color.Chocolate;
            this.txtPassword.Font = new System.Drawing.Font("Franklin Gothic Medium", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.Location = new System.Drawing.Point(19, 49);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(39, 15);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.Text = "ŞİFRE";
            this.txtPassword.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(30, 65);
            this.textPassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(157, 20);
            this.textPassword.TabIndex = 5;
            this.textPassword.UseSystemPasswordChar = true;
            // 
            // textEmail
            // 
            this.textEmail.Location = new System.Drawing.Point(30, 28);
            this.textEmail.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textEmail.Name = "textEmail";
            this.textEmail.Size = new System.Drawing.Size(157, 20);
            this.textEmail.TabIndex = 4;
            // 
            // txtEmail
            // 
            this.txtEmail.AutoSize = true;
            this.txtEmail.BackColor = System.Drawing.Color.Chocolate;
            this.txtEmail.Font = new System.Drawing.Font("Franklin Gothic Medium", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtEmail.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtEmail.Location = new System.Drawing.Point(19, 12);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(88, 15);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.Text = "KULLANICI ADI";
            this.txtEmail.Click += new System.EventHandler(this.label1_Click);
            // 
            // picBackground
            // 
            this.picBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBackground.Image = global::VetClinic.UI1.Properties.Resources.Gemini_Generated_Image_pzyvk7pzyvk7pzyv;
            this.picBackground.Location = new System.Drawing.Point(0, 0);
            this.picBackground.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picBackground.Name = "picBackground";
            this.picBackground.Size = new System.Drawing.Size(684, 461);
            this.picBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBackground.TabIndex = 0;
            this.picBackground.TabStop = false;
            this.picBackground.Click += new System.EventHandler(this.picBackground_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.picBackground);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBackground)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBackground;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Label txtEmail;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.TextBox textEmail;
        private System.Windows.Forms.Label txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
    }
}