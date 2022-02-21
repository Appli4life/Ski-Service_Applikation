﻿using System;
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
        public ActionResult Login()
        {
            if (ModelState.IsValid)
            {
                using (var context = new ski_serviceEntities())
                {
                    var obj = context.kunde.Where(a => a.Email.Equals(Request.Form["Passwort"]) && a.Password.Equals(Request.Form["Passwort"])).FirstOrDefault()
                        ?? context.mitarbeiter.Where(a => a.Email.Equals(Request.Form["Passwort"]) && a.Password.Equals(Request.Form["Passwort"])).FirstOrDefault();


                    if (obj is kunde)
                    {
                        Session.Timeout = 10;

                        Session["Logged_in"] = true;
                        Session["Kunde_ID"] = obj.Kunde_ID.ToString();
                        Session["Vorname"] = obj.Vorname.ToString();
                        Session["Nachname"] = obj.Nachname.ToString();
                        Session["Email"] = obj.Email.ToString();
                        Session["Telefon"] = obj.Telefon.ToString();

                        return Redirect("/Angebot");
                    }
                    else if(obj is mitarbeiter)
                    {
                        Session.Timeout = 10;
                        Session["Logged_in"] = true;
                        Session["Stufe"] = obj.berechtigungsstufe.ToString();
                        Session["Kunde_ID"] = obj.Mitarbeiter_ID.ToString();
                        Session["Username"] = obj.username.ToString();
                        Session["Vorname"] = obj.Vorname.ToString();
                        Session["Nachname"] = obj.Nachname.ToString();
                        Session["Email"] = obj.Email.ToString();
                        Session["Telefon"] = obj.Telefon.ToString();

                        return Redirect("/Miete");
                    }


                }
            }
            return View();
        }
    }
}