using pweb1920.DAL;
using pweb1920.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pweb1920.Models.ViewModels
{
    public class MyChargingPointsViewModel
    {
        public Station Station { get; set; }
        public List<ChargingPointDTO> ChargingPoints;

        public MyChargingPointsViewModel(Station Station, List<ChargingPointDTO> ChargingPoints)
        {
            this.Station = Station;
            this.ChargingPoints = new List<ChargingPointDTO>(ChargingPoints);
        }
    }
}