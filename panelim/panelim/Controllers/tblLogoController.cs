using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using panelim.Models;

namespace mvcpanel.Controllers
{
    public class tblLogoController : Controller
    {
        private SiteDbEntities db = new SiteDbEntities();


        public ActionResult Create()
        {
            tblLogo tblLogo = db.tblLogo.FirstOrDefault();
            return View(tblLogo);
   
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LogoIcerik")] tblLogo tblLogo)
        { 

                tblLogo tblLogosu = db.tblLogo.Find(tblLogo.Id);
            if (tblLogo.LogoIcerik== null)
            {
                return View(tblLogo);
            }
                if (tblLogosu != null)
                {
                    tblLogosu.Id = tblLogo.Id;
                    tblLogosu.LogoIcerik = tblLogo.LogoIcerik;
                    db.Entry(tblLogosu).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["genelMesaj"] = "Güncellendi";
                    return RedirectToAction("Create");

                }
                else
                {
                db.tblLogo.Add(tblLogo);
                    db.SaveChanges();
                    TempData["genelMesaj"] = "Kaydoldu";
                     return RedirectToAction("Create");

            }

         
           
        }
        public ActionResult Delete(int id)
        {
            var silinceklogo = db.tblLogo.Find(id);
            if (silinceklogo == null)
            {
                return HttpNotFound();
            }
            db.tblLogo.Remove(silinceklogo);
            db.SaveChanges();
            TempData["genelMesaj"] = "Silindi";
            return RedirectToAction("Create");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
