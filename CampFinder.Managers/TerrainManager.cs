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
    public class TerrainManager : CampPlaceManager<Terrain>
    {
        public TerrainManager(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<TerrainViewModel> GetTerrainViewModel(Guid id)
        {
            using CampFinderDbContext context = dbContextFactory.CreateDbContext();
            return mapper.Map<TerrainViewModel>(await context.Terrains
                .Include(b => b.Person)
                .Include(b => b.Reviews)
                .Include(b => b.Place)
                .SingleAsync(t => t.Id.Equals(id)));
        }

        public IEnumerable<TerrainOverviewItemViewModel> GetTerrainsForSearch(TerrainSearchViewModel terrainSearch)
        {
            using CampFinderDbContext context = dbContextFactory.CreateDbContext();
            IQueryable<Terrain> terrains = context.Terrains
                .Include(t => t.Reviews)
                .Include(t => t.Place);

            if (terrainSearch != null)
            {
                terrains = GetSearch(terrains, terrainSearch);

                if (terrainSearch.Toilets)
                {
                    terrains = terrains.Where(t => t.Toilets);
                }
                if (terrainSearch.Water)
                {
                    terrains = terrains.Where(t => t.Water);
                }
                if (terrainSearch.Electricity)
                {
                    terrains = terrains.Where(t => t.Electricity);
                }
            }
            return terrains.Select(t => mapper.Map<TerrainOverviewItemViewModel>(t)).ToList();
        }

        public async Task PostNewTerrain(TerrainViewModel terrainViewModel)
        {
            using CampFinderDbContext context = dbContextFactory.CreateDbContext();
            await context.Terrains.AddAsync(mapper.Map<Terrain>(terrainViewModel));
            await context.SaveChangesAsync();
        }

        public async Task UpdateTerrain(TerrainViewModel terrainViewModel)
        {
            using CampFinderDbContext context = dbContextFactory.CreateDbContext();
            context.Terrains.Update(mapper.Map<Terrain>(terrainViewModel));
            await context.SaveChangesAsync();
        }
    }
}
