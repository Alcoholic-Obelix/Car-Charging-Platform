using pweb1920.DAL;
using pweb1920.Models;
using pweb1920.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace pweb1920.Controllers
{
    public class HomeController : Controller
    {
        private ERDataModelContainer db = new ERDataModelContainer();

        public Client GetClient()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var userIdValue = userIdClaim.Value;
            var client = db.Clients.Where(m => m.IdentityId == userIdValue).FirstOrDefault();

            return client;
        }

        public Company GetCompany()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var userIdValue = userIdClaim.Value;
            var company = db.Companies.Where(m => m.IdentityId == userIdValue).FirstOrDefault();

            return company;
        }

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var client = GetClient();
                var indexClientDTO = new IndexClientDTO();
                indexClientDTO.myReservations = db.Reservations.Where(e => e.Client.Id == client.Id).Where(e => e.Status == ConstantValues.READY).Take(5).ToList();
                indexClientDTO.reservationsHistory = db.Reservations.Where(e => e.Client.Id == client.Id).Where(e => e.Status == ConstantValues.DONE).Take(5).ToList();

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