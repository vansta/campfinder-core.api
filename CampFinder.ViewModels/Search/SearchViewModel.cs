using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.ViewModels
{
    public class SearchViewModel
    {
        public string Name { get; set; }
        public string AmountPersons { get; set; }
        public IEnumerable<string> Province { get; set; }
        public bool Foreign { get; set; }
        public bool Forest { get; set; }
        public double MinimumScore  { get;set; } 
        public double Accessibility { get; set; }
        public string AccessibilityNote { get; set; }
    }
}
