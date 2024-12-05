using AdventureWorksWebApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace AdventureWorksWebApp.Controllers
{
    public class HomeController : Controller
    {
        private AdventureWorks_DBEntities db = new AdventureWorks_DBEntities();

        public ActionResult Index()
        {
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