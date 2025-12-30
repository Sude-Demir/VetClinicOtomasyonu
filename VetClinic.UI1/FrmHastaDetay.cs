using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VetClinic.UI1
{
    public partial class FrmHastaDetay : DevExpress.XtraEditors.XtraForm
    {
        public Hasta SeciliHasta { get; set; }

        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            if (SeciliHasta != null)
            {
                lblAdSoyad.Text = SeciliHasta.SahibiAd + " " + SeciliHasta.SahibiSoyad;
                lblTur.Text = SeciliHasta.Tur;
                lblCinsiyet.Text = SeciliHasta.Cinsiyet;
                lblSahip.Text = SeciliHasta.SahibiAd; 
                lblTelefon.Text = "Belirtilmedi"; // Designer'da olan kontrol
            }
        }

        private void lblTur_Click(object sender, EventArgs e)
        {

        }
    }
}
