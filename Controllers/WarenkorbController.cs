using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Ski_Service_Applikation.Core;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ski_Service_Applikation.Controllers
{
    public class WarenkorbController : Controller
    {
        private ski_serviceEntities db = new ski_serviceEntities();

        // GET: Warenkorb
        public ActionResult Index()
        {
            return RedirectToAction("Detail");
        }

        // GET: Warenkorb/Details
        public ActionResult Detail()
        {
            Session.Timeout = 15;

            if (Session["Logged_in"] == null)
            {
                return Redirect("/Login");
            }

            if (Request.Cookies["Warenkorb"] != null)
            {
                HttpCookie cookie = Request.Cookies["Warenkorb"];
                if (cookie["id"] != "")
                {
                    angebot a = db.angebot.Find(Convert.ToInt32(cookie["id"]));
                    ViewBag.Altersgruppe_ID = new SelectList(db.altersgruppe, "Altersgruppe_ID", "Altersgruppe1");
                    ViewBag.Geschlecht_ID = new SelectList(db.geschlecht, "Geschlecht_ID", "Geschlecht1");
                    return View(a);
                }
            }
            return Redirect("/Angebot");
        }

        public ActionResult Delete()
        {
            HttpCookie Warenkorb = new HttpCookie("Warenkorb");

            Warenkorb.Expires.Add(new TimeSpan(0, 0, 1));
            Response.Cookies.Set(Warenkorb);

            return Redirect("/");
        }


        // Angebot Hinuzufügen
        public ActionResult Add(int? id)
        {
            Session.Timeout = 15;

            if (Session["Logged_in"] == null)
            {
                return Redirect("/Login");
            }

            if (ModelState.IsValid)
            {
                angebot angebot = db.angebot.Find(id);

                if (angebot == null)
                    return HttpNotFound();

                HttpCookie Warenkorb = new HttpCookie("Warenkorb");
                Warenkorb["id"] = angebot.Angebot_ID.ToString();
                Warenkorb.Expires.Add(new TimeSpan(48, 0, 0));
                Response.Cookies.Add(Warenkorb);
            }

            return RedirectToAction("Detail");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmation()
        {
            Session.Timeout = 15;

            HttpCookie cookie = Request.Cookies["Warenkorb"];
            if (cookie["id"] != "")
            {
                angebot a = db.angebot.Find(Convert.ToInt32(cookie["id"]));

                if (a != null)
                {
                    miete m = new miete()
                    {
                        Altersgruppe_ID = Convert.ToInt32(Request.Form["Altersgruppe_ID"]),
                        angebot = a,
                        Geschlecht_ID = Convert.ToInt32(Request.Form["Geschlecht_ID"]),
                        Koerpergroesse = Convert.ToInt32(Request.Form["groesse"]),
                        Kunde_ID = Convert.ToInt32(Session["User_id"]),
                        Miet_Datum = Convert.ToDateTime(Request.Form["von"]),
                        Rueckgabe_Datum = Convert.ToDateTime(Request.Form["bis"]),
                        Status_ID = db.status.FirstOrDefault().Status_ID,
                        Menge = Convert.ToInt32(Request.Form["menge"])
                    };

                    db.miete.Add(m);
                    db.SaveChanges();

                    if (Request.Form["pdf"] == "YES")
                    {
                        //PDF Generieren
                        PDF.Generate_Bill(m);
                    }

                    //Cookie löschen
                    HttpCookie Warenkorb = new HttpCookie("Warenkorb");

                    Warenkorb.Expires.Add(new TimeSpan(0, 0, 1));
                    Response.Cookies.Set(Warenkorb);

                    return View(m);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return HttpNotFound();
        }
    }
}
