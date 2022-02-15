using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ski_Service_Applikation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            using (var context = new ski_serviceEntities())
            {
                var List<Angebot>Angebote = context.alle_angebote_view.ToList();

                Angebote
                

            }
            return View();
        }

        public ActionResult About()
        { 
            ViewBag.Message = "Your application description page.";

            return View();

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}