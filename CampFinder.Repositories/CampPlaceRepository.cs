using System;
using CampFinder.Models;
using CampFinder.DbContext;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CampFinder.Repositories
{
    //public class CampPlaceRepository : BaseRepository
    //{
    //    //private readonly CampFinderDbContext context = new CampFinderDbContext();
    //    public CampPlaceRepository(IConfiguration _configuration) : base(_configuration) { }

    //    //public void Dispose()
    //    //{
    //    //    using (CampFinderDbContext context = dbContextFactory.CreateDbContext())
    //    //    {
    //    //        context.di
    //    //    }
    //    //    context.Dispose();
    //    //}

    //    public IQueryable<T> Get<T>() where T:CampPlace
    //    {
    //        using (CampFinderDbContext context = dbContextFactory.CreateDbContext())
    //        {
    //            return context.Set<T>()
    //                .Include(c => c.Place)
    //                .Include(c => c.Reviews);
    //        }
    //    }

    //    public async Task<T> GetById<T>(Guid id) where T : CampPlace
    //    {
    //        using CampFinderDbContext context = dbContextFactory.CreateDbContext();
    //        return await context.Set<T>()
    //            .Include(c => c.Person)
    //            .Include(c => c.Place)
    //            .Include(c => c.Reviews)
    //            .SingleAsync(c => c.Id == id);

    //    }

    //    public async Task PostNew<T>(T campPlace) where T : CampPlace
    //    {
    //        using CampFinderDbContext context = dbContextFactory.CreateDbContext();
    //        await context.Set<T>().AddAsync(campPlace);
    //        await context.SaveChangesAsync();
    //    }

    //    public async Task UpdateCampPlace<T>(T campPlace) where T : CampPlace
    //    {
    //        using CampFinderDbContext context = dbContextFactory.CreateDbContext();
    //        context.Set<T>().Update(campPlace);
    //        await context.SaveChangesAsync();
    //    }

    //    public async Task Delete<T>(T campPlace) where T : CampPlace
    //    {
    //        using CampFinderDbContext context = dbContextFactory.CreateDbContext();
    //        context.Set<T>().Remove(campPlace);
    //        await context.SaveChangesAsync();
    //    }

    //    public async Task DeletePerson(Person person)
    //    {
    //        using CampFinderDbContext context = dbContextFactory.CreateDbContext();
    //        context.People.Remove(person);
    //        await context.SaveChangesAsync();
    //    }

    //    public async Task DeletePlace(Place place)
    //    {
    //        using CampFinderDbContext context = dbContextFactory.CreateDbContext();
    //        context.Places.Remove(place);
    //        await context.SaveChangesAsync();
    //    }
    //}
}
