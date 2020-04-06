using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.ViewModels
{
    public class BuildingViewModel: CampPlaceViewModel
    {
        public string Dormitories { get; set; }
        public bool KitchenGear { get; set; }
        public bool Beds { get; set; }
        public string DaySpaces { get; set; }
    }
}
