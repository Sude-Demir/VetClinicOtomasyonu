using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using VetClinic.UI1;

namespace VetClinic.UI1.Data
{
    public class VetClinicContext : DbContext
    {
        public DbSet<Personel> Personeller { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<HastaDetay> Hastalar { get; set; }
        public DbSet<TedaviKaydi> Tedaviler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=vetclinic.db");
        }

        public void EnsureSeeded()
        {
            this.Database.EnsureCreated();

            if (!Personeller.Any())
            {
                Personeller.AddRange(
                    new Personel { AdSoyad = "Dr. Ayşe Yılmaz", Gorev = "Veteriner Hekim", Uzmanlik = "İç Hastalıkları", Telefon = "0553 214 67 89", Eposta = "ayse.yilmaz@suvet.com", CalismaGunleri = "Pzt-Cum", Yetki = "Veteriner", CalismaSaatleri = "09:00 - 17:30", Durum = "Aktif" },
                    new Personel { AdSoyad = "Dr. Mehmet Karaca", Gorev = "Veteriner Hekim", Uzmanlik = "Cerrahi", Telefon = "0541 389 45 12", Eposta = "mehmet.karaca@suvet.com", CalismaGunleri = "Pzt-Cts", Yetki = "Veteriner", CalismaSaatleri = "09:00 - 18:00", Durum = "Aktif" },
                    new Personel { AdSoyad = "Elif Demir", Gorev = "Veteriner Asistanı", Uzmanlik = "Hasta Bakımı", Telefon = "0507 622 13 44", Eposta = "elif.demir@suvet.com", CalismaGunleri = "Pzt-Cum", Yetki = "Asistan", CalismaSaatleri = "08:30 - 17:30", Durum = "İzinde" },
                    new Personel { AdSoyad = "Zeynep Arslan", Gorev = "Klinik Sekreteri", Uzmanlik = "Randevu & Kayıt", Telefon = "0530 911 28 55", Eposta = "zeynep.arslan@suvet.com", CalismaGunleri = "Pzt-Cum", Yetki = "Sekreter", CalismaSaatleri = "08:00 - 18:00", Durum = "Aktif" },
                    new Personel { AdSoyad = "Ali Can Öztürk", Gorev = "Klinik Yöneticisi", Uzmanlik = "Operasyon & Finans", Telefon = "0551 406 70 99", Eposta = "ali.ozturk@suvet.com", CalismaGunleri = "Pzt-Cum", Yetki = "Admin", CalismaSaatleri = "Esnek", Durum = "Aktif" }
                );
            }

            if (!Musteriler.Any())
            {
                Musteriler.AddRange(
                    new Musteri { AdSoyad = "Sude Demir", Telefon = "0555 111 22 33", Eposta = "sude@gmail.com", Hayvanlar = "Luna (Kedi), Max (Köpek)" },
                    new Musteri { AdSoyad = "Burak Aydın", Telefon = "0544 222 33 44", Eposta = "burak@hotmail.com", Hayvanlar = "Rocky (Köpek)" },
                    new Musteri { AdSoyad = "Merve Çetin", Telefon = "0533 444 55 66", Eposta = "merve.c@outlook.com", Hayvanlar = "Pamuk (Kedi), Leo (Kedi)" },
                    new Musteri { AdSoyad = "Emre Yıldız", Telefon = "0532 999 88 77", Eposta = "emre_y@gmail.com", Hayvanlar = "Boncuk (Kuş)" },
                    new Musteri { AdSoyad = "Ayşe Nur Demir", Telefon = "0505 666 77 88", Eposta = "ayse.nur@yahoo.com", Hayvanlar = "Daisy (Köpek)" }
                );
            }

            if (!Hastalar.Any())
            {
                Hastalar.AddRange(
                    new HastaDetay { HayvanAdi = "Luna", Tur = "Kedi", Cins = "Scottish Fold", Yas = "2", Cinsiyet = "Dişi", Sahibi = "Sude Demir" },
                    new HastaDetay { HayvanAdi = "Max", Tur = "Köpek", Cins = "Golden Retriever", Yas = "4", Cinsiyet = "Erkek", Sahibi = "Sude Demir" },
                    new HastaDetay { HayvanAdi = "Rocky", Tur = "Köpek", Cins = "Pitbull", Yas = "3", Cinsiyet = "Erkek", Sahibi = "Burak Aydın" },
                    new HastaDetay { HayvanAdi = "Pamuk", Tur = "Kedi", Cins = "Tekir", Yas = "1", Cinsiyet = "Dişi", Sahibi = "Merve Çetin" },
                    new HastaDetay { HayvanAdi = "Leo", Tur = "Kedi", Cins = "British Shorthair", Yas = "5", Cinsiyet = "Erkek", Sahibi = "Merve Çetin" },
                    new HastaDetay { HayvanAdi = "Boncuk", Tur = "Kuş", Cins = "Muhabbet Kuşu", Yas = "2", Cinsiyet = "Dişi", Sahibi = "Emre Yıldız" },
                    new HastaDetay { HayvanAdi = "Daisy", Tur = "Köpek", Cins = "Poodle", Yas = "6", Cinsiyet = "Dişi", Sahibi = "Ayşe Nur Demir" }
                );
            }

            if (!Tedaviler.Any())
            {
                Tedaviler.AddRange(
                    new TedaviKaydi { Tarih = "30.12.2023", HastaAdi = "Luna", Tur = "Kedi", Sahip = "Sude Demir", Sikayet = "Yüksek Ateş / Halsizlik", Islem = "Muayene + Antibiyotik Enjeksiyonu", Hekim = "Dr. Ayşe Yılmaz" },
                    new TedaviKaydi { Tarih = "28.12.2023", HastaAdi = "Max", Tur = "Köpek", Sahip = "Sude Demir", Sikayet = "Rutin Kontrol", Islem = "İç-Dış Parazit Aşısı", Hekim = "Dr. Mehmet Karaca" },
                    new TedaviKaydi { Tarih = "25.12.2023", HastaAdi = "Rocky", Tur = "Köpek", Sahip = "Burak Aydın", Sikayet = "Pati Yaralanması", Islem = "Dikiş Atılması + Pansuman", Hekim = "Dr. Mehmet Karaca" },
                    new TedaviKaydi { Tarih = "20.12.2023", HastaAdi = "Pamuk", Tur = "Kedi", Sahip = "Merve Çetin", Sikayet = "Gözde Çapaklanma", Islem = "Göz Damlası Reçetesi", Hekim = "Dr. Ayşe Yılmaz" },
                    new TedaviKaydi { Tarih = "15.12.2023", HastaAdi = "Boncuk", Tur = "Kuş", Sahip = "Emre Yıldız", Sikayet = "Tüy Dökülmesi", Islem = "Vitamin Takviyesi", Hekim = "Dr. Ayşe Yılmaz" },
                    new TedaviKaydi { Tarih = "10.12.2023", HastaAdi = "Daisy", Tur = "Köpek", Sahip = "Ayşe Nur Demir", Sikayet = "Kusma ve İştahsızlık", Islem = "Serum Fizyolojik + Diet Mama", Hekim = "Dr. Mehmet Karaca" },
                    new TedaviKaydi { Tarih = "05.12.2023", HastaAdi = "Leo", Tur = "Kedi", Sahip = "Merve Çetin", Sikayet = "Check-up", Islem = "Kan Tahlili + Genel Muayene", Hekim = "Dr. Ayşe Yılmaz" }
                );
            }
            
            this.SaveChanges();
        }
    }
}
