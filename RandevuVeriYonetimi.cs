using System;
using System.Collections.Generic;
using System.Linq;
using VetClinic.UI1.Data;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.UI1
{
    public static class RandevuVeriYonetimi
    {
        public static List<Randevu> Yukle()
        {
            try
            {
                using (var db = new VetClinicContext())
                {
                    db.EnsureSeeded();
                    return db.Randevular.AsNoTracking().Select(r => new Randevu
                    {
                        Id = r.Id,
                        Tur = r.Tur,
                        Durum = r.Durum,
                        HastaAd = r.HastaAd,
                        HastaSoyad = r.HastaSoyad,
                        RandevuTarihi = r.RandevuTarihi,
                        Aciklama = r.Aciklama,
                        Hekim = r.Hekim
                    }).ToList();
                }
            }
            catch
            {
                return new List<Randevu>();
            }
        }

        public static void Kaydet(List<Randevu> randevuListesi)
        {
            try
            {
                using (var db = new VetClinicContext())
                {
                    db.Database.EnsureCreated();
                    
                    foreach (var randevu in randevuListesi)
                    {
                        var rEntity = db.Randevular.FirstOrDefault(r => r.Id == randevu.Id);
                        if (rEntity != null)
                        {
                            rEntity.Tur = randevu.Tur;
                            rEntity.Durum = randevu.Durum;
                            rEntity.HastaAd = randevu.HastaAd;
                            rEntity.HastaSoyad = randevu.HastaSoyad;
                            rEntity.RandevuTarihi = randevu.RandevuTarihi;
                            rEntity.Aciklama = randevu.Aciklama;
                            rEntity.Hekim = randevu.Hekim;
                        }
                        else
                        {
                            db.Randevular.Add(new RandevuEntity
                            {
                                Tur = randevu.Tur,
                                Durum = randevu.Durum,
                                HastaAd = randevu.HastaAd,
                                HastaSoyad = randevu.HastaSoyad,
                                RandevuTarihi = randevu.RandevuTarihi,
                                Aciklama = randevu.Aciklama,
                                Hekim = randevu.Hekim
                            });
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Randevu kaydetme hatası: " + ex.Message);
            }
        }
    }
}
