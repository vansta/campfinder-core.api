using System;

namespace CampFinder.Models
{
    public class Building: CampPlace
    {
        public int Dormitories { get; set; }
        public bool KitchenGear { get; set; }
        public bool Beds { get; set; }
        public int DaySpaces { get; set; }
    }
}
