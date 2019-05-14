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
    public class yorumController : Controller
    {
        private SiteDbEntities db = new SiteDbEntities();

        // GET: yorum
        public ActionResult Index()
        {
            var yorum = db.yorum.Include(y => y.Kullanici).Include(y => y.Kurs);
            return View(yorum.ToList());
        }

        // GET: yorum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yorum yorum = db.yorum.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // GET: yorum/Create
        public ActionResult Create()
        {
            ViewBag.y_yapan = new SelectList(db.Kullanici, "KullaniciId", "KullaniciAdi");
            ViewBag.y_id = new SelectList(db.Kurs, "KursId", "KursAdi");
            return View();
        }
        [HttpPost]
        public JsonResult YorumYap(yorum yorum)
        {

                yorum.y_yapan = Convert.ToInt32(HttpContext.Request.Cookies["giris"].Values[0]);
                db.yorum.Add(yorum);
                db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
            

            

        }
        // POST: yorum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,y_yapan,y_icerik,y_secenek,y_id")] yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.yorum.Add(yorum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.y_yapan = new SelectList(db.Kullanici, "KullaniciId", "KullaniciAdi", yorum.y_yapan);
            ViewBag.y_id = new SelectList(db.Kurs, "KursId", "KursAdi", yorum.y_id);
            return View(yorum);
        }

        // GET: yorum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yorum yorum = db.yorum.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            ViewBag.y_yapan = new SelectList(db.Kullanici, "KullaniciId", "KullaniciAdi", yorum.y_yapan);
            ViewBag.y_id = new SelectList(db.Kurs, "KursId", "KursAdi", yorum.y_id);
            return View(yorum);
        }

        // POST: yorum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,y_yapan,y_icerik,y_secenek,y_id")] yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yorum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.y_yapan = new SelectList(db.Kullanici, "KullaniciId", "KullaniciAdi", yorum.y_yapan);
            ViewBag.y_id = new SelectList(db.Kurs, "KursId", "KursAdi", yorum.y_id);
            return View(yorum);
        }

        // GET: yorum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yorum yorum = db.yorum.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // POST: yorum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            yorum yorum = db.yorum.Find(id);
            db.yorum.Remove(yorum);
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
