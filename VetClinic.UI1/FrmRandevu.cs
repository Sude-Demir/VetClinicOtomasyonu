using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VetClinic.UI1
{
    public partial class FrmRandevu : XtraForm
    {
        public static List<Randevu> RandevuListesi = new List<Randevu>();

        public FrmRandevu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FrmRandevu
            // 
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Name = "FrmRandevu";
            this.Text = "Randevu Oluştur";
            this.ResumeLayout(false);
        }
    }
}
