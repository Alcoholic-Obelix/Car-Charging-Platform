using pweb1920.DAL;
using System.Collections.Generic;

namespace pweb1920.Models.DTO
{
    public class IndexCompanyDTO
    {
        public List<Station> myStations { get; set; }
        public List<ChargingPoint> myChargingPoints { get; set; }
    }
}