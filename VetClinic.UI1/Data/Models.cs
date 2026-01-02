using System.ComponentModel.DataAnnotations;

namespace VetClinic.UI1
{
    public class Personel
    {
        [Key]
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Gorev { get; set; }
        public string Uzmanlik { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public string CalismaGunleri { get; set; }
        public string Yetki { get; set; }
        public string CalismaSaatleri { get; set; }
        public string Durum { get; set; }
    }

    public class Musteri
    {
        [Key]
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public string Hayvanlar { get; set; }
    }

    public class HastaDetay
    {
        [Key]
        public int Id { get; set; }
        public string HayvanAdi { get; set; }
        public string Tur { get; set; }
        public string Cins { get; set; }
        public string Yas { get; set; }
        public string Cinsiyet { get; set; }
        public string Sahibi { get; set; }
    }

    public class TedaviKaydi
    {
        [Key]
        public int Id { get; set; }
        public string Tarih { get; set; }
        public string HastaAdi { get; set; }
        public string Tur { get; set; }
        public string Sahip { get; set; }
        public string Sikayet { get; set; }
        public string Islem { get; set; }
        public string Hekim { get; set; }
    }


}
