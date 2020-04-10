using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CampFinder.Models
{
    public abstract class CampPlace
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public int AmountPersons { get; set; }
        public bool Forest { get; set; }
        public double Area { get; set; }

        [ForeignKey("Person_Id")]
        public virtual Person Person { get; set; }
        [ForeignKey("Place_Id")]
        public virtual Place Place { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public double GetAverageScore()
        {
            if (Reviews.Count == 0)
            {
                return 0;
            }
            else
            {
                return Reviews.Select(r => r.Score).Average();
            }                       
        }
    }
}
