using B403Blog.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B403Blog.Controllers
{
    [Authorize]
    public class PanelKategoriController : Controller
    {
        BlogYazilimEntities2 db = new BlogYazilimEntities2();


        // GET: PanelKategori
        public ActionResult Index()
        {
            var model = db.Kategori.ToList();
            return View(model);
        }

        public ActionResult KategoriEkle()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Kategori ktg )
        {

            db.Kategori.Add(ktg);
            db.SaveChanges();
            return RedirectToAction("Index", "PanelKategori");
        }



        public ActionResult Sil(int id)
        {
            var silinecekkategori = db.Kategori.Find(id);
            if (silinecekkategori == null)
                return HttpNotFound();

            if (silinecekkategori.Makale.Count>0)
            {
                TempData["KategoriSilinemez"] = "Bu Kategoriye bağlı Makaleler bulunmaktadır!";
            }
            if (silinecekkategori.Makale.Count<0)
            {
                db.Kategori.Remove(silinecekkategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");



        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Kategori.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("PanelKategoriForm", model);

        }

        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Kategori kategori)
        {
            if (kategori.KategoriId == 0)
            {
                db.Kategori.Add(kategori);
            }
            else
            {
                db.Entry(kategori).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}