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
        public SelectList DistrictDropDown { get; set; }
        public SelectList CityDropDown { get; set; }
        public int District { get; set; }
        public int City { get; set; }
    }
}