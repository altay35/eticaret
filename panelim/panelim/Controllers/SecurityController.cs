using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using panelim.Models;

namespace YoneticiPanel.Controllers
{
    public class SecurityController : Controller
    {
        SiteDbEntities db = new SiteDbEntities();

        
        public ActionResult Login()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            var kullaniciInDb = db.Kullanici.FirstOrDefault(x => x.KullaniciAdi == kullanici.KullaniciAdi && x.Parola == kullanici.Parola);

            if (kullaniciInDb != null)
            {

        
                HttpCookie giris = new HttpCookie("giris");
                giris["userid"]= (kullaniciInDb.KullaniciId).ToString();
                Response.Cookies.Add(giris);
                 Response.Redirect(url:"/Home/Anasayfa");
                

            }
            else if (kullaniciInDb == null)
            ViewBag.color = "red";
            ViewBag.Mesaj = "Geçersiz kullanıcı adı veya şifre";
            return RedirectToAction("Login");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {

                db.Kullanici.Add(kullanici);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Response.Cookies["giris"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Login");
        }
    }
}