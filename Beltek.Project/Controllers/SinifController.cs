using Beltek.Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Beltek.Project.Controllers
{
    public class SinifController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new OkulDbContext())
            {
                var lst = db.Siniflar.ToList();
                return View(lst);
            }
        }

        [HttpGet]
        public IActionResult SinifEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SinifEkle(Sinif s)
        {
            using (var db = new OkulDbContext())
            {
                db.Siniflar.Add(s);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult SinifDuzenle(int id)
        {
            using (var db = new OkulDbContext())
            {
                var sinif = db.Siniflar.Find(id);
                return View(sinif);
            }
        }

        [HttpPost]
        public IActionResult SinifDuzenle(Sinif sinif)
        {
            using (var db = new OkulDbContext())
            {
                var mevcutSinif = db.Siniflar.FirstOrDefault(s => s.SinifId == sinif.SinifId);

                if (mevcutSinif != null)
                {
                    mevcutSinif.SinifAdi = sinif.SinifAdi;
                    mevcutSinif.Kontenjan = sinif.Kontenjan;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return BadRequest("Sınıf bulunamadı!");
            }
        }

        public IActionResult SinifSil(int id)
        {
            using (var db = new OkulDbContext())
            {
                db.Siniflar.Remove(db.Siniflar.Find(id));
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
