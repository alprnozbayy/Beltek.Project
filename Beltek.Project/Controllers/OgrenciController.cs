using Beltek.Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Beltek.Project.Controllers
{
    public class OgrenciController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new OkulDbContext())
            {
                var lstc = db.Siniflar.ToList();
                var lst = db.Ogrenciler.ToList();
                return View(lst);
            }
        }

        [HttpGet]
        public IActionResult OgrenciEkle()
        {
            using (var db = new OkulDbContext())
            {
                // Sınıf tablosunda sınıf olup olmadığını kontrol edin
                if (!db.Siniflar.Any())
                {
                    ViewBag.ErrorMessage = "Mevcut sınıf yok. Öğrenci eklemeden önce lütfen bir sınıf ekleyin.";
                    return View();
                }

                var lst = db.Ogrenciler.ToList();
                ViewBag.Siniflar = db.Siniflar.Select(s => new SelectListItem
                {
                    Value = s.SinifId.ToString(),
                    Text = s.SinifAdi
                }).ToList();
                return View();
            }
        }

        [HttpPost]
        public IActionResult OgrenciEkle(Ogrenci ogrenci)
        {
            using (var db = new OkulDbContext())
            {
                db.Ogrenciler.Add(ogrenci);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult OgrenciDuzenle(int id)
        {
            using (var db = new OkulDbContext())
            {
                var lst = db.Siniflar.ToList();
                ViewBag.Siniflar = db.Siniflar.Select(s => new SelectListItem
                {
                    Value = s.SinifId.ToString(),
                    Text = s.SinifAdi
                }).ToList();

                var ogrenci = db.Ogrenciler.Find(id);

                return View(ogrenci);
            }
        }

        [HttpPost]
        public IActionResult OgrenciDuzenle(Ogrenci ogrenci)
        {
            using (var db = new OkulDbContext())
            {

                var mevcutOgrenci = db.Ogrenciler.Include(s => s.Sinif).FirstOrDefault(s => s.OgrenciId == ogrenci.OgrenciId);
                if (mevcutOgrenci != null)
                {
                    mevcutOgrenci.Isim = ogrenci.Isim;
                    mevcutOgrenci.Soyisim = ogrenci.Soyisim;
                    mevcutOgrenci.Numara = ogrenci.Numara;
                    mevcutOgrenci.Sinif.SinifAdi = ogrenci.Sinif.SinifAdi;
                    mevcutOgrenci.Sinif.Kontenjan = ogrenci.Sinif.Kontenjan;

                    if (mevcutOgrenci.SinifId != ogrenci.Sinif.SinifId)
                    {
                        var yeniSinif = db.Siniflar.Find(ogrenci.Sinif.SinifId);
                        if (yeniSinif != null)
                        {
                            mevcutOgrenci.SinifId = yeniSinif.SinifId;
                            mevcutOgrenci.Sinif = yeniSinif;
                        }
                        else
                        {
                            return BadRequest("Geçersiz Sınıf ID'si.");
                        }
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return BadRequest("Öğrenci bulunamadı!");
            }
        }

        public IActionResult OgrenciSil(int id)
        {
            using (var db = new OkulDbContext())
            {
                db.Ogrenciler.Remove(db.Ogrenciler.Find(id));
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}