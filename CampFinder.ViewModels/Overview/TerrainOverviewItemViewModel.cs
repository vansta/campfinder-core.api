using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.ViewModels
{
    public class TerrainOverviewItemViewModel: OverviewItemViewModel
    {
        public string Type
        {
            get { return "terrain"; }
        }
        public bool Water { get; set; }
    }
}
