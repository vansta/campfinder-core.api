using System;
using CampFinder.Models;
using CampFinder.DbContext;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CampFinder.Repositories
{
    public class CampPlaceRepository
    {
        private readonly CampFinderDbContext context = new CampFinderDbContext();

        public void Dispose()
        {
            context.Dispose();
        }

        public IQueryable<T> Get<T>() where T:CampPlace
        {
            return context.Set<T>().Include(c => c.Place).Include(c => c.Reviews);
        }

        public T GetById<T>(Guid id) where T : CampPlace
        {
            return context.Set<T>().Include(c => c.Person).Include(c => c.Place).Include(c => c.Reviews).Single(c => c.Id == id);
        }

        public void PostNew<T>(T campPlace) where T : CampPlace
        {
            CampFinderDbContext dbContext = new CampFinderDbContext();
            using var transaction = dbContext.Database.BeginTransactionAsync();
            context.Set<T>().Add(campPlace);
            context.SaveChangesAsync();
        }
    }
}
