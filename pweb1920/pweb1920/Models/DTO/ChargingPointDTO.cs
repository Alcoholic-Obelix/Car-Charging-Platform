using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pweb1920.Models.DTO
{
    public class ChargingPointDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int ModeId { get; set; }
        public string ModeName { get; set; }
        public string ModeDescription { get; set; }

        public ChargingPointDTO(int Id, string Status, int ModeId, string ModeName, string ModeDescription)
        {
            this.Id = Id;
            this.Status = Status;
            this.ModeId = ModeId;
            this.ModeName = ModeName;
            this.ModeDescription = ModeDescription;
        }
    }
}