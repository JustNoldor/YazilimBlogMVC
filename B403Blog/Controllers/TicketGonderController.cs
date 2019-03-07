using B403Blog.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B403Blog.Controllers
{
    public class TicketGonderController : Controller
    {
        BlogYazilimEntities2 db = new BlogYazilimEntities2();

        // GET: TicketGonder
        public ActionResult Index()
        {
            var model = db.Ticket.ToList();
            return View(model);
        }

        public ActionResult TicketGonderEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Ticket tckt)
        {

            db.Ticket.Add(tckt);
            db.SaveChanges();
            return RedirectToAction("Index", "Panel");
        }

    }
}