using pweb1920.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pweb1920.Models.DTO
{
    public class IndexCompanyDTO
    {
        public IQueryable<Station> myStations { get; set; }
        public IQueryable<ChargingPoint> myChargingPoints { get; set; }
    }
}