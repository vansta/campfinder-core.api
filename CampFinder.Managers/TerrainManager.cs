using System;
using System.Collections.Generic;
using System.Linq;
using CampFinder.Models;
using CampFinder.Repositories;
using CampFinder.ViewModels;

namespace CampFinder.Managers
{
    public class TerrainManager: CampPlaceManager<Terrain>
    {
        private readonly CampPlaceRepository repository = new CampPlaceRepository();

        public IEnumerable<TerrainOverviewItemViewModel> GetTerrainViewModels()
        {
            try
            {
                return repository.Get<Terrain>().Select(t => mapper.Map<TerrainOverviewItemViewModel>(t));
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                throw ex;
            }
        }

        public TerrainViewModel GetTerrainViewModel(Guid Id)
        {
            try
            {
                Terrain terrain = repository.GetById<Terrain>(Id);
                return MapModelToViewModel<TerrainViewModel>(terrain);
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                throw ex;
            }
        }

        public IEnumerable<TerrainOverviewItemViewModel> GetTerrainsForSearch(TerrainSearchViewModel terrainSearch)
        {
            IQueryable<Terrain> terrains = new List<Terrain>().AsQueryable();
            try
            {
                if (terrainSearch != null)
                {
                    terrains = GetSearch(terrainSearch);

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
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                throw ex;
            }
            return terrains.Select(t => mapper.Map<TerrainOverviewItemViewModel>(t));
        }

        public void PostNewTerrain(TerrainViewModel terrainViewModel)
        {
            try
            {
                Terrain terrain = MapViewModelToModel(terrainViewModel);
                repository.PostNew(terrain);
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                throw ex;
            }
        }

        public void UpdateTerrain(TerrainViewModel terrainViewModel)
        {
            try
            {
                Terrain terrain = MapViewModelToModel(terrainViewModel);
                repository.UpdateCampPlace(terrain);
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                throw ex;
            }
        }
    }
}
