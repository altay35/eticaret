using panelim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace panelim.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            if (genelIslem.loginOlmusmu() == 0)
            {

                return RedirectToAction("Login", "Security");
            }




            return View();
        }

        //protected override void HandleUnknownAction(string actionName)
        //{

        //    this.View(actionName).ExecuteResult(this.ControllerContext);

        //}

        public ActionResult Temp1()
        {
            return View();
        }
        public ActionResult Anasayfa()
        {
            return View();
        }

        public ActionResult Kurslar()
        {

            return View();
        }
        public ActionResult Projeler()
        {

            return View();
        }
        public ActionResult Vizefinal()
        {

            return View();
        }
        public ActionResult Iletisim()
        {
            return View();
        }

        public ActionResult KursDetay(int id)
        {
            if (genelIslem.loginOlmusmu() == 0)
            {

                return RedirectToAction("Login", "Security");
            }
            int kursid = id;
            ViewBag.id = kursid;
            return View();
      
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
    }
}