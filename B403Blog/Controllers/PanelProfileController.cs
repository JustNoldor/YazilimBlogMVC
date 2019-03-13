using B403Blog.App_Classes;
using B403Blog.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace B403Blog.Controllers
{
    [Authorize(Roles = "Admin,2,1")]
    public class PanelProfileController : Controller
    {
        BlogYazilimEntities2 db = new BlogYazilimEntities2();

        // GET: PanelProfile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProfilGor(int id, Kullanici klnc)
        {
            int yzrId = db.Kullanici.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name).KullaniciId;

            var son = db.Kullanici.FirstOrDefault(x => x.KullaniciId == yzrId);

            return View(son);
        }


        public ActionResult Guncelle(int id, HttpPostedFileBase resim, Kullanici klnc)
        {
            //Güncelle

            try
            {
                int yzrId = db.Kullanici.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name).KullaniciId;
                var model = db.Kullanici.Where(m => m.KullaniciId == yzrId).SingleOrDefault();

                string resimkonum = Request.MapPath(model.Resim.BuyukBoyut);

                if (resim != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(model.Resim.KucukBoyut)))
                    {
                        System.IO.File.Delete(Server.MapPath(model.Resim.KucukBoyut));
                        System.IO.File.Delete(Server.MapPath(model.Resim.OrtaBoyut));
                        System.IO.File.Delete(Server.MapPath(model.Resim.BuyukBoyut));
                    }

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

                    //Resimi ekleyip Kullanıcıya bağlı olan ResimID'yi değiştirir.
                    db.Resim.Add(rsm);
                    model.ResimID = rsm.ResimId;


                    model.KullaniciAdi = klnc.KullaniciAdi;
                    model.Parola = klnc.Parola;
                    model.Adi = klnc.Adi;
                    model.Soyadi = klnc.Soyadi;
                    model.MailAdres = klnc.MailAdres;
                    db.SaveChanges();

                    TempData["GuncellemeBasarili"] ="Profiliniz Başarıyla Güncellendi!" ;

                    FormsAuthentication.SignOut();
                    return RedirectToAction("Login","Security");
                }


                    return View("ProfileForm", model );



            }
            catch (Exception)
            {
                return View();
            }

        }
    }
}