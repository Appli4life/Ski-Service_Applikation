using System.Web.Mvc;

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
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}