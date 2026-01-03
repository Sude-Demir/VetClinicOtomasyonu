using System;
using System.Collections.Generic;
using System.Linq;
using VetClinic.UI1.Data;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.UI1
{
    public class KullaniciVeriYonetimi
    {
        public static bool Kaydet(List<Kullanici> kullaniciListesi)
        {
            try
            {
                using (var db = new VetClinicContext())
                {
                    db.Database.EnsureCreated();
                    
                    foreach (var kullanici in kullaniciListesi)
                    {
                        string cleanEmail = kullanici.Email.ToLower().Trim();
                        var k = db.Kullanicilar.FirstOrDefault(u => u.Email.ToLower() == cleanEmail);
                        if (k != null)
                        {
                            // 1. Kullanicilar Güncelle
                            k.Sifre = kullanici.Sifre;
                            k.AdSoyad = kullanici.AdSoyad;
                            k.TCKimlikNo = kullanici.TCKimlikNo;
                            k.DogumTarihi = kullanici.DogumTarihi;
                            k.Cinsiyet = kullanici.Cinsiyet;
                            k.SahipOlduguHayvan = kullanici.SahipOlduguHayvan;
                            k.HayvanAdi = kullanici.HayvanAdi;
                            k.HayvanTurCins = kullanici.HayvanTurCins;
                            k.HayvanYasi = kullanici.HayvanYasi;
                            k.HayvanMikrocip = kullanici.HayvanMikrocip;
                            k.HayvanSaglikNotu = kullanici.HayvanSaglikNotu;

                            // 2. Musteriler Güncelle (Eğer Admin değilse)
                            if (!k.IsAdmin)
                            {
                                var m = db.Musteriler.FirstOrDefault(mu => mu.Eposta.ToLower() == cleanEmail);
                                if (m != null)
                                {
                                    m.AdSoyad = kullanici.AdSoyad;
                                    // Telefon ve Hayvanlar alanı Kullanici modelinde yok, o yüzden dokunmuyoruz veya varsayılan bırakıyoruz
                                }
                                else
                                {
                                    db.Musteriler.Add(new Musteri
                                    {
                                        AdSoyad = kullanici.AdSoyad,
                                        Eposta = kullanici.Email,
                                        Telefon = "Girilmedi",
                                        Hayvanlar = kullanici.SahipOlduguHayvan ?? "Henüz Kayıtlı Hayvan Yok"
                                    });
                                }
                            }
                        }
                        else
                        {
                            // Yeni Kullanici ekle
                            db.Kullanicilar.Add(new KullaniciEntity
                            {
                                Email = kullanici.Email,
                                Sifre = kullanici.Sifre,
                                IsAdmin = kullanici.IsAdmin,
                                AdSoyad = kullanici.AdSoyad,
                                TCKimlikNo = kullanici.TCKimlikNo,
                                DogumTarihi = kullanici.DogumTarihi,
                                Cinsiyet = kullanici.Cinsiyet,
                                SahipOlduguHayvan = kullanici.SahipOlduguHayvan,
                                HayvanAdi = kullanici.HayvanAdi,
                                HayvanTurCins = kullanici.HayvanTurCins,
                                HayvanYasi = kullanici.HayvanYasi,
                                HayvanMikrocip = kullanici.HayvanMikrocip,
                                HayvanSaglikNotu = kullanici.HayvanSaglikNotu
                            });

                            // Yeni Musteri ekle (Eğer Admin değilse)
                            if (!kullanici.IsAdmin)
                            {
                                db.Musteriler.Add(new Musteri
                                {
                                    AdSoyad = kullanici.AdSoyad,
                                    Eposta = kullanici.Email,
                                    Telefon = "Girilmedi",
                                    Hayvanlar = kullanici.SahipOlduguHayvan ?? "Henüz Kayıtlı Hayvan Yok"
                                });
                            }
                        }
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Kullanıcı kaydetme hatası: " + ex.Message);
                return false;
            }
        }

        public static List<Kullanici> Yukle()
        {
            try
            {
                using (var db = new VetClinicContext())
                {
                    db.EnsureSeeded();
                    return db.Kullanicilar.AsNoTracking().Select(k => new Kullanici(k.Email, k.Sifre, k.AdSoyad, k.IsAdmin)
                    {
                        TCKimlikNo = k.TCKimlikNo,
                        DogumTarihi = k.DogumTarihi,
                        Cinsiyet = k.Cinsiyet,
                        SahipOlduguHayvan = k.SahipOlduguHayvan,
                        HayvanAdi = k.HayvanAdi,
                        HayvanTurCins = k.HayvanTurCins,
                        HayvanYasi = k.HayvanYasi,
                        HayvanMikrocip = k.HayvanMikrocip,
                        HayvanSaglikNotu = k.HayvanSaglikNotu
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Kullanıcı yükleme hatası: " + ex.Message);
                return new List<Kullanici>();
            }
        }

        public static void KullaniciEkle(string email, string sifre, string adSoyad, bool isAdmin)
        {
            try
            {
                using (var db = new VetClinicContext())
                {
                    // Email'in daha önce kaydedilip kaydedilmediğini kontrol et (Case-insensitive)
                    string cleanEmail = email.ToLower().Trim();
                    if (db.Kullanicilar.Any(k => k.Email.ToLower() == cleanEmail)) return;

                    // 1. Kullanicilar Tablosuna Ekle
                    db.Kullanicilar.Add(new KullaniciEntity
                    {
                        Email = email.Trim(),
                        Sifre = sifre.Trim(),
                        AdSoyad = adSoyad.Trim(),
                        IsAdmin = isAdmin
                    });

                    // 2. Eğer Müşteri ise Musteriler Tablosuna da Ekle (Yönetici Paneli listesi için)
                    if (!isAdmin)
                    {
                        if (!db.Musteriler.Any(m => m.Eposta.ToLower() == cleanEmail))
                        {
                            db.Musteriler.Add(new Musteri
                            {
                                AdSoyad = adSoyad.Trim(),
                                Eposta = email.Trim(),
                                Telefon = "Girilmedi",
                                Hayvanlar = "Henüz Kayıtlı Hayvan Yok"
                            });
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Kullanıcı ekleme hatası: " + ex.Message);
                throw; // Formda yakalanması için fırlat
            }
        }

        public static Kullanici GirisYap(string email, string sifre)
        {
            try
            {
                using (var db = new VetClinicContext())
                {
                    db.EnsureSeeded();
                    
                    string inputEmail = email.ToLower().Trim();
                    string inputSifre = sifre.Trim();

                    // Veritabanında ara
                    var k = db.Kullanicilar.AsNoTracking().FirstOrDefault(u => 
                        u.Email.ToLower() == inputEmail && u.Sifre == inputSifre);

                    if (k != null)
                    {
                        return new Kullanici(k.Email, k.Sifre, k.AdSoyad, k.IsAdmin)
                        {
                            TCKimlikNo = k.TCKimlikNo,
                            DogumTarihi = k.DogumTarihi,
                            Cinsiyet = k.Cinsiyet,
                            SahipOlduguHayvan = k.SahipOlduguHayvan,
                            HayvanAdi = k.HayvanAdi,
                            HayvanTurCins = k.HayvanTurCins,
                            HayvanYasi = k.HayvanYasi,
                            HayvanMikrocip = k.HayvanMikrocip,
                            HayvanSaglikNotu = k.HayvanSaglikNotu
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Giriş hatası: " + ex.Message);
            }
            return null;
        }
    }
}
