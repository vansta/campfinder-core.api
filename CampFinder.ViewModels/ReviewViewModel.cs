using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.ViewModels
{
    public class ReviewViewModel
    {
        public Guid Id { get; set; }
        public double Score { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public Guid CampPlaceId { get; set; }
    }
}
