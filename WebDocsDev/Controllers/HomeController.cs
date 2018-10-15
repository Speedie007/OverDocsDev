using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDocs.Web.Controllers
{
    public class HomeController : Controller
    {
        WebDocs.DataAccessLayer.Models.WebDocsEntities db = new WebDocs.DataAccessLayer.Models.WebDocsEntities();
        public ActionResult Index()
        {
            IList<WebDocs.DataAccessLayer.Models.File> x = db.Files.ToList();

           
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