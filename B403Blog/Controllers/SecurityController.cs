using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using B403Blog.Models.EntityFramework;
using B403Blog.Models;
using System.Data.Entity;
using System.Data;
using System.Web.Security;


namespace B403Blog.Controllers
{
    public class SecurityController : Controller
    {
        BlogYazilimEntities2 db = new BlogYazilimEntities2();




        // GET: Security
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.Message = TempData["Message"];

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Kullanici kullanici)
        {
            var kullaniciInDb = db.Kullanici.FirstOrDefault(x => x.KullaniciAdi == kullanici.KullaniciAdi && x.Parola == kullanici.Parola);
            if (kullaniciInDb != null)
            {
                FormsAuthentication.SetAuthCookie(kullaniciInDb.KullaniciAdi, false);
                return RedirectToAction("Index", "PanelDashboard");
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz Kullanıcı Adı veya Şifre";
                return View();
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }
    }
}