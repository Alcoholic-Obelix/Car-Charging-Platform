using pweb1920.DAL;
using pweb1920.Models.DTO;
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

                var indexClientDTO = new IndexClientDTO();
                indexClientDTO.myReservations = db.Reservations.Where(e => e.Status == "1").Take(5).ToList();
                indexClientDTO.reservationsHistory = db.Reservations.Where(e => e.Status == "0").Take(5).ToList();

                return View("IndexClient", indexClientDTO);
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