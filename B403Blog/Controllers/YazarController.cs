using B403Blog.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B403Blog.Controllers
{
    public class YazarController : Controller
    {
        BlogYazilimEntities2 db = new BlogYazilimEntities2();

        // GET: Yazar
        public ActionResult Index(int id)
        {
            return View(id);
        }

        public ActionResult KullaniciMakaleListele(int id)
        {
            var data = db.Makale.Where(x => x.YazarID == id).OrderByDescending(x => x.EklenmeTarihi).ToList();
            return View("MakaleListeleWidget", data);
        }
    }
}