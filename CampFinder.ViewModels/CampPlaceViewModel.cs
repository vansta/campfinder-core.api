using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.ViewModels
{
    public abstract class CampPlaceViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string AmountPersons { get; set; }
        public bool Forest { get; set; }
        public string Area { get; set; }

        public PersonViewModel Person { get; set; }
        public PlaceViewModel Place { get; set; }
        public ICollection<ReviewViewModel> Reviews { get; set; }

        public double AverageScore { get; set; }
    }
}
