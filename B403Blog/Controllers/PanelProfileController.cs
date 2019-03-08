using B403Blog.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B403Blog.Controllers
{
    public class PanelProfileController : Controller
    {
        BlogYazilimEntities2 db = new BlogYazilimEntities2();

        // GET: PanelProfile
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProfilGor(int id)
        {
            var data = db.Kullanici.FirstOrDefault(x => x.KullaniciId == id);
            return View(data);
        }
    }
}