namespace VetClinic.UI1
{
    partial class FrmHastaDetay
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
            this.lblAdSoyad = new System.Windows.Forms.Label();
            this.lblTur = new System.Windows.Forms.Label();
            this.lblCinsiyet = new System.Windows.Forms.Label();
            this.lblTelefon = new System.Windows.Forms.Label();
            this.lblSahip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblAdSoyad
            // 
            this.lblAdSoyad.AutoSize = true;
            this.lblAdSoyad.Location = new System.Drawing.Point(465, 124);
            this.lblAdSoyad.Name = "lblAdSoyad";
            this.lblAdSoyad.Size = new System.Drawing.Size(67, 16);
            this.lblAdSoyad.TabIndex = 0;
            this.lblAdSoyad.Text = "Ad Soyad";
            // 
            // lblTur
            // 
            this.lblTur.AutoSize = true;
            this.lblTur.Location = new System.Drawing.Point(427, 227);
            this.lblTur.Name = "lblTur";
            this.lblTur.Size = new System.Drawing.Size(27, 16);
            this.lblTur.TabIndex = 1;
            this.lblTur.Text = "Tür";
            this.lblTur.Click += new System.EventHandler(this.lblTur_Click);
            // 
            // lblCinsiyet
            // 
            this.lblCinsiyet.AutoSize = true;
            this.lblCinsiyet.Location = new System.Drawing.Point(573, 249);
            this.lblCinsiyet.Name = "lblCinsiyet";
            this.lblCinsiyet.Size = new System.Drawing.Size(54, 16);
            this.lblCinsiyet.TabIndex = 2;
            this.lblCinsiyet.Text = "Cinsiyet";
            // 
            // lblTelefon
            // 
            this.lblTelefon.AutoSize = true;
            this.lblTelefon.Location = new System.Drawing.Point(576, 363);
            this.lblTelefon.Name = "lblTelefon";
            this.lblTelefon.Size = new System.Drawing.Size(53, 16);
            this.lblTelefon.TabIndex = 3;
            this.lblTelefon.Text = "Telefon";
            // 
            // lblSahip
            // 
            this.lblSahip.AutoSize = true;
            this.lblSahip.Location = new System.Drawing.Point(426, 375);
            this.lblSahip.Name = "lblSahip";
            this.lblSahip.Size = new System.Drawing.Size(42, 16);
            this.lblSahip.TabIndex = 4;
            this.lblSahip.Text = "Sahip";
            // 
            // FrmHastaDetay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1674, 788);
            this.Controls.Add(this.lblSahip);
            this.Controls.Add(this.lblTelefon);
            this.Controls.Add(this.lblCinsiyet);
            this.Controls.Add(this.lblTur);
            this.Controls.Add(this.lblAdSoyad);
            this.Name = "FrmHastaDetay";
            this.Text = "FrmHastaDetay";
            this.Load += new System.EventHandler(this.FrmHastaDetay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAdSoyad;
        private System.Windows.Forms.Label lblTur;
        private System.Windows.Forms.Label lblCinsiyet;
        private System.Windows.Forms.Label lblTelefon;
        private System.Windows.Forms.Label lblSahip;
    }
}