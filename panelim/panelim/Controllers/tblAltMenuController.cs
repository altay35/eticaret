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
    public class tblAltMenuController : Controller
    {
        private static SiteDbEntities db = new SiteDbEntities();

        // GET: tblAltMenu
        public ActionResult Index()
        {
            //var tblAltMenu = db.tblAltMenu.Include(t => t.Sayfalar).Include(t => t.tblMenu);

            return View();
        }

        // GET: tblAltMenu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAltMenu tblAltMenu = db.tblAltMenu.Find(id);
            if (tblAltMenu == null)
            {
                return HttpNotFound();
            }
            return View(tblAltMenu);
        }

        // GET: tblAltMenu/Create
        public ActionResult Create()
        {
            ViewBag.SayfaId = new SelectList(db.Sayfalar, "Id", "SayfaAdi");
            ViewBag.MenuId = new SelectList(db.tblMenu, "Id", "MenuAd");
            return View();
        }


        [HttpPost]
        public JsonResult altMenuKaydet(string veriler,string menuad)
        {
           
            
            int menuid = Convert.ToInt32(Session["menuid"]);
            if (menuad != null)
            {
                var model = db.tblMenu.Find(menuid);
                model.Id = menuid;
                model.MenuAd = menuad;
                Session["menuad"] = model.MenuAd;
                db.SaveChanges();

            }



            string[] veri = veriler.Split(';');


            foreach (var item in veri)
            {

                if (item != "")
                {
                    string[] dd = item.ToString().Split('-');

                    int gelenMenuid = Convert.ToInt32(dd[0]);
                    int gelenSayfaid = Convert.ToInt32(dd[1]);
                    int gelenUstid = Convert.ToInt32(dd[2]);
                    if (gelenUstid > 0)
                    {
                        List<tblAltMenu> liste = db.tblAltMenu.Where(m => m.MenuId == gelenMenuid && m.SayfaId == gelenUstid).AsEnumerable().ToList();
                        
                        if (liste.Count > 0)
                        {
                            gelenUstid = liste.FirstOrDefault().Id;
                        }
                    }

                
                    tblAltMenu alt = new tblAltMenu();
                    
                    alt.MenuId = gelenMenuid;
                    alt.SayfaId = gelenSayfaid;
                    alt.UstId = gelenUstid;
                    db.tblAltMenu.Add(alt);
                    db.SaveChanges();


                }





            }
            return Json(menuad);

        }


        // POST: tblAltMenu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MenuId,SayfaId,UstId")] tblAltMenu tblAltMenu)
        {
            if (ModelState.IsValid)
            {
                db.tblAltMenu.Add(tblAltMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SayfaId = new SelectList(db.Sayfalar, "Id", "SayfaAdi", tblAltMenu.SayfaId);
            ViewBag.MenuId = new SelectList(db.tblMenu, "Id", "MenuAd", tblAltMenu.MenuId);
            return View(tblAltMenu);
        }

        // GET: tblAltMenu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAltMenu tblAltMenu = db.tblAltMenu.Find(id);
            if (tblAltMenu == null)
            {
                return HttpNotFound();
            }
            ViewBag.SayfaId = new SelectList(db.Sayfalar, "Id", "SayfaAdi", tblAltMenu.SayfaId);
            ViewBag.MenuId = new SelectList(db.tblMenu, "Id", "MenuAd", tblAltMenu.MenuId);
            return View(tblAltMenu);
        }

        // POST: tblAltMenu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        public ActionResult Edit([Bind(Include = "Id,MenuId,SayfaId,UstId")] tblAltMenu tblAltMenu, int ustid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblAltMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SayfaId = new SelectList(db.Sayfalar, "Id", "SayfaAdi", tblAltMenu.SayfaId);
            ViewBag.MenuId = new SelectList(db.tblMenu, "Id", "MenuAd", tblAltMenu.MenuId);
            return View(tblAltMenu);
        }
        
        public ActionResult Delete(int id)
        {
        
            List<tblAltMenu> tblAlt = db.tblAltMenu.Where(x => x.MenuId == id).ToList();
            if (tblAlt.Count > 0)
            {
                foreach (var item in tblAlt)
                {
                    db.tblAltMenu.Remove(item);
                    db.SaveChanges();
                }

            }



            return RedirectToAction("Index");
        }


    }
}
