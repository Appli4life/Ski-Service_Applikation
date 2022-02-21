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
                angebot a = db.angebot.Find(cookie["id"]);
                return View(a);
            }
            return View();
        }

        public ActionResult Delete()
        {
            Response.Cookies.Remove("Warenkorb");
            
            return RedirectToAction("Detail");
        }
    }
}
