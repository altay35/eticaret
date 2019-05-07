using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult Dersler()
        {
            return View();
        }
        public ActionResult VizeFinal()
        {
            return View();
        }
        public ActionResult Projeler()
        {
            return View();
        }
        public ActionResult NotHesaplama()
        {
            return View();
        }
        public ActionResult Iletisim()
        {
            return View();
        }
        public ActionResult KursDetay()
        {
            return View();
        }
    }
}