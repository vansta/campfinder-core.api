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
        public double? Area { get; set; }

        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }
        public Guid PlaceId { get; set; }
        public virtual Place Place { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public double AverageScore
        {
            get{
                double averageScore = 0;
                if (Reviews != null && Reviews.Count > 0)
                {
                    averageScore = Reviews.Select(r => r.Score).Average();
                }
                return averageScore;
            }
        }
    }
}
