using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Org.BouncyCastle.Crypto.Generators;
using Ski_Service_Applikation;
using Ski_Service_Applikation.Core;

namespace Ski_Service_Applikation.Controllers
{
    public class KundeController : Controller
    {
        private ski_serviceEntities db = new ski_serviceEntities();

        // GET: Kunde
        public ActionResult Index()
        {
            Session.Timeout = 15;

            if (Session["Stufe"] != "Admin")
            {
                return Redirect("/Login");
            }
            return View(db.kunde.ToList());
        }

        // GET: Kunde/Details/5
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
            kunde kunde = db.kunde.Find(id);
            if (kunde == null)
            {
                return HttpNotFound();
            }
            return View(kunde);
        }

        // GET: Kunde/Create
        public ActionResult Create()
        {
            Session.Timeout = 15;

            if (Session["Stufe"] != "Admin")
            {
                return Redirect("/Login");
            }
            return View();
        }

        // POST: Kunde/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Kunde_ID,Vorname,Nachname,Telefon,Email,Password")] kunde kunde)
        {
            if (ModelState.IsValid)
            {
                kunde.Password = Passwort_Hash.ComputeHash(kunde.Password, new MD5CryptoServiceProvider());
                db.kunde.Add(kunde);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kunde);
        }

        public ActionResult Registrieren()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrieren([Bind(Include = "Kunde_ID,Vorname,Nachname,Telefon,Email,Password")] kunde kunde)
        {
            if (ModelState.IsValid)
            {
                kunde.Password = Passwort_Hash.ComputeHash(kunde.Password, new MD5CryptoServiceProvider());
                db.kunde.Add(kunde);
                db.SaveChanges();
                return Redirect("/Angebot");
            }

            return View(kunde);
        }

        // GET: Kunde/Edit/5
        public ActionResult Edit(int? id)
        {
            Session.Timeout = 15;

            if (Session["Stufe"]!= "Admin")
            {
                return Redirect("/Login");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kunde kunde = db.kunde.Find(id);
            if (kunde == null)
            {
                return HttpNotFound();
            }
            return View(kunde);
        }

        // POST: Kunde/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Kunde_ID,Vorname,Nachname,Telefon,Email,Password")] kunde kunde)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kunde).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kunde);
        }

        // GET: Kunde/Delete/5
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
            kunde kunde = db.kunde.Find(id);
            if (kunde == null)
            {
                return HttpNotFound();
            }
            return View(kunde);
        }

        // POST: Kunde/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            kunde kunde = db.kunde.Find(id);
            db.kunde.Remove(kunde);
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
