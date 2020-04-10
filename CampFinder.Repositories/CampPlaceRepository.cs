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
        public IQueryable<Building> GetBuildings()
        {
            return context.Buildings.Include(nameof(Building.Place)).Include(nameof(Building.Reviews));
        }
        public Building GetBuilding(Guid? id)
        {
            return context.Buildings.Include(t => t.Person).Include(t => t.Place).Include(t => t.Reviews).SingleOrDefault(t => t.Id == id);
        }
        public void Dispose()
        {
            context.Dispose();
        }
        public IQueryable<Terrain> GetTerrains()
        {
            return context.Terrains.Include("Place").Include("Reviews");
        }
        public Terrain GetTerrain(Guid? id)
        {
            return context.Terrains.Include(t => t.Person).Include(t => t.Place).Include(t => t.Reviews).SingleOrDefault(t => t.Id == id);
        }

        public void PostNewTerrain(Terrain terrain)
        {
            CampFinderDbContext dbContext = new CampFinderDbContext();
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                context.Terrains.Add(terrain);
                context.SaveChangesAsync();
            }
        }

        public void PostNewBuilding(Building building)
        {
            CampFinderDbContext dbContext = new CampFinderDbContext();
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                context.Buildings.Add(building);
                context.SaveChangesAsync();
            }
        }
    }
}
