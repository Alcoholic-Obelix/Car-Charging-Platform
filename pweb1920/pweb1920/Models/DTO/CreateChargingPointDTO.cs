using pweb1920.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pweb1920.Models.DTO
{
    public class CreateChargingPointDTO
    {
        public int StationId { get; set; }

        [Display( Name = "Modes")]
        public List<ChargingMode> ChargingModesList { get; set; }

        public CreateChargingPointDTO (int StationId, List<ChargingMode> ChargingModesList)
        {
            this.StationId = StationId;
            this.ChargingModesList = new List<ChargingMode>(ChargingModesList);
        }
    }
}