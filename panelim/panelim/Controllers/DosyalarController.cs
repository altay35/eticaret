using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using panelim.Models;

namespace panelim.Controllers
{
    public class DosyalarController : Controller
    {
        private SiteDbEntities db = new SiteDbEntities();

        // GET: Dosyalar
        public ActionResult Index()
        {

            if (genelIslem.loginOlmusmu() == 0)
            {

                return RedirectToAction("Login", "Security");
            }

            return View();
        }


        // GET: Dosyalar/Create
        public ActionResult Create()
        {


            return View();
        }

        // POST: Dosyalar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblDosya tblDosya,HttpPostedFileBase yuklenecekdosya)
        {
            if (yuklenecekdosya.ContentLength>0)
            {

                //FileInfo yuklenecekdosyainfo = new FileInfo(yuklenecekdosya.FileName);
                string guidi = Guid.NewGuid().ToString();
                string newheadimage = guidi  + yuklenecekdosya.FileName ;
                string ext = System.IO.Path.GetExtension(newheadimage);
                tblDosya.DosyaEkle = Path.Combine(Server.MapPath("~/Templates/Files/"), newheadimage);
                yuklenecekdosya.SaveAs(tblDosya.DosyaEkle);


                tblDosya.DosyaAdi = yuklenecekdosya.FileName;
                Session["ad"] = newheadimage;
                tblDosya.DosyaEkle = newheadimage ;
                db.tblDosya.Add(tblDosya);
                db.SaveChanges();
              
                return RedirectToAction("Index");
            }
            
            return View(tblDosya);
        }



      
        public ActionResult Delete(int id)
        {
            var silinecekdosya = db.tblDosya.Find(id);
            if (silinecekdosya == null)
            {
                return HttpNotFound();
            }
            db.tblDosya.Remove(silinecekdosya);
            if (silinecekdosya.DosyaEkle != null)
            {
                System.IO.File.Delete(Server.MapPath("~/Templates/Files/" + silinecekdosya.DosyaEkle));
            }

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
