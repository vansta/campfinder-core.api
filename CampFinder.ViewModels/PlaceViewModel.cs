using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.ViewModels
{
    public class PlaceViewModel
    {
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostNumber { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

        public double Accessibility { get; set; }
        public string AccessibilityNote { get; set; }
    }
}
