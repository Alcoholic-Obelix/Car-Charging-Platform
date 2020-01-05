using pweb1920.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pweb1920.Models.DTO
{
    public class StationDetailsDTO
    {
        public string CurrentCompanyIdentityId { get; set; }
        public Station Station { get; set; }
        public List<ChargingPointDTO> ChargingPoints { get; set; }

        public StationDetailsDTO(string CurrentCompanyIdentityId, Station Station)
        {
            this.CurrentCompanyIdentityId = CurrentCompanyIdentityId;
            this.Station = Station;
            this.ChargingPoints = new List<ChargingPointDTO>();
        }
    }
}