using CampFinder.DbContext;
using CampFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CampFinder.Repositories
{
    public class ReviewRepository
    {
        private readonly CampFinderDbContext context = new CampFinderDbContext();
        public IEnumerable<Review> GetReviewsById(Guid id)
        {
            return context.Reviews.Where(r => r.CampPlaceId == id);
        }

        public void PostnewReview(Review review)
        {
            context.Reviews.Add(review);
        }
    }
}
