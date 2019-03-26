using B403Blog.App_Classes;
using B403Blog.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B403Blog.Controllers
{
    [Authorize]
    public class PanelUyelerController : Controller
    {
        // Veritabanı Bağlantısı
        BlogYazilimEntities2 db = new BlogYazilimEntities2();




        public ActionResult Index()
        {
            var model = db.Kullanici.ToList();
            return View(model);
          
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Kullanici.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("PanelUyelerForm", model);

        }

        public ActionResult UyeEkle()
        {
            ViewBag.Kullanicilar = db.Rol.Take(2).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Kullanici klnc, HttpPostedFileBase resim,KullaniciRol roller,Rol asılrol)
        {
            Image img = Image.FromStream(resim.InputStream);
            Bitmap kckResim = new Bitmap(img, Settings.ResimKucukBoyut);
            Bitmap ortaResim = new Bitmap(img, Settings.ResimOrtaBoyut);
            Bitmap bykResim = new Bitmap(img, Settings.ResimBuyukBoyut);

            kckResim.Save(Server.MapPath("/Content/MakaleResim/KucukBoyut/" + resim.FileName));
            ortaResim.Save(Server.MapPath("/Content/MakaleResim/OrtaBoyut/" + resim.FileName));
            bykResim.Save(Server.MapPath("/Content/MakaleResim/BuyukBoyut/" + resim.FileName));

            Resim rsm = new Resim();

            rsm.BuyukBoyut = "/Content/MakaleResim/BuyukBoyut/" + resim.FileName;
            rsm.OrtaBoyut = "/Content/MakaleResim/OrtaBoyut/" + resim.FileName;
            rsm.KucukBoyut = "/Content/MakaleResim/KucukBoyut/" + resim.FileName;


            db.Resim.Add(rsm);
            db.SaveChanges();


            klnc.ResimID = rsm.ResimId;

            klnc.KullaniciId = roller.KullaniciID;
            asılrol.RollID = roller.RolID;



            klnc.KayitTarihi = DateTime.Now;

            db.Kullanici.Add(klnc);
            db.KullaniciRol.Add(roller);
            db.Rol.Add(asılrol);

            db.SaveChanges();
            return RedirectToAction("Index", "PanelUyeler");

        }

        public ActionResult Sil(int id )
        {
            var silinecekklnc = db.Kullanici.Find(id);

            //Kullanıcının bağlı olduğu rolü bulmak için;
            var userrol = db.KullaniciRol.Where(x => x.KullaniciID == id).SingleOrDefault();

            if (silinecekklnc == null)
                return HttpNotFound();
            else
            {
                //Kullanıcının bağlı olduğu makaleleri bul ve bir listeye ata.
                var baglipost = db.Makale.Where(x => x.YazarID == id).ToList();
                
                // Kullanıcı Makalelerinin bağlı olduğu Id'yi Adminin Id'sine tek tek atar(AdminId=1).
                foreach (var gonderi in baglipost)
                {
                    gonderi.YazarID = 1;
                }
                db.SaveChanges();

                //Kullanıcının bağlı olduğu rolü siliniyor.
                db.KullaniciRol.Remove(userrol);
                db.SaveChanges();

                //Kullanıcıyı siler.
                db.Kullanici.Remove(silinecekklnc);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
        } 


    }
}