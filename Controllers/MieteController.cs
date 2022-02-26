using System;
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
    public class MieteController : Controller
    {
        private ski_serviceEntities db = new ski_serviceEntities();

        // GET: Miete
        public ActionResult Index()
        {
            Session.Timeout = 15;

            // Nur Mitarbeiter
            if (Session["Logged_in"] == null || Session["Stufe"].ToString() == "")
            {
                return Redirect("/Login");
            }

            var miete = db.miete.Include(m => m.altersgruppe).Include(m => m.angebot).Include(m => m.geschlecht).Include(m => m.kunde).Include(m => m.status);
            return View(miete.ToList());
        }

        // GET: Miete/Details/5
        public ActionResult Details(int? id)
        {
            Session.Timeout = 15;
            // Nur Mitarbeiter
            if (Session["Logged_in"] == null || Session["Stufe"].ToString() == "")
            {
                return Redirect("/Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            miete miete = db.miete.Find(id);
            if (miete == null)
            {
                return HttpNotFound();
            }
            return View(miete);
        }

        // GET: Miete/Create
        public ActionResult Create()
        {
            Session.Timeout = 15;
            // Nur Mitarbeiter
            if (Session["Logged_in"] == null || Session["Stufe"].ToString() == "")
            {
                return Redirect("/Login");
            }

            ViewBag.Altersgruppe_ID = new SelectList(db.altersgruppe, "Altersgruppe_ID", "Altersgruppe1");
            ViewBag.Angebot_ID = new SelectList(db.angebot, "Angebot_ID", "Angebot_ID");
            ViewBag.Geschlecht_ID = new SelectList(db.geschlecht, "Geschlecht_ID", "Geschlecht1");
            ViewBag.Kunde_ID = new SelectList(db.kunde, "Kunde_ID", "Email");
            ViewBag.Status_ID = new SelectList(db.status, "Status_ID", "Status1");
            return View();
        }

        // POST: Miete/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Miete_ID,Angebot_ID,Kunde_ID,Miet_Datum,Rueckgabe_Datum,Menge,Koerpergroesse,Altersgruppe_ID,Geschlecht_ID,Status_ID")] miete miete)
        {
            if (ModelState.IsValid)
            {
                db.miete.Add(miete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Altersgruppe_ID = new SelectList(db.altersgruppe, "Altersgruppe_ID", "Altersgruppe1", miete.Altersgruppe_ID);
            ViewBag.Angebot_ID = new SelectList(db.angebot, "Angebot_ID", "Angebot_ID", miete.Angebot_ID);
            ViewBag.Geschlecht_ID = new SelectList(db.geschlecht, "Geschlecht_ID", "Geschlecht1", miete.Geschlecht_ID);
            ViewBag.Kunde_ID = new SelectList(db.kunde, "Kunde_ID", "Vorname", miete.Kunde_ID);
            ViewBag.Status_ID = new SelectList(db.status, "Status_ID", "Status1", miete.Status_ID);
            return View(miete);
        }

        // GET: Miete/Edit/5
        public ActionResult Edit(int? id)
        {
            Session.Timeout = 15;
            // Nur Mitarbeiter
            if (Session["Logged_in"] == null || Session["Stufe"].ToString() == "")
            {
                return Redirect("/Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            miete miete = db.miete.Find(id);
            if (miete == null)
            {
                return HttpNotFound();
            }
            ViewBag.Altersgruppe_ID = new SelectList(db.altersgruppe, "Altersgruppe_ID", "Altersgruppe1", miete.Altersgruppe_ID);
            ViewBag.Angebot_ID = new SelectList(db.angebot, "Angebot_ID", "Angebot_ID", miete.Angebot_ID);
            ViewBag.Geschlecht_ID = new SelectList(db.geschlecht, "Geschlecht_ID", "Geschlecht1", miete.Geschlecht_ID);
            ViewBag.Kunde_ID = new SelectList(db.kunde, "Kunde_ID", "Email", miete.kunde.Email);
            ViewBag.Status_ID = new SelectList(db.status, "Status_ID", "Status1", miete.Status_ID);
            return View(miete);
        }

        // POST: Miete/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Miete_ID,Angebot_ID,Kunde_ID,Miet_Datum,Rueckgabe_Datum,Menge,Koerpergroesse,Altersgruppe_ID,Geschlecht_ID,Status_ID")] miete miete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(miete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Altersgruppe_ID = new SelectList(db.altersgruppe, "Altersgruppe_ID", "Altersgruppe1", miete.Altersgruppe_ID);
            ViewBag.Angebot_ID = new SelectList(db.angebot, "Angebot_ID", "Angebot_ID", miete.Angebot_ID);
            ViewBag.Geschlecht_ID = new SelectList(db.geschlecht, "Geschlecht_ID", "Geschlecht1", miete.Geschlecht_ID);
            ViewBag.Kunde_ID = new SelectList(db.kunde, "Kunde_ID", "Vorname", miete.Kunde_ID);
            ViewBag.Status_ID = new SelectList(db.status, "Status_ID", "Status1", miete.Status_ID);
            return View(miete);
        }

        // GET: Miete/Delete/5
        public ActionResult Delete(int? id)
        {
            Session.Timeout = 15;
            // Nur Mitarbeiter
            if (Session["Logged_in"] == null || Session["Stufe"].ToString() == "")
            {
                return Redirect("/Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            miete miete = db.miete.Find(id);
            if (miete == null)
            {
                return HttpNotFound();
            }
            return View(miete);
        }

        // POST: Miete/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            miete miete = db.miete.Find(id);
            db.miete.Remove(miete);
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
