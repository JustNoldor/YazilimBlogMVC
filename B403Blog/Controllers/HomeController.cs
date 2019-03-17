using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using B403Blog.Models.EntityFramework;
using B403Blog.Models;
using System.Data.Entity;
using System.Data;
using PagedList;
//using B403Blog.ViewModels;

namespace B403Blog.Controllers
{
    using Models;
    using App_Classes;
    public class HomeController : Controller
    {
        BlogYazilimEntities2 db = new BlogYazilimEntities2();


        // GET: Home

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AyrıListele()
        {
            var data = db.Makale.OrderByDescending(x => x.EklenmeTarihi).ToList();
            return View("PostListele", data);
        }


        //Asıl Makale Listeleme Kısmı
        public ActionResult MakaleListele(int? page)
        {
            var data = db.Makale.OrderByDescending(x => x.EklenmeTarihi).Skip(5).ToList().ToPagedList(page ?? 1,10);
            return View("PostListele", data);
        }
        
        public PartialViewResult PopulerMakalelerWidget()
        {
            var model = db.Makale.OrderByDescending(x => x.GoruntulenmeSayisi).Take(3).ToList();
            return PartialView(model);
        }


        //Üst Post kısmı
        public PartialViewResult AnaPost()
        {
            var model = db.Makale.OrderByDescending(x => x.EklenmeTarihi).Skip(1).Take(4).ToList();
            return PartialView(model);
        }
        public ActionResult AnaPostOne()
        {
            var model  = db.Makale.OrderByDescending(x => x.EklenmeTarihi).Take(1).ToList();
            return PartialView(model);
        }

        public PartialViewResult TicketWidget()
        {
            var model = db.Ticket.OrderByDescending(x => x.TicketID).Take(3).ToList();
            return PartialView(model);
        }

    }
}