using pweb1920.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pweb1920.Models.DTO
{
    public class CreateReservationDTO
    {
        public Reservation Reservation { get; set; }

        public SelectList DistrictDropDown { get; set; }

        public SelectList CityDropDown { get; set; }

        public SelectList StationDropDown { get; set; }

        public int District { get; set; }
        public int City { get; set; }
        public int Station { get; set; }
        public List<ChargingPoint> ChargingPoints  { get; set; }
        public List<Reservation> FreeReservations { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}