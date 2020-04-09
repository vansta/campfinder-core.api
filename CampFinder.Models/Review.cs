using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Column("CampPlace_Id")]
        public Guid CampPlaceId { get; set; }
        public CampPlace CampPlace { get; set; }
    }
}
