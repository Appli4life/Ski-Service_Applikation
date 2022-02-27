using Ski_Service_Applikation.Core;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace Ski_Service_Applikation.Controllers
{
    public class MitarbeiterController : Controller
    {
        private ski_serviceEntities db = new ski_serviceEntities();

        // GET: Mitarbeiter
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

                var mitarbeiter = db.mitarbeiter.Include(m => m.berechtigungsstufe);
                return View(mitarbeiter.ToList());

            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Mitarbeiter/Details/5
        public ActionResult Details(int? id)
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

                mitarbeiter mitarbeiter = db.mitarbeiter.Find(id);
                if (mitarbeiter == null)
                {
                    return HttpNotFound();
                }
                return View(mitarbeiter);

            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Mitarbeiter/Create
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

                ViewBag.Berechtigungsstufe_ID = new SelectList(db.berechtigungsstufe, "Berechtigungsstufe_ID", "Berechtigungsstufe1");
                return View();

            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: Mitarbeiter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Mitarbeiter_ID,username,Vorname,Nachname,Email,Telefon,Password,Berechtigungsstufe_ID")] mitarbeiter mitarbeiter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mitarbeiter.Password = Passwort_Hash.ComputeHash(mitarbeiter.Password, new MD5CryptoServiceProvider());
                    db.mitarbeiter.Add(mitarbeiter);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception)
            {
                return View("Error");
            }

            ViewBag.Berechtigungsstufe_ID = new SelectList(db.berechtigungsstufe, "Berechtigungsstufe_ID", "Berechtigungsstufe1", mitarbeiter.Berechtigungsstufe_ID);
            return View(mitarbeiter);
        }

        // GET: Mitarbeiter/Edit/5
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
                mitarbeiter mitarbeiter = db.mitarbeiter.Find(id);
                if (mitarbeiter == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Berechtigungsstufe_ID = new SelectList(db.berechtigungsstufe, "Berechtigungsstufe_ID", "Berechtigungsstufe1", mitarbeiter.Berechtigungsstufe_ID);
                return View(mitarbeiter);

            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: Mitarbeiter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Mitarbeiter_ID,username,Vorname,Nachname,Email,Telefon,Password,Berechtigungsstufe_ID")] mitarbeiter mitarbeiter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mitarbeiter.Password = Passwort_Hash.ComputeHash(mitarbeiter.Password, new MD5CryptoServiceProvider());
                    db.Entry(mitarbeiter).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Berechtigungsstufe_ID = new SelectList(db.berechtigungsstufe, "Berechtigungsstufe_ID", "Berechtigungsstufe1", mitarbeiter.Berechtigungsstufe_ID);
                return View(mitarbeiter);

            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Mitarbeiter/Delete/5
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
                mitarbeiter mitarbeiter = db.mitarbeiter.Find(id);
                if (mitarbeiter == null)
                {
                    return HttpNotFound();
                }
                return View(mitarbeiter);

            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // POST: Mitarbeiter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                mitarbeiter mitarbeiter = db.mitarbeiter.Find(id);
                db.mitarbeiter.Remove(mitarbeiter);
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
