using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using B403Blog.Models.EntityFramework;
using B403Blog.Models;
using System.Data.Entity;
using System.Data;

namespace B403Blog.Controllers
{
    public class KategoriController : Controller
    {
        BlogYazilimEntities2 db = new BlogYazilimEntities2();



        // GET: Kategori
        public ActionResult Index(int id)
        {
            return View(id);
        }

        [OutputCache(Duration = 120)]
        public PartialViewResult KategoriWidget()
        {
            return PartialView(db.Kategori.ToList());
        }

        [OutputCache(Duration = 120)]
        public ActionResult MakaleListele(int id)
        {
            var data = db.Makale.Where(x => x.Kategori.KategoriId == id).OrderByDescending(x=>x.EklenmeTarihi).ToList();
            var duzenlenmiscategory = data.Where(x => x.Aktif == true).ToList();
            return View("MakaleListeleWidget", data);
        }


    }
}
