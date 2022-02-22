using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Ski_Service_Applikation;
using Ski_Service_Applikation.Core;

namespace Ski_Service_Applikation.Controllers
{
    public class MitarbeiterController : Controller
    {
        private ski_serviceEntities db = new ski_serviceEntities();

        // GET: Mitarbeiter
        public ActionResult Index()
        {
            Session.Timeout = 15;

            if (Session["Stufe"] != "Admin")
            {
                return Redirect("/Login");
            }

            var mitarbeiter = db.mitarbeiter.Include(m => m.berechtigungsstufe);
            return View(mitarbeiter.ToList());
        }

        // GET: Mitarbeiter/Details/5
        public ActionResult Details(int? id)
        {
            Session.Timeout = 15;

            if (Session["Stufe"] != "Admin")
            {
                return Redirect("/Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            mitarbeiter mitarbeiter = db.mitarbeiter.Find(id);
            if (mitarbeiter == null)
            {
                return HttpNotFound();
            }
            return View(mitarbeiter);
        }

        // GET: Mitarbeiter/Create
        public ActionResult Create()
        {
            Session.Timeout = 15;

            if (Session["Stufe"] != "Admin")
            {
                return Redirect("/Login");
            }

            ViewBag.Berechtigungsstufe_ID = new SelectList(db.berechtigungsstufe, "Berechtigungsstufe_ID", "Berechtigungsstufe1");
            return View();
        }

        // POST: Mitarbeiter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Mitarbeiter_ID,username,Vorname,Nachname,Email,Telefon,Password,Berechtigungsstufe_ID")] mitarbeiter mitarbeiter)
        {
            if (ModelState.IsValid)
            {
                mitarbeiter.Password = Passwort_Hash.ComputeHash(mitarbeiter.Password, new MD5CryptoServiceProvider());
                db.mitarbeiter.Add(mitarbeiter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Berechtigungsstufe_ID = new SelectList(db.berechtigungsstufe, "Berechtigungsstufe_ID", "Berechtigungsstufe1", mitarbeiter.Berechtigungsstufe_ID);
            return View(mitarbeiter);
        }

        // GET: Mitarbeiter/Edit/5
        public ActionResult Edit(int? id)
        {
            Session.Timeout = 15;

            if (Session["Stufe"] != "Admin")
            {
                return Redirect("/Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mitarbeiter mitarbeiter = db.mitarbeiter.Find(id);
            if (mitarbeiter == null)
            {
                return HttpNotFound();
            }
            ViewBag.Berechtigungsstufe_ID = new SelectList(db.berechtigungsstufe, "Berechtigungsstufe_ID", "Berechtigungsstufe1", mitarbeiter.Berechtigungsstufe_ID);
            return View(mitarbeiter);
        }

        // POST: Mitarbeiter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Mitarbeiter_ID,username,Vorname,Nachname,Email,Telefon,Password,Berechtigungsstufe_ID")] mitarbeiter mitarbeiter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mitarbeiter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Berechtigungsstufe_ID = new SelectList(db.berechtigungsstufe, "Berechtigungsstufe_ID", "Berechtigungsstufe1", mitarbeiter.Berechtigungsstufe_ID);
            return View(mitarbeiter);
        }

        // GET: Mitarbeiter/Delete/5
        public ActionResult Delete(int? id)
        {
            Session.Timeout = 15;

            if (Session["Stufe"] != "Admin")
            {
                return Redirect("/Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mitarbeiter mitarbeiter = db.mitarbeiter.Find(id);
            if (mitarbeiter == null)
            {
                return HttpNotFound();
            }
            return View(mitarbeiter);
        }

        // POST: Mitarbeiter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mitarbeiter mitarbeiter = db.mitarbeiter.Find(id);
            db.mitarbeiter.Remove(mitarbeiter);
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
