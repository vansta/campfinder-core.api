using CampFinder.DbContext;
using CampFinder.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampFinder.Repositories
{
    //public class ReviewRepository : BaseRepository
    //{
    //    public ReviewRepository(IConfiguration _configuration) : base(_configuration) { }
    //    //private readonly CampFinderDbContext context = new CampFinderDbContext();
    //    public IQueryable<Review> GetReviewsById(Guid id)
    //    {
    //        using CampFinderDbContext context = dbContextFactory.CreateDbContext();
    //        return context.Reviews.Where(r => r.CampPlaceId == id);
    //    }

    //    public async Task PostnewReview(Review review)
    //    {
    //        using CampFinderDbContext context = dbContextFactory.CreateDbContext();
    //        context.Reviews.Add(review);
    //        await context.SaveChangesAsync();
    //    }

    //    public async Task Delete(IEnumerable<Review> reviews)
    //    {
    //        using CampFinderDbContext context = dbContextFactory.CreateDbContext();
    //        context.Reviews.RemoveRange(reviews);
    //        await context.SaveChangesAsync();
    //    }
    //}
}
