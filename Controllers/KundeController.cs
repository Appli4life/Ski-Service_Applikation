using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
            try
            {
                Session.Timeout = 15;
                // Nur Admins
                if (Session["Logged_in"] == null || Session["Stufe"].ToString() != "Admin")
                {
                    return Redirect("/Login");
                }
                return View(db.kunde.ToList());

            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Kunde/Details/5
        public ActionResult Details(int? id)
        {
            try
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

                kunde kunde = db.kunde.Find(id);

                if (kunde == null)
                {
                    return HttpNotFound();
                }
                return View(kunde);

            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Kunde/Create
        public ActionResult Create()
        {
            try
            {
                Session.Timeout = 15;

                // Nur Admins
                if (Session["Logged_in"] == null || Session["Stufe"].ToString() != "Admin")
                {
                    return Redirect("/Login");
                }
                return View();

            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: Kunde/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Kunde_ID,Vorname,Nachname,Telefon,Email,Password")] kunde kunde)
        {
            try
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
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult Registrieren()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrieren([Bind(Include = "Kunde_ID,Vorname,Nachname,Telefon,Email,Password")] kunde kunde)
        {
            try
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
            catch (DbUpdateException)
            {
                return View("Email_duplicate_error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Kunde/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                Session.Timeout = 15;

                // Nur Admins
                if (Session["Logged_in"] == null || Session["Stufe"].ToString() != "Admin")
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
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: Kunde/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Kunde_ID,Vorname,Nachname,Telefon,Email,Password")] kunde kunde)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    kunde.Password = Passwort_Hash.ComputeHash(kunde.Password, new MD5CryptoServiceProvider());
                    db.Entry(kunde).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(kunde);

            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Kunde/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                Session.Timeout = 15;

                // Nur Admins
                if (Session["Logged_in"] == null || Session["Stufe"].ToString() != "Admin")
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
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: Kunde/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                kunde kunde = db.kunde.Find(id);
                db.kunde.Remove(kunde);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
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
