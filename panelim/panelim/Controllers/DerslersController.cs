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
    public class DerslersController : Controller
    {
        private SiteDbEntities db = new SiteDbEntities();

        // GET: Derslers
        public ActionResult Index()
        {
            var dersler = db.Dersler.Include(d => d.Kurs);
            return View(dersler.ToList());
        }

        // GET: Derslers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dersler dersler = db.Dersler.Find(id);
            if (dersler == null)
            {
                return HttpNotFound();
            }
            return View(dersler);
        }

        // GET: Derslers/Create
        public ActionResult Create()
        {
            ViewBag.KursId = new SelectList(db.Kurs, "KursId", "KursAdi");
            return View();
        }

        // POST: Derslers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DersId,DersAdi,DersIcerik,DersVideo,KursId")] Dersler dersler)
        {
            if (ModelState.IsValid)
            {
                db.Dersler.Add(dersler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KursId = new SelectList(db.Kurs, "KursId", "KursAdi", dersler.KursId);
            return View(dersler);
        }

        // GET: Derslers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dersler dersler = db.Dersler.Find(id);
            if (dersler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KursId = new SelectList(db.Kurs, "KursId", "KursAdi", dersler.KursId);
            return View(dersler);
        }

        // POST: Derslers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DersId,DersAdi,DersIcerik,DersVideo,KursId")] Dersler dersler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dersler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KursId = new SelectList(db.Kurs, "KursId", "KursAdi", dersler.KursId);
            return View(dersler);
        }

        // GET: Derslers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dersler dersler = db.Dersler.Find(id);
            if (dersler == null)
            {
                return HttpNotFound();
            }
            return View(dersler);
        }

        // POST: Derslers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dersler dersler = db.Dersler.Find(id);
            db.Dersler.Remove(dersler);
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
