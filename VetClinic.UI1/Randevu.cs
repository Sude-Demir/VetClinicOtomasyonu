using System;

namespace VetClinic.UI1
{
    public class Randevu
    {
        public int Id { get; set; }
        public string Tur { get; set; } = "Muayene"; // Varsayılan
        public string Durum { get; set; } = "Bekliyor";
        public string HastaAd { get; set; }
        public string HastaSoyad { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public string Aciklama { get; set; }
        public string Hekim { get; set; }
    }

}
