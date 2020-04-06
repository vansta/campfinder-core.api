using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.ViewModels
{
    public class TerrainViewModel: CampPlaceViewModel
    {
        public bool Water { get; set; }
        public bool Electricity { get; set; }
        public bool Toilets { get; set; }
    }
}
