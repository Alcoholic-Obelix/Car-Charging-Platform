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
        public List<ChargingPointDTO> ChargingPoints { get; set; }

        public StationDetailsDTO(Station Station)
        {
            this.Station = Station;
            this.ChargingPoints = new List<ChargingPointDTO>();
        }
    }
}