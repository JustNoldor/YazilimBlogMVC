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
    using System.Text.RegularExpressions;
    public class MakaleController : Controller
    {
        BlogYazilimEntities2 db = new BlogYazilimEntities2();



        // GET: Makale

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

       
        [AllowAnonymous]
        [Route("Makaleler/{category}/{baslik}-{id}")]
        public ActionResult DetaySingle(int id,string baslik,string category)
        {
            ViewBag.Title = baslik;
            var data = db.Makale.FirstOrDefault(x => x.MakaleId == id);
            return View(data);
        }
        [AllowAnonymous]
        public ActionResult OkunmaArttir(int Makaleid)
        {
            var makale = db.Makale.Where(m => m.MakaleId == Makaleid).SingleOrDefault();
            makale.GoruntulenmeSayisi += 1;
            db.SaveChanges();
            return View();
        }

        //SEO URL Helper
       
        




    }
}