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
    using B403Blog.App_Classes;
    using Models;
    using System.Drawing;

    [Authorize]
    public class MakaleController : Controller
    {
        BlogYazilimEntities1 db = new BlogYazilimEntities1();



        // GET: Makale

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Detay(int id)
        {
            var data = db.Makale.FirstOrDefault(x => x.MakaleId == id);
            return View(data);
        }

        [AllowAnonymous]
        public ActionResult DetaySingle(int id)
        {
            var data = db.Makale.FirstOrDefault(x => x.MakaleId == id);
            return View(data);
        }






    }
}