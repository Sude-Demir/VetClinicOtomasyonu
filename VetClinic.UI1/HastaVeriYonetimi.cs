using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace VetClinic.UI1
{
    public class HastaVeriYonetimi
    {
        private static string dosyaYolu = "hastalar.xml";

        public static void Kaydet(BindingList<Hasta> hastaListesi)
        {
            try
            {
                List<Hasta> list = new List<Hasta>(hastaListesi);
                XmlSerializer serializer = new XmlSerializer(typeof(List<Hasta>));
                using (TextWriter writer = new StreamWriter(dosyaYolu))
                {
                    serializer.Serialize(writer, list);
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıyı bilgilendirmeyebiliriz veya loglayabiliriz
                // MessageBox.Show("Kaydetme hatası: " + ex.Message);
            }
        }

        public static List<Hasta> Yukle()
        {
            try
            {
                if (File.Exists(dosyaYolu))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Hasta>));
                    using (TextReader reader = new StreamReader(dosyaYolu))
                    {
                        return (List<Hasta>)serializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                // Dosya bozuksa vs.
            }
            return new List<Hasta>();
        }
    }
}
