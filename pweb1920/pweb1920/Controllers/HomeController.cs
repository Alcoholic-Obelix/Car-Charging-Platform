using pweb1920.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pweb1920.Controllers
{
    public class HomeController : Controller
    {
        private ERDataModelContainer db = new ERDataModelContainer();

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View("IndexClient", db.Reservations.ToList());
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