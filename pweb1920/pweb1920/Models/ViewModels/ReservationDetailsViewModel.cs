using pweb1920.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pweb1920.Models.ViewModels
{
    public class ReservationDetailsViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public System.DateTime Date { get; set; }

        [Display(Name = "Starting Time")]
        public System.TimeSpan TimeStart { get; set; }

        [Display(Name = "Finishing Time")]
        public System.TimeSpan TimeFinish { get; set; }

        [Display(Name = "Service Code")]
        public long ServiceCode { get; set; }

        [Display(Name = "Estimated Cost")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double EstimatedCost { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        public ReservationDetailsViewModel(Reservation reservation)
        {
            this.Id = reservation.Id;
            this.TimeStart = reservation.TimeStart;
            this.TimeFinish = reservation.TimeFinish;
            this.ServiceCode = reservation.ServiceCode;
            this.EstimatedCost = reservation.EstimatedCost;
            this.Status = reservation.Status;
        }
    }
}