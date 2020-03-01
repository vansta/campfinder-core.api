using System;

namespace CampFinder.ViewModels
{
    public class BuildingOverviewItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Dormitories { get; set; }
        public int AmountPersons { get; set; }
        public string City { get; set; }
        public string Website { get; set; }
    }
}
