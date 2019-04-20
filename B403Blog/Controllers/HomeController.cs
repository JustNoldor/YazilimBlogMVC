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


        public ActionResult Index()
        {
            return View();
        }

        //Asıl Makale Listeleme Kısmı
        [OutputCache(Duration =120)]
        public ActionResult MakaleListele(int? page)
        {
            //var data = db.Makale.OrderByDescending(x => x.EklenmeTarihi).Skip(5).ToList().ToPagedList(page ?? 1,7);

            var data = db.Makale.OrderByDescending(x => x.EklenmeTarihi).Skip(5).ToList();
            var duzenlenmisdata = data.Where(x=>x.Aktif==true).ToPagedList(page ?? 1,7);
            return View("PostListele", duzenlenmisdata);
        }

        //Popüler Makaleler(En çok görüntülenen 3 makaleyi gösterir.)
        [OutputCache(Duration = 120)]
        public PartialViewResult PopulerMakalelerWidget()
        {
            var model = db.Makale.OrderByDescending(x => x.GoruntulenmeSayisi).Take(5).ToList();
            return PartialView(model);
        }


        //Üst Post kısmı
        [OutputCache(Duration = 120)]
        public PartialViewResult AnaPost()
        {
            var model = db.Makale.OrderByDescending(x => x.EklenmeTarihi).Skip(1).Take(4).ToList();
            return PartialView(model);
        }
        [OutputCache(Duration = 120)]
        public ActionResult AnaPostOne()
        {
            var model  = db.Makale.OrderByDescending(x => x.EklenmeTarihi).Take(1).ToList();
            return PartialView(model);
        }


        //Ticket Bildirim Widget
        public PartialViewResult TicketWidget()
        {
            var model = db.Ticket.OrderByDescending(x => x.TicketID).Take(3).ToList();
            return PartialView(model);
        }

        //Trend Makaleler(Son 1 haftada en çok görüntülenen 6 makaleyi listeler.)
        public ActionResult TrendMakaleler()
        {
            DateTime birhaftaonce = DateTime.Now.AddDays(-7);
            DateTime ikihaftaonce = DateTime.Now.AddDays(-14);
            var makalelersonbirhafta = db.Makale.Where(x => x.EklenmeTarihi > birhaftaonce).ToList();
            var makalelersonikihafta = db.Makale.Where(x => x.EklenmeTarihi > ikihaftaonce).ToList();

            var goruntulenen = makalelersonbirhafta.OrderByDescending(x => x.GoruntulenmeSayisi).Take(6).ToList();
            var goruntulenen2 = makalelersonikihafta.OrderByDescending(x => x.GoruntulenmeSayisi).Take(6).ToList();


            if (goruntulenen.Count()==0)
            {
                return PartialView(goruntulenen2);
            }
            else
            {
                return PartialView(goruntulenen);
            }

        }

        //Menü için Trend Kategoriler
        public ActionResult TrendKategoriler()
        {
            var encok = db.Kategori.OrderByDescending(x => x.Makale.Count).Take(7).ToList();
            return PartialView(encok);
        }

    }
}