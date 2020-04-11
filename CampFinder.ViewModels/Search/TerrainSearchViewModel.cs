using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.ViewModels
{
    public class TerrainSearchViewModel : SearchViewModel
    {
        public bool Toilets { get; set; }
        public bool Water { get; set; }
        public bool Electricity { get; set; }
    }
}
