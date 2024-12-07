using AdventureWorksWebApp.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AdventureWorksWebApp.Controllers
{
    public class HomeController : BaseController
    {
        private AdventureWorks_DBEntities db = new AdventureWorks_DBEntities();

        public ActionResult Index()
        {
            var photos = db.Photo.AsNoTracking().Include("User").OrderByDescending(p => p.PhotoID).Take(4).AsEnumerable();
            
            return View(photos);
        }

    }
}