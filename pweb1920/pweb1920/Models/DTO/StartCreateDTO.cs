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

        [Required]
        public int District { get; set; }
        [Required]
        public int City { get; set; }
        [Required]
        public int Station { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }
    }
}