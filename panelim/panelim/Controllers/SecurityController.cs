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
                 Response.Redirect(url:"/Home/Index");
                

            }
            else if (kullaniciInDb == null)
            ViewBag.color = "red";
            ViewBag.Mesaj = "Geçersiz kullanıcı adı veya şifre";
            return RedirectToAction("Login");
        }
        [HttpPost]
        public JsonResult SiteLogin(Kullanici kullanici)
        {
            var kullaniciInDb = db.Kullanici.FirstOrDefault(x => x.KullaniciAdi == kullanici.KullaniciAdi && x.Parola == kullanici.Parola);

            if (kullaniciInDb != null)
            {
                string mail = kullaniciInDb.KullaniciAdi;
                return Json(mail, JsonRequestBehavior.AllowGet);

            }  

            return Json(false);
        }
        public ActionResult Logout()
        {
            Response.Cookies["giris"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Login");
        }
    }
}