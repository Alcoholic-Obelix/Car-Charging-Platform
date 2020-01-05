using pweb1920.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pweb1920.Models.DTO
{
    public class StationDTO
    {
        public List<Station> StationList { get; set; }

        public StationDTO (List<Station> StationList)
        {
            this.StationList = new List<Station>(StationList);
        }
    }
}