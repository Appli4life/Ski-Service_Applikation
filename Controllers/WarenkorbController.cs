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
            RedirectToAction("Details");
            return View();
        }

        // GET: Warenkorb/Details
        public ActionResult Details()
        {
            if(Response.Cookies["Warenkorb"] != null)
            {
                angebot a = db.angebot.Find(Response.Cookies["Warenkorb"]["id"]);
                return View(a);
            }
            return View();
        }

        public ActionResult Delete()
        {
            Response.Cookies.Remove("Warenkorb");
            RedirectToAction("Details");
            return View();
        }
    }
}
