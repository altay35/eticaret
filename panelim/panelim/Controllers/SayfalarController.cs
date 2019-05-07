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

namespace mvcpanel.Controllers
{
    public class SayfalarController : Controller
    {
        private SiteDbEntities db = new SiteDbEntities();

        // GET: Sayfalar
        public ActionResult Index()
        {

            if (genelIslem.loginOlmusmu() == 0)
            {

                return RedirectToAction("Login", "Security");
            }
        
            return View(db.Sayfalar.ToList());
        }


        // GET: Sayfalar/Create
        public ActionResult Create()
        {
            Sayfalar sayfa = new Sayfalar();
            ViewBag.GaleriId = new SelectList(db.tblGaleri, "Id", "g_adi");
            ViewBag.FormId = new MultiSelectList(db.tblForm, "id", "FormAdi");
           
       
            List<Sayfalar> sayfalar = db.Sayfalar.ToList();
            sayfa.sirano = sayfalar.Count + 1;
            return View(sayfa);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SayfaAdi,Icerik,headimage,headaktif,sayfaaktif,sirano,GaleriId")] Sayfalar sayfalar, HttpPostedFileBase headimage)
        {
            if (ModelState.IsValid)
            {
                if (headimage == null)
                {
                    
                    List<Sayfalar> sayfa = db.Sayfalar.ToList();
                    sayfalar.sirano = sayfa.Count + 1;
                    db.Sayfalar.Add(sayfalar);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else if (headimage.ContentLength > 0)
                {
                    WebImage img = new WebImage(headimage.InputStream);
                    FileInfo headimageinfo = new FileInfo(headimage.FileName);

                    string newheadimage = Guid.NewGuid().ToString() + headimageinfo.Extension;
                    Session["headimage"] = newheadimage;
                    img.Resize(1200, 450);
                    img.Save("~/Templates/" + "/images/" + newheadimage);
                    sayfalar.headimage = "/Templates/" + "/images/" + newheadimage;
                  
                    db.Sayfalar.Add(sayfalar);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }


            }
            ViewBag.GaleriId = new SelectList(db.tblGaleri, "Id", "g_adi", sayfalar.GaleriId);
            return View(sayfalar);
        }


        // GET: Sayfalar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sayfalar sayfalar = db.Sayfalar.Find(id);
            if (sayfalar == null)
            {
                return HttpNotFound();
            }
            ViewBag.GaleriId = new SelectList(db.tblGaleri, "Id", "g_adi", sayfalar.GaleriId);
            ViewBag.sayfaid = id;
            ViewBag.FormId = new MultiSelectList(db.tblForm, "id", "FormAdi");
            return View(sayfalar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SayfaAdi,Icerik,TempId,headimage,headaktif,sayfaaktif,sirano,FormId,GaleriId")] Sayfalar sayfalar, HttpPostedFileBase headimage, int[] FormId)
        {
            if (ModelState.IsValid)

            {
                if (headimage != null)
                {
                    WebImage img = new WebImage(headimage.InputStream);
                    FileInfo headimageinfo = new FileInfo(headimage.FileName);

                    string newheadimage = Guid.NewGuid().ToString() + headimageinfo.Extension;
                    Session["headimage"] = newheadimage;
                    img.Resize(1200, 450);
                    img.Save("~/Templates/"+ "/images/" + newheadimage);
                    sayfalar.headimage = "/Templates/" + "/images/" + newheadimage;

                }

                if (FormId != null)
                {
                    sayfaform sayfaform = new sayfaform();
           
                    for (int i = 0; i <= FormId.Length-1; i++)
                    {
                        sayfaform.SayfaId = sayfalar.Id;
                        sayfaform.FormId = FormId[i];
                        db.sayfaform.Add(sayfaform);

                        db.SaveChanges();
                    }
                  
                }
                db.Sayfalar.Add(sayfalar);
                db.Entry(sayfalar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GaleriId = new SelectList(db.tblGaleri, "Id", "g_adi", sayfalar.GaleriId);
            return View(sayfalar);
        }
        public JsonResult form(int? id)
        {

            List<tblFormIcerik> tblForm = db.tblFormIcerik.Where(x => x.FormId == id).ToList();
            return Json(tblForm, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Sil(int id, Sayfalar sayfa)
        {
            var silineceksayfa = db.Sayfalar.Find(id);
            List<tblAltMenu> tblAlt = db.tblAltMenu.Where(x => x.SayfaId == id).ToList();
            foreach (var item in tblAlt)
            {
                db.tblAltMenu.Remove(item);
            }
            if (silineceksayfa == null)
            {
                return HttpNotFound();
            }

            db.Sayfalar.Remove(silineceksayfa);
            if (silineceksayfa.headimage != null)
            {
                System.IO.File.Delete(Server.MapPath("~/Templates/" + silineceksayfa));
            }
           
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult ResimSil(int id, Sayfalar sayfa)
        {
            var model = db.Sayfalar.Find(id);
            model.headimage = null;

            db.SaveChanges();
            return RedirectToAction("Edit", new { id = id });
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
