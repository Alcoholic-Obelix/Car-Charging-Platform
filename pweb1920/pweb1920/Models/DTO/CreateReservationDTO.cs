using pweb1920.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pweb1920.Models.DTO
{
    public class CreateReservationDTO
    {
        public Station Station { get; set; }
        public List<Station> Stations { get; set; }
        public Reservation Reservation { get; set; }
        public List<SelectListItem> DistrictDropDown { get; set; }
        public List<SelectListItem> CityDropDown { get; set; }
    }
}