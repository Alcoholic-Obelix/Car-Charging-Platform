using pweb1920.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pweb1920.Models.DTO
{
    public class IndexStationDTO
    {
        public const int DISTRICT_VIEW = 0;
        public const int CITY_VIEW = 1;
        public const int STATION_VIEW = 2;
        public int district { get; set; }
        public int city { get; set; }
        public int sttaion { get; set; }
        public List<Station> stations { get; set; }
    }
}