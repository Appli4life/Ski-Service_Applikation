using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            if(Response.Cookies["Warenkorb"] != null)
            {
                HttpCookie cookie = Request.Cookies["Warenkorb"];
                if (cookie["id"] != "")
                {
                    throw new Exception();
                    angebot a = db.angebot.Find(Convert.ToInt32(cookie["id"]));
                    return View(a);
                }
            }
            return View();
        }

        public ActionResult Delete()
        {
            Response.Cookies.Remove("Warenkorb");
            
            return RedirectToAction("Detail");
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

    }
}
