using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace VetClinic.UI1
{
    public class KullaniciVeriYonetimi
    {
        private static string dosyaYolu = "kullanicilar.xml";

        public static void Kaydet(List<Kullanici> kullaniciListesi)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Kullanici>));
                using (TextWriter writer = new StreamWriter(dosyaYolu))
                {
                    serializer.Serialize(writer, kullaniciListesi);
                }
            }
            catch (Exception)
            {
                // Sessizce geçebiliriz veya loglayabiliriz
            }
        }

        public static List<Kullanici> Yukle()
        {
            try
            {
                if (File.Exists(dosyaYolu))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Kullanici>));
                    using (TextReader reader = new StreamReader(dosyaYolu))
                    {
                        return (List<Kullanici>)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception)
            {
                // Dosya bozuksa boş liste dön
            }
            return new List<Kullanici>();
        }

        public static void KullaniciEkle(string email, string sifre, bool isAdmin)
        {
            var liste = Yukle();
            // Aynı email ile varsa ekleme (isteğe bağlı)
            if (liste.Exists(k => k.Email == email)) return;

            liste.Add(new Kullanici(email, sifre, isAdmin));
            Kaydet(liste);
        }

        public static Kullanici GirisYap(string email, string sifre)
        {
            var liste = Yukle();
            return liste.Find(k => k.Email == email && k.Sifre == sifre);
        }
    }
}
