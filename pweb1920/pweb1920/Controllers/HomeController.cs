using pweb1920.DAL;
using pweb1920.Models;
using pweb1920.Models.DTO;
using pweb1920.Models.ViewModels;
using System.Linq;
using System.Security.Claims;
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
                if(User.IsInRole("Client"))
                {
                    var client = GetClient();
                    var indexClientDTO = new IndexClientDTO();

               db.Reservations
                .Join(db.ChargingPoints,
                    reser => reser.ChargingPoint,
                    charg => charg,
                    (reser, charg) => new
                    {
                        Reser = reser,
                        Charg = charg
                    }
                )
                .Join(db.Stations,
                    joined => joined.Charg.Station,
                    stat => stat,
                    (joined, stat) => new
                    {
                        Joined = joined,
                        Stat = stat
                    }
                )
                .OrderByDescending(e => e.Joined.Reser.Date)
                .Where(e => e.Joined.Reser.Client.Id == client.Id)
                .Where(e => e.Joined.Reser.Status == ConstantValues.READY)
                .Select(e => new
                {
                    Reservation = e.Joined.Reser,
                    StationName = e.Stat.Name
                }).Take(5).ToList().ForEach(e => indexClientDTO.myReservations.Add(new ReservationDetailsViewModel(e.Reservation, e.StationName)));

                    db.Reservations
                .Join(db.ChargingPoints,
                    reser => reser.ChargingPoint,
                    charg => charg,
                    (reser, charg) => new
                    {
                        Reser = reser,
                        Charg = charg
                    }
                )
                .Join(db.Stations,
                    joined => joined.Charg.Station,
                    stat => stat,
                    (joined, stat) => new
                    {
                        Joined = joined,
                        Stat = stat
                    }
                )
                .OrderByDescending(e => e.Joined.Reser.Date)
                .Where(e => e.Joined.Reser.Client.Id == client.Id)
                .Where(e => e.Joined.Reser.Status == ConstantValues.DONE)
                .Select(e => new
                {
                    Reservation = e.Joined.Reser,
                    StationName = e.Stat.Name
                }).Take(5).ToList().ForEach(e => indexClientDTO.reservationsHistory.Add(new ReservationDetailsViewModel(e.Reservation, e.StationName)));
                    
 
                    return View("IndexClient", indexClientDTO);
                }
                else if (User.IsInRole("Company"))
                {
                    var company = GetCompany();

                    if(company.Status == ConstantValues.ACCEPTED)
                    {
                        var myStations = db.Stations.Where(e => e.Companies.Id == company.Id).ToList();
                        var myChargingPoints = db.ChargingPoints.Where(e => e.Station.Companies.Id == company.Id).ToList();

                        var indexCompanyDTO = new IndexCompanyDTO();
                        indexCompanyDTO.myStations = myStations;
                        indexCompanyDTO.myChargingPoints = myChargingPoints;

                        return View("IndexCompany", indexCompanyDTO);
                    }
                    else
                    {
                        return View("PendingApproval");
                    }

                    
                }

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