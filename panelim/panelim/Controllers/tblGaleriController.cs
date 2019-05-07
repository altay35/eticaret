
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using panelim.Models;

namespace panelim.Controllers
{
    public class tblGaleriController : Controller
    {
        private SiteDbEntities db = new SiteDbEntities();

       
        public ActionResult Index()
        {
            if (genelIslem.loginOlmusmu() == 0)
            {

                return RedirectToAction("Login", "Security");
            }


            return View(db.tblGaleri.ToList());
        }
        public ActionResult Create()
        {
            //var tipi = new SelectList()(List < SelectListItem >
            //{
            //    new SelectListItem { Text = "Full Width", Value = "Full Width" },
            //    new SelectListItem { Text = "Content Galeri", Value = "Content Galeri" },
            //    new SelectListItem { Text = "Slider", Value = "Slider" },
            //},"Value");

            //ViewBag.gtipi = tipi;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,g_adi,g_tipi")] tblGaleri tblGaleri)
        {
            if (ModelState.IsValid)
            {
             
                db.tblGaleri.Add(tblGaleri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        
            return View(tblGaleri);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult galeri(tblGaleri tblGaleri,string gtipi = "", string gadi = "")
        //{
        //    if (gtipi == "" || gadi == "")
        //    {
        //        //TempData["genelMesaj"] = "Kayıt Başarısız";
        //        return Json(false, JsonRequestBehavior.AllowGet);

        //    }


        //    tblGaleri.g_adi = gadi;
        //    tblGaleri.g_tipi = gtipi;
        //    tblGaleri.temp_id = Convert.ToInt32(Session["TempId"]);
        //    db.tblGaleri.Add(tblGaleri);
        //    db.SaveChanges();
        //    return Json(true, JsonRequestBehavior.AllowGet);


        //}

        public ActionResult Edit(int? id, tblGaleriResim galeriResim)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGaleri tblGaleri = db.tblGaleri.Find(id);
            if (tblGaleri == null)
            {
                return HttpNotFound();
            }
            var gresim = db.tblGaleriResim.Where(x=>x.Galeri_Id==tblGaleri.Id).ToList();

            ViewBag.gresim = gresim;
            return View(tblGaleri);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,g_adi,g_tipi,aktif,sira,temp_id")] tblGaleri tblGaleri, tblGaleriResim galeriResim)
        {
            if (ModelState.IsValid)
            {
              
            
                    db.Entry(tblGaleri).State = EntityState.Modified;
                    db.SaveChanges();
                TempData["genelMesaj"] = "Kaydoldu";

                    return RedirectToAction("/Edit/"+ tblGaleri.Id);
 
                
            }
            TempData["genelMesaj"] = "yapılamadı";
            return View(tblGaleri);
        }


        public ActionResult Delete(int id)
        {
            var silinicekgaleri = db.tblGaleri.Find(id);
            List<tblGaleriResim> tblgaleri = db.tblGaleriResim.Where(x => x.Galeri_Id == id).ToList();
            foreach (var item in tblgaleri)
            {
                db.tblGaleriResim.Remove(item);
                System.IO.File.Delete(Server.MapPath(item.bresim));
                System.IO.File.Delete(Server.MapPath(item.kresim));
                db.SaveChanges();
            }
            List<Sayfalar> sayfa = db.Sayfalar.Where(x => x.GaleriId == id).ToList();
            foreach (var item in sayfa)
            {
                item.GaleriId = null;
                db.SaveChanges();
            }
            if (silinicekgaleri == null )
            {
                return HttpNotFound();
            }
            db.tblGaleri.Remove(silinicekgaleri);
            db.SaveChanges();
            return RedirectToAction("Index");
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
