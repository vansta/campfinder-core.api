﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.ViewModels
{
    public class SearchViewModel
    {
        public string Name { get; set; }
        public int? AmountPersons { get; set; }
        public IEnumerable<string> Province { get; set; }
        public bool Foreign { get; set; }
        public bool Forest { get; set; }
    }
}