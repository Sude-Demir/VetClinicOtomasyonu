using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VetClinic.UI1
{
    public partial class FrmRandevu : XtraForm
    {
        public static System.ComponentModel.BindingList<Randevu> RandevuListesi = new System.ComponentModel.BindingList<Randevu>()
        {
            new Randevu { Id = 1, Tur = "Muayene", Durum = "Onaylandı", HastaAd = "Pamuk", HastaSoyad = "Yılmaz", RandevuTarihi = new DateTime(2026, 01, 10, 10, 30, 0), Aciklama = "Genel sağlık kontrolü", Hekim = "Dr. Ahmet" },
            new Randevu { Id = 2, Tur = "Aşı", Durum = "Beklemede", HastaAd = "Boncuk", HastaSoyad = "Demir", RandevuTarihi = new DateTime(2026, 01, 10, 11, 00, 0), Aciklama = "Kuduz aşısı", Hekim = "Dr. Elif" },
            new Randevu { Id = 3, Tur = "Kontrol", Durum = "Tamamlandı", HastaAd = "Leo", HastaSoyad = "Kaya", RandevuTarihi = new DateTime(2026, 01, 09, 14, 00, 0), Aciklama = "Ameliyat sonrası kontrol", Hekim = "Dr. Mehmet" },
            new Randevu { Id = 4, Tur = "Acil", Durum = "Onaylandı", HastaAd = "Karabaş", HastaSoyad = "Çetin", RandevuTarihi = new DateTime(2026, 01, 10, 15, 45, 0), Aciklama = "İştahsızlık ve halsizlik", Hekim = "Dr. Ahmet" },
            new Randevu { Id = 5, Tur = "Muayene", Durum = "İptal Edildi", HastaAd = "Maviş", HastaSoyad = "Arslan", RandevuTarihi = new DateTime(2026, 01, 11, 09, 30, 0), Aciklama = "Sahip randevuyu iptal etti", Hekim = "Dr. Elif" }
        };

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
