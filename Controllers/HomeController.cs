using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ski_Service_Applikation.ViewModels;

namespace Ski_Service_Applikation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        { 
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult About()
        {
            using (var context = new ski_serviceEntities())
            {
  
                List<angebot> Angebote = context.angebot.ToList();

                var first = Angebote.First();

                var ViewModel = new firstAngebotViewModel()
                {
                    first_angebot = first
                };

                return View(ViewModel);

            }

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}