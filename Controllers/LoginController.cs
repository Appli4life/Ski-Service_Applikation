using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ski_Service_Applikation.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(kunde k)
        {
            if (ModelState.IsValid)
            {
                using (var context = new ski_serviceEntities())
                {
                    var obj = context.kunde.Where(a => a.Email.Equals(k.Email) && a.Password.Equals(k.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["Logged_in"] = true;
                        Session["Kunde_ID"] = obj.Kunde_ID.ToString();
                        Session["Vorname"] = obj.Vorname.ToString();
                        Session["Nachname"] = obj.Nachname.ToString();
                        Session["Email"] = obj.Email.ToString();
                        Session["Telefon"] = obj.Telefon.ToString();

                        return Redirect("/Angebot");
                    }
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(mitarbeiter m)
        {
            if (ModelState.IsValid)
            {
                using (var context = new ski_serviceEntities())
                {
                    var obj = context.mitarbeiter.Where(a => a.Email.Equals(m.Email) && a.Password.Equals(m.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["Logged_in"] = true;
                        Session["Stufe"] = obj.berechtigungsstufe.ToString();
                        Session["Kunde_ID"] = obj.Mitarbeiter_ID.ToString();
                        Session["Username"] = obj.username.ToString();
                        Session["Vorname"] = obj.Vorname.ToString();
                        Session["Nachname"] = obj.Nachname.ToString();
                        Session["Email"] = obj.Email.ToString();
                        Session["Telefon"] = obj.Telefon.ToString();

                        return Redirect("/");
                    }
                }
            }
            return View();
        }

    }
}