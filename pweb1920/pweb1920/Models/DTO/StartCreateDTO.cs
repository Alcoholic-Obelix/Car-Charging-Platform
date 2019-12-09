using pweb1920.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pweb1920.Models.DTO
{
    public class StartCreateDTO
    {
        public SelectList DistrictDropDown { get; set; }

        public int District { get; set; }
        public int City { get; set; }
        public int Station { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}