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
        BlogYazilimEntities2 db = new BlogYazilimEntities2();



        // GET: PanelDashboard
        public ActionResult Index()
        {
            // var model = db.Makale.Count();
            // return View();/*model*/
            BlogYazilimEntities2 db = new BlogYazilimEntities2();
            ViewModels.DashboardVM vm = new ViewModels.DashboardVM();
            vm.Makale = db.Makale.ToList();
            vm.Kategori = db.Kategori.ToList();
            vm.Kullanici = db.Kullanici.ToList();
            vm.MakaleSon = db.Makale.OrderByDescending(x => x.EklenmeTarihi).Take(1).ToList();
            return View(vm);
        }
    }
}