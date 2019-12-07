using pweb1920.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pweb1920.Models.DTO
{
    public class IndexStationDTO
    {
        public Station District { get; set; }
        public Station City { get; set; }
        public List<Station> Stations { get; set; }
    }
}