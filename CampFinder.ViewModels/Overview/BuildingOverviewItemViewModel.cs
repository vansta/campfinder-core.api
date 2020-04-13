using System;

namespace CampFinder.ViewModels
{
    public class BuildingOverviewItemViewModel : OverviewItemViewModel
    {
        public string Type
        {
            get { return "building"; }
        }
        public int Dormitories { get; set; }
        public bool Beds { get; set; }
    }
}
