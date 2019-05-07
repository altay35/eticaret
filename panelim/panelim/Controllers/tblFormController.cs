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
    public class tblFormController : Controller
    {
        private SiteDbEntities db = new SiteDbEntities();

        // GET: tblForm
        public ActionResult Index()
        {
            if (genelIslem.loginOlmusmu() == 0)
            {

                return RedirectToAction("Login", "Security");
            }

   
            return View(db.tblForm.ToList());
        }

        // GET: tblForm/Create
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FormAdi")] tblForm tblForm)
        {
            if (ModelState.IsValid)
            {
       
                db.tblForm.Add(tblForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(tblForm);
        }

        // GET: tblForm/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblForm tblForm =db.tblForm.Find(id);
            ViewBag.FormAdi = tblForm.FormAdi;
            ViewBag.id = tblForm.FormAdi;
            if (tblForm == null)
            {
                return HttpNotFound();
            }
            var form = db.tblFormIcerik.Where(x => x.FormId == tblForm.id).ToList().OrderBy(x=>x.sira);
            ViewBag.Form = form;
            tblFormIcerik tblFormIcerik = new tblFormIcerik();
             int formum = db.tblFormIcerik.Where(x=>x.FormId== id).Count();
            tblFormIcerik.sira = formum+1;
            return View(tblFormIcerik);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,FormAdi")] tblForm tblForm)
        {
            if (ModelState.IsValid)
            {
              
                db.Entry(tblForm).State = EntityState.Modified;
                db.SaveChanges();
                TempData["genelMesaj"] = "Güncellendi";
                return RedirectToAction("Edit/" + tblForm.id);
            }
            return View(tblForm);
        }
        public ActionResult Delete(int id)
        {
            var silinicekform = db.tblForm.Find(id);
            if (silinicekform == null)
            {
                return HttpNotFound();
            }
            db.tblForm.Remove(silinicekform);
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
