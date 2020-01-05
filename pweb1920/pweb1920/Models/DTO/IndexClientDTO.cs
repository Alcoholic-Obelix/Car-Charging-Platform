using pweb1920.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pweb1920.Models.DTO
{
    public class IndexClientDTO
    {
        public List<ReservationDetailsViewModel> myReservations { get; set; }
        public List<ReservationDetailsViewModel> reservationsHistory { get; set; }

        
        public IndexClientDTO()
        {
            this.myReservations = new List<ReservationDetailsViewModel>();
            this.reservationsHistory = new List<ReservationDetailsViewModel>();
        }
    }
}