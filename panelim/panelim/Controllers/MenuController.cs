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
    public class MenuController : Controller
    {
        private SiteDbEntities db = new SiteDbEntities();

        // GET: Menu
        public ActionResult Index()
        {
            if (genelIslem.loginOlmusmu() == 0)
            {

                return RedirectToAction("Login", "Security");
            }

            return View(db.tblMenu.ToList());
        }


        // GET: Menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MenuAd")] tblMenu tblMenu)
        {
            if (ModelState.IsValid)
            {
              
                db.tblMenu.Add(tblMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblMenu);
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int? id, Sayfalar sayfalar )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        
            List<tblAltMenu> tblAltMenu = db.tblAltMenu.Where(x => x.MenuId == id && x.UstId==0).ToList();
    
            ViewBag.sayfa = new SelectList(db.Sayfalar.ToList(), "Id", "SayfaAdi");
            ViewBag.menu = id;
            Session["menuid"] =id;




            return View(tblAltMenu);
        }


    [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MenuAd")] tblMenu tblMenu)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(tblMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblMenu);
        }

       
    public ActionResult Delete(int id)
        {
            var silinicekmenu = db.tblMenu.Find(id);
            List<tblAltMenu> tblAlt = db.tblAltMenu.Where(x => x.MenuId == id).ToList();
            foreach (var item in tblAlt)
            {
                db.tblAltMenu.Remove(item);
            }
            if (silinicekmenu == null)
            {
                return HttpNotFound();
            }
            db.tblMenu.Remove(silinicekmenu);
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
