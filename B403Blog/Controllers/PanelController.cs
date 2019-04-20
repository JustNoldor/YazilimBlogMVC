using B403Blog.App_Classes;
using B403Blog.Models.EntityFramework;
using Elmah;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace B403Blog.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {

        BlogYazilimEntities2 db = new BlogYazilimEntities2();


        public ActionResult Index()
        {
            var model1 = db.Makale.OrderByDescending(x=>x.EklenmeTarihi).ToList();
            var model = db.Makale.OrderByDescending(x => x.EklenmeTarihi).ToList();
            return View(model);
        }




        public ActionResult MakaleEkle()
        {
            ViewBag.Kategoriler = db.Kategori.ToList();
            ViewBag.Kategorisecim = new SelectList(db.Kategori, "KategoriId", "Adi");
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Ekle(Makale mkl, HttpPostedFileBase resim, Etiket etkt)
        {


            Image img = Image.FromStream(resim.InputStream);
            Bitmap kckResim = new Bitmap(img, Settings.ResimKucukBoyut);
            Bitmap ortaResim = new Bitmap(img, Settings.ResimOrtaBoyut);
            Bitmap bykResim = new Bitmap(img, Settings.ResimBuyukBoyut);
            string uzantitarih = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            var uzantiresim = System.IO.Path.GetExtension(resim.FileName);

            kckResim.Save(Server.MapPath("/Content/MakaleResim/KucukBoyut/" + resim.FileName+ uzantitarih+ uzantiresim));
            ortaResim.Save(Server.MapPath("/Content/MakaleResim/OrtaBoyut/" + resim.FileName + uzantitarih + uzantiresim));
            bykResim.Save(Server.MapPath("/Content/MakaleResim/BuyukBoyut/" + resim.FileName +  uzantitarih + uzantiresim));

            Resim rsm = new Resim();

            rsm.BuyukBoyut = "/Content/MakaleResim/BuyukBoyut/" + resim.FileName + uzantitarih + uzantiresim;
            rsm.OrtaBoyut = "/Content/MakaleResim/OrtaBoyut/" + resim.FileName + uzantitarih + uzantiresim;
            rsm.KucukBoyut = "/Content/MakaleResim/KucukBoyut/" + resim.FileName + uzantitarih + uzantiresim;

 
            db.Resim.Add(rsm);
            db.SaveChanges();
            mkl.ResimID = rsm.ResimId;
            mkl.EklenmeTarihi = DateTime.Now;
            mkl.GoruntulenmeSayisi = 0;
            mkl.Begeni = 0;
            int yzrId = db.Kullanici.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name).KullaniciId;
            mkl.YazarID = yzrId;
            db.Makale.Add(mkl);
            db.SaveChanges();
            return RedirectToAction("Index", "Panel");

        }


        public ActionResult Sil(int id)
        {
            var silinecekmakale = db.Makale.Find(id);
            if (silinecekmakale == null)
                return HttpNotFound();
            else { 
            db.Makale.Remove(silinecekmakale);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
        }


        [ValidateInput(false)]
        public ActionResult Guncelle(int id, HttpPostedFileBase resim,Makale mkl)
        {
            ViewBag.Kategorisecim = new SelectList(db.Kategori, "KategoriId", "Adi");

            //Güncelle
            try
            {
                var model = db.Makale.Where(m => m.MakaleId == id).SingleOrDefault();
                var eskiresim = db.Resim.Where(m => m.ResimId == model.ResimID).SingleOrDefault();

                ViewBag.kategoriId = new SelectList(db.Kategori, "KategoriID", "Adi");

                string resimkonum = Request.MapPath(model.Resim.BuyukBoyut);

                if (resim!=null)
                {
                    if (System.IO.File.Exists(Server.MapPath(model.Resim.KucukBoyut)))
                    {
                        System.IO.File.Delete(Server.MapPath(model.Resim.KucukBoyut));
                        System.IO.File.Delete(Server.MapPath(model.Resim.OrtaBoyut));
                        System.IO.File.Delete(Server.MapPath(model.Resim.BuyukBoyut ));
                    }

                    Image img = Image.FromStream(resim.InputStream);
                    Bitmap kckResim = new Bitmap(img, Settings.ResimKucukBoyut);
                    Bitmap ortaResim = new Bitmap(img, Settings.ResimOrtaBoyut);
                    Bitmap bykResim = new Bitmap(img, Settings.ResimBuyukBoyut);
                    string uzantitarih = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
                    var uzantiresim = System.IO.Path.GetExtension(resim.FileName);

                    kckResim.Save(Server.MapPath("/Content/MakaleResim/KucukBoyut/" + resim.FileName + uzantitarih + uzantiresim));
                    ortaResim.Save(Server.MapPath("/Content/MakaleResim/OrtaBoyut/" + resim.FileName + uzantitarih + uzantiresim));
                    bykResim.Save(Server.MapPath("/Content/MakaleResim/BuyukBoyut/" + resim.FileName + uzantitarih + uzantiresim));

                    Resim rsm = new Resim();

                    rsm.BuyukBoyut = "/Content/MakaleResim/BuyukBoyut/" + resim.FileName + uzantitarih + uzantiresim;
                    rsm.OrtaBoyut = "/Content/MakaleResim/OrtaBoyut/" + resim.FileName + uzantitarih + uzantiresim;
                    rsm.KucukBoyut = "/Content/MakaleResim/KucukBoyut/" + resim.FileName + uzantitarih + uzantiresim;

                    //Eski resimi siler
                    db.Resim.Remove(eskiresim);

                    //Resimi ekleyip Makaleye bağlı olan ResimID'yi değiştirir.
                    db.Resim.Add(rsm);
                    model.ResimID = rsm.ResimId;
                    model.Aktif = true;
                    model.Etiketler = mkl.Etiketler;
                    model.Aciklama = mkl.Aciklama;
                    model.Baslik = mkl.Baslik;
                    model.Icerik = mkl.Icerik;
                    model.KategoriID = mkl.KategoriID;
                    db.SaveChanges();

                }
              
                //return RedirectToAction("Guncelle",model);
                return View("PanelForm", model);

            }
            catch (Exception)
            {

                return View();
            }

        }

        [ValidateAntiForgeryToken]
        public ActionResult Kaydet(Makale makale, HttpPostedFileBase resim)
        {
            if (makale.MakaleId == 0)
            {
                db.Makale.Add(makale);
            }
            else
            {

                db.Entry(makale).State = System.Data.Entity.EntityState.Modified;

            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        


    }
}