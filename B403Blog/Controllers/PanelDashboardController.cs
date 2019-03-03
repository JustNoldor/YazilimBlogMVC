using B403Blog.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B403Blog.Controllers
{
    [Authorize]
    public class PanelDashboardController : Controller
    {
        BlogYazilimEntities1 db = new BlogYazilimEntities1();



        // GET: PanelDashboard
        public ActionResult Index()
        {
            var model = db.Makale.Count();
            return View(model);
        }
    }
}