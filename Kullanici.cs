using System;

namespace VetClinic.UI1
{
    [Serializable]
    public class Kullanici
    {
        public string Email { get; set; }
        public string Sifre { get; set; }
        public string AdSoyad { get; set; }
        public string TCKimlikNo { get; set; }
        public string DogumTarihi { get; set; }
        public string Cinsiyet { get; set; }
        public string SahipOlduguHayvan { get; set; }
        public string HayvanAdi { get; set; }
        public string HayvanTurCins { get; set; }
        public string HayvanYasi { get; set; }
        public string HayvanMikrocip { get; set; }
        public string HayvanSaglikNotu { get; set; }
        public bool IsAdmin { get; set; }

        public Kullanici() { }

        public Kullanici(string email, string sifre, string adSoyad, bool isAdmin)
        {
            Email = email;
            Sifre = sifre;
            AdSoyad = adSoyad;
            IsAdmin = isAdmin;
        }
    }
}
