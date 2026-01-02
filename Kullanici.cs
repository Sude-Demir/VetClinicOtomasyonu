using System;

namespace VetClinic.UI1
{
    [Serializable]
    public class Kullanici
    {
        public string Email { get; set; }
        public string Sifre { get; set; }
        public bool IsAdmin { get; set; }

        public Kullanici() { }

        public Kullanici(string email, string sifre, bool isAdmin)
        {
            Email = email;
            Sifre = sifre;
            IsAdmin = isAdmin;
        }
    }
}
