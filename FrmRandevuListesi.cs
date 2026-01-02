using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VetClinic.UI1
{
    public partial class FrmRandevuListesi : XtraForm
    {
        public FrmRandevuListesi()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FrmRandevuListesi
            // 
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Name = "FrmRandevuListesi";
            this.Text = "Randevu Listesi";
            this.ResumeLayout(false);
        }
    }
}
