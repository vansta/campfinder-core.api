using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public double Score { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }

        public Person Person { get; set; }
        public CampPlace CampPlace { get; set; }
    }
}
