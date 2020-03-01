using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.ViewModels
{
    public class TerrainViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public int AmountPersons { get; set; }
        public bool Forest { get; set; }
        public double Area { get; set; }

        public bool Water { get; set; }
        public bool Electricity { get; set; }
        public bool Toilets { get; set; }

        public PersonViewModel Person { get; set; }
        public PlaceViewModel Place { get; set; }
        public ICollection<ReviewViewModel> Reviews { get; set; }
    }
}
