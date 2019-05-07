using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using panelim.Models;

namespace panelim.Controllers
{
    public class tblFormIcerikController : Controller
    {
        private SiteDbEntities db = new SiteDbEntities();



    
        public ActionResult Create()
        {
            ViewBag.FormId = new SelectList(db.tblForm, "Id", "FormAdi");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FormId,elementadi,elementtipi,sira,aktiflik")] tblFormIcerik tblFormIcerik)
        {
            if (ModelState.IsValid)
            {

                tblFormIcerik.FormId = tblFormIcerik.Id;
                tblFormIcerik.Id = Convert.ToInt32(null);
                 db.tblFormIcerik.Add(tblFormIcerik);
                db.SaveChanges();
                TempData["genelMesaj"] = "Eklendi";
                return RedirectToAction("Edit/" + tblFormIcerik.FormId, "tblForm");
            }

            return RedirectToAction("Edit/" + tblFormIcerik.FormId, "tblForm");
        }
        //// GET: tblFormIceriks/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblFormIcerik tblFormIcerik = db.tblFormIcerik.Find(id);
        //    if (tblFormIcerik == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(tblFormIcerik);
        //}

        //// POST: tblFormIceriks/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,FormId,elementadi,elementtipi,sira,aktiflik")] tblFormIcerik tblFormIcerik)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tblFormIcerik).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.FormId = new SelectList(db.tblForm, "id", "FormAdi", tblFormIcerik.FormId);
        //    return View(tblFormIcerik);
        //}
        [HttpPost]
        public JsonResult Guncelleme(string id)
        {

            int ids = Convert.ToInt32(id);
            var acikla = db.tblFormIcerik.Find(ids);
            tblFormIcerik tblFormIcerik = new tblFormIcerik();
            tblFormIcerik.Id = acikla.Id;
            tblFormIcerik.elementadi = acikla.elementadi;
            tblFormIcerik.elementtipi = acikla.elementtipi;
            tblFormIcerik.sira = acikla.sira;
            tblFormIcerik.aktiflik = acikla.aktiflik;
            return Json(tblFormIcerik, JsonRequestBehavior.AllowGet);
       



        }
        [HttpPost]
        public JsonResult GuncelKayit(string id,string elementadi,string elementtipi, string sira, string akt)
        {
            int Id = Convert.ToInt32(id);
            tblFormIcerik tblFormIcerik = db.tblFormIcerik.Find(Id);
            tblFormIcerik.elementadi = elementadi;
            tblFormIcerik.elementtipi = elementtipi;
            int sirasi = Convert.ToInt32(sira);
            tblFormIcerik.sira = sirasi;
            bool aktif = Convert.ToBoolean(akt);
            tblFormIcerik.aktiflik = aktif;
            db.Entry(tblFormIcerik).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);




        }
        public ActionResult Delete(int id)
        {
            var silinicekformıcer = db.tblFormIcerik.Find(id);
            if (silinicekformıcer == null)
            {
                return HttpNotFound();
            }
            db.tblFormIcerik.Remove(silinicekformıcer);
            db.SaveChanges();
            return RedirectToAction("Edit/" + silinicekformıcer.FormId, "tblForm");
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
