using CampFinder.DbContext;
using CampFinder.Models;
using CampFinder.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampFinder.Managers
{
    public class ReviewManager : BaseManager
    {
        public ReviewManager(IConfiguration configuration) : base(configuration)
        {

        }
        public IEnumerable<ReviewViewModel> GetReviewsById(Guid id)
        {
            using CampFinderDbContext context = dbContextFactory.CreateDbContext();
            return context.Reviews.Where(r => r.CampPlaceId.Equals(id)).Select(r => mapper.Map<ReviewViewModel>(r)).ToList();
        }

        public async Task<ReviewViewModel> PostNewReview(ReviewViewModel reviewViewModel)
        {
            Review review = mapper.Map<Review>(reviewViewModel);
            using CampFinderDbContext context = dbContextFactory.CreateDbContext();
            review = (await context.Reviews.AddAsync(review)).Entity;
            await context.SaveChangesAsync();
            return mapper.Map<ReviewViewModel>(review);
        }
    }
}
