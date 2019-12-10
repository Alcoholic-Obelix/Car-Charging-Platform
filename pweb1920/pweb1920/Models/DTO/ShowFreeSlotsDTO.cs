using pweb1920.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pweb1920.Models.DTO
{
    public class ShowFreeSlotsDTO
    {
        public Station Station { get; set; }
        public List<Reservation> Reservations { get; set; }
        public SelectList ChargingModeDropdown { get; set; }
    }
}