using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampFinder.DbContext;
using CampFinder.Models;
using CampFinder.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CampFinder.Managers
{
    public class BuildingManager : CampPlaceManager<Building>
    {
        public BuildingManager(IConfiguration configuration) : base(configuration)
        {
            
        }

        public async Task PostNewBuilding(BuildingViewModel buildingViewModel)
        {
            using CampFinderDbContext context = dbContextFactory.CreateDbContext();
            await context.Buildings.AddAsync(mapper.Map<Building>(buildingViewModel));
            await context.SaveChangesAsync();
        }

        public IEnumerable<BuildingOverviewItemViewModel> GetBuildingSearch(BuildingSearchViewModel buildingSearch)
        {
            using CampFinderDbContext context = dbContextFactory.CreateDbContext();
            IQueryable<Building> buildings = context.Buildings
                .Include(t => t.Reviews)
                .Include(t => t.Place);

            if (buildingSearch != null)
            {
                buildings = GetSearch(buildings, buildingSearch);
                if (buildingSearch.Beds)
                {
                    buildings = buildings.Where(b => b.Beds);
                }
                if (buildingSearch.KitchenGear)
                {
                    buildings = buildings.Where(b => b.KitchenGear);
                }

            }
            return buildings.Select(b => mapper.Map<BuildingOverviewItemViewModel>(b)).ToList();
        }

        public async Task<BuildingViewModel> GetBuildingViewModel(Guid id)
        {
            using CampFinderDbContext context = dbContextFactory.CreateDbContext();
            Building building = await context.Buildings
                .Include(b => b.Person)
                .Include(b => b.Reviews)
                .Include(b => b.Place)
                .SingleAsync(b => b.Id.Equals(id));

            return mapper.Map<BuildingViewModel>(building);
        }

        public async Task UpdateBuilding(BuildingViewModel buildingViewModel)
        {
            using CampFinderDbContext context = dbContextFactory.CreateDbContext();
            context.Buildings.Update(mapper.Map<Building>(buildingViewModel));
            await context.SaveChangesAsync();
        }
    }
}
