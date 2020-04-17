using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.ViewModels
{
    public abstract class OverviewItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AmountPersons { get; set; }
        public string City { get; set; }
        public string Website { get; set; }

        public double AverageScore { get; set; }
    }
}
