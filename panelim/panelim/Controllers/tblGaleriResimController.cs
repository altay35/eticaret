using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using panelim.Models;

namespace panelim.Controllers
{
    public class tblGaleriResimController : Controller
    {
        private SiteDbEntities db = new SiteDbEntities();

        // GET: tblGaleriResim
        public ActionResult Index()
        {
            if (genelIslem.loginOlmusmu() == 0)
            {

                return RedirectToAction("Login", "Security");
            }
            int tempid = Convert.ToInt32(HttpContext.Request.Cookies["giris"].Values[1]);

            return View(db.tblGaleri.ToList());
        }

        // GET: tblGaleriResim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGaleriResim tblGaleriResim = db.tblGaleriResim.Find(id);
            if (tblGaleriResim == null)
            {
                return HttpNotFound();
            }
            return View(tblGaleriResim);
        }

        // GET: tblGaleriResim/Create
        public ActionResult Create()
        {
            ViewBag.Galeri_Id = new SelectList(db.tblGaleri, "Id", "g_adi");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Galeri_Id,kresim,bresim,aciklama")] tblGaleriResim galeriResim, tblGaleri tblGaleri, HttpPostedFileBase galeriimg)
        {

            if (galeriimg == null)
            {
                galeriResim.Galeri_Id = tblGaleri.Id;
                ViewBag.Error = "Resim seçiniz.";
                return RedirectToAction("Edit/" + galeriResim.Galeri_Id, "tblGaleri");
            }

            if (galeriimg.ContentLength > 0)
            {
                WebImage img = new WebImage(galeriimg.InputStream);
                FileInfo galeriimginfo = new FileInfo(galeriimg.FileName);

                string newgaleriimg = Guid.NewGuid().ToString() + galeriimginfo.Extension;

               
                img.Save("~/Templates/" + "/images/" + newgaleriimg);
                galeriResim.bresim = "/Templates/" + "/images/" + newgaleriimg;
                string resimimg = Guid.NewGuid().ToString() + galeriimginfo.Extension;
                img.Resize(200, 200);
                img.Save("~/Templates/" + "/images/" + resimimg);
                galeriResim.kresim = "/Templates/" + "/images/" + resimimg;
               
                galeriResim.Galeri_Id = tblGaleri.Id;
                List<tblGaleriResim> resim = db.tblGaleriResim.Where(x => x.Galeri_Id ==galeriResim.Galeri_Id).ToList();
                galeriResim.sira = resim.Count+1;
                db.tblGaleriResim.Add(galeriResim);
                db.SaveChanges();
                TempData["genelMesaj"] = "Resim Eklendi";
                return RedirectToAction("Edit/" + galeriResim.Galeri_Id, "tblGaleri");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: tblGaleriResim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGaleriResim tblGaleriResim = db.tblGaleriResim.Find(id);
            if (tblGaleriResim == null)
            {
                return HttpNotFound();
            }
            ViewBag.Galeri_Id = new SelectList(db.tblGaleri, "Id", "g_adi", tblGaleriResim.Galeri_Id);
            return View(tblGaleriResim);
        }

        // POST: tblGaleriResim/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Galeri_Id,kresim,bresim,sira")] tblGaleriResim tblGaleriResim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGaleriResim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Galeri_Id = new SelectList(db.tblGaleri, "Id", "g_adi", tblGaleriResim.Galeri_Id);
            return View(tblGaleriResim);
        }
        [HttpPost]
        public JsonResult Aciklama(string id)
        {

            int ids = Convert.ToInt32(id);
            var acikla = db.tblGaleriResim.Find(ids);
            if (acikla == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            if (acikla.aciklama == null)
            {
                var result = new { ID = acikla.Id };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { ID = acikla.Id, Result = acikla.aciklama };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            
           

        }
        [HttpPost]
        public JsonResult AciklamaKayit(string id,string aciklama)
        {
            
            int ids = Convert.ToInt32(id);
            var acikla = db.tblGaleriResim.Find(ids);
            if (acikla == null)
            {
                TempData["genelMesaj"] = "Kaydolmadı";
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            acikla.aciklama = aciklama;
            db.SaveChanges();
            TempData["genelMesaj"] = "Kaydoldu";
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Sira(string id, string sira, tblGaleriResim galeriResim)
        {

            int ids = Convert.ToInt32(id);
            int sirasi = Convert.ToInt32(sira);
            var resimsira = db.tblGaleriResim.Find(ids);
            if (sira == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            if (sirasi > 0)
            {
                resimsira.sira = sirasi;
                db.SaveChanges();
            }


            TempData["genelMesaj"] = "Kaydoldu";
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult Delete(string id)
        {
            int ids = Convert.ToInt32(id);
            var silinicekresim = db.tblGaleriResim.Find(ids);
            if (silinicekresim == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            db.tblGaleriResim.Remove(silinicekresim);
            System.IO.File.Delete(Server.MapPath(silinicekresim.bresim));
            System.IO.File.Delete(Server.MapPath(silinicekresim.kresim));
            TempData["genelMesaj"] = "Silindi";
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);

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
