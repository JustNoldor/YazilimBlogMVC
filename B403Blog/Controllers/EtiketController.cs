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
    public class EtiketController : Controller
    {
        BlogYazilimEntities1 db = new BlogYazilimEntities1();




        // GET: Etiket
        public ActionResult Index(int id)
        {
            return View(id);
        }
        public PartialViewResult EtiketlerWidget()
        {
            return PartialView(db.Etiket.ToList());
        }
        public ActionResult MakaleListele(int id)
        {
            var data = db.Makale.Where(x => x.Etiket.Any(y => y.EtiketId == id)).ToList();
            return View("MakaleListeleWidget", data);
        }
    }
}