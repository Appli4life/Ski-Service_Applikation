﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ski_Service_Applikation;

namespace Ski_Service_Applikation.Controllers
{
    public class AngebotController : Controller
    {
        private ski_serviceEntities db = new ski_serviceEntities();

        // GET: Angebot
        public ActionResult Index()
        {
            var angebot = db.angebot.Include(a => a.kategorie).Include(a => a.marke);
            return View(angebot.ToList());
        }

        // Angebot Hinuzufügen
        public ActionResult Add(int? id)
        {
            if (Session["Logged_in"].Equals(true))
            {
                Redirect("/Login");
            }

            if (ModelState.IsValid)
            {
                if(db.angebot.Find(id) == null || Response.Cookies["Warenkorb"]["id"] != null)
                    return HttpNotFound();
            }            

            HttpCookie Warenkorb = new HttpCookie("Warenkorb");
            Warenkorb["id"] = id.ToString();
            // 24 Stunde Cookie
            Warenkorb.Expires.Add(new TimeSpan(24,0,0));
            Response.Cookies.Add(Warenkorb);

            RedirectToAction("/Warenkorb");

            return HttpNotFound();
        }

        // GET: Angebot/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            angebot angebot = db.angebot.Find(id);
            if (angebot == null)
            {
                return HttpNotFound();
            }
            return View(angebot);
        }

        // GET: Angebot/Create
        public ActionResult Create()
        {
            ViewBag.Kategorie_ID = new SelectList(db.kategorie, "Kategorie_ID", "Kategorie1");
            ViewBag.Marke_ID = new SelectList(db.marke, "Marke_ID", "Marke1");
            return View();
        }

        // POST: Angebot/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Angebot_ID,Preis_pro_Tag,Kategorie_ID,Marke_ID")] angebot angebot)
        {
            if (ModelState.IsValid)
            {
                db.angebot.Add(angebot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Kategorie_ID = new SelectList(db.kategorie, "Kategorie_ID", "Kategorie1", angebot.Kategorie_ID);
            ViewBag.Marke_ID = new SelectList(db.marke, "Marke_ID", "Marke1", angebot.Marke_ID);
            return View(angebot);
        }

        // GET: Angebot/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            angebot angebot = db.angebot.Find(id);
            if (angebot == null)
            {
                return HttpNotFound();
            }
            ViewBag.Kategorie_ID = new SelectList(db.kategorie, "Kategorie_ID", "Kategorie1", angebot.Kategorie_ID);
            ViewBag.Marke_ID = new SelectList(db.marke, "Marke_ID", "Marke1", angebot.Marke_ID);
            return View(angebot);
        }

        // POST: Angebot/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Angebot_ID,Preis_pro_Tag,Kategorie_ID,Marke_ID")] angebot angebot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(angebot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Kategorie_ID = new SelectList(db.kategorie, "Kategorie_ID", "Kategorie1", angebot.Kategorie_ID);
            ViewBag.Marke_ID = new SelectList(db.marke, "Marke_ID", "Marke1", angebot.Marke_ID);
            return View(angebot);
        }

        // GET: Angebot/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            angebot angebot = db.angebot.Find(id);
            if (angebot == null)
            {
                return HttpNotFound();
            }
            return View(angebot);
        }

        // POST: Angebot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            angebot angebot = db.angebot.Find(id);
            db.angebot.Remove(angebot);
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