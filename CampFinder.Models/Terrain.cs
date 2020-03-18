using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.Models
{
    public class Terrain : CampPlace
    {
        public bool Water { get; set; }
        public bool Electricity { get; set; }
        public bool Toilets { get; set; }
    }
}
