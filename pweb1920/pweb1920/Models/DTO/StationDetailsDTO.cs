using pweb1920.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pweb1920.Models.DTO
{
    public class StationDetailsDTO
    {
        public Station Station { get; set; }
        public List<ChargingPoint> ChargingPoints { get; set; }
    }
}