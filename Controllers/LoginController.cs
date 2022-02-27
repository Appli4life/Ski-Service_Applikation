using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ski_Service_Applikation.Core;

namespace Ski_Service_Applikation.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Session.Timeout = 10;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login()
        {
            try
            {

                if (ModelState.IsValid)
                {
                    using (var context = new ski_serviceEntities())
                    {
                        string email = Request.Form["Email"];
                        string passwort = Passwort_Hash.VerifyPassword(Request.Form["Passwort"]);

                        kunde k = context.kunde.Where(a => a.Email.Equals(email) && a.Password.Equals(passwort)).FirstOrDefault();

                        if (k != null)
                        {
                            Session.Timeout = 10;

                            Session["Logged_in"] = true;
                            Session["Stufe"] = "";
                            Session["User_id"] = k.Kunde_ID.ToString();
                            Session["Vorname"] = k.Vorname.ToString();
                            Session["Nachname"] = k.Nachname.ToString();
                            Session["Email"] = k.Email.ToString();
                            Session["Telefon"] = k.Telefon.ToString();

                            return Redirect("/Angebot");
                        }
                        else
                        {
                            mitarbeiter m = context.mitarbeiter.Where(a => a.Email.Equals(email) && a.Password.Equals(passwort)).FirstOrDefault();

                            if (m != null)
                            {
                                Session.Timeout = 10;
                                Session["Logged_in"] = true;
                                Session["Stufe"] = m.berechtigungsstufe.Berechtigungsstufe1.ToString();
                                Session["User_id"] = m.Mitarbeiter_ID.ToString();
                                Session["Username"] = m.username.ToString();
                                Session["Vorname"] = m.Vorname.ToString();
                                Session["Nachname"] = m.Nachname.ToString();
                                Session["Email"] = m.Email.ToString();
                                Session["Telefon"] = m.Telefon.ToString();
                                return Redirect("/Miete");
                            }
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        public ActionResult Logout()
        {
            Session["Logged_in"] = null;

            return RedirectToAction("Index");
        }

    }
}