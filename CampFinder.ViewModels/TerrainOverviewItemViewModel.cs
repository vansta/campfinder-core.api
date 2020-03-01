using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.ViewModels
{
    public class TerrainOverviewItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public int AmountPersons { get; set; }
        public string City { get; set; }
        public string Website { get; set; }
        public bool Water { get; set; }
    }
}
