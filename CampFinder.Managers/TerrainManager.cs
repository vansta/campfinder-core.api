using System;
using System.Collections.Generic;
using System.Linq;
using CampFinder.AutoMapperConfiguration;
using CampFinder.Models;
using CampFinder.Repositories;
using CampFinder.ViewModels;

namespace CampFinder.Managers
{
    public class TerrainManager
    {
        private readonly CampPlaceRepository repository = new CampPlaceRepository();

        public IEnumerable<TerrainOverviewItemViewModel> GetTerrainViewModels()
        {
            IEnumerable<Terrain> terrains = repository.GetTerrains();
            List<TerrainOverviewItemViewModel> terrainViewModels = new List<TerrainOverviewItemViewModel>();
            foreach (Terrain terrain in terrains)
            {
                terrainViewModels.Add(new MapperService<TerrainOverviewItemViewModel>().Map(terrain));
            }
            return terrainViewModels;
        }

        public TerrainViewModel GetTerrainViewModel(Guid Id)
        {
            Terrain terrain = repository.GetTerrain(Id);
            return MapTerrainToViewModel(terrain);
        }

        public IEnumerable<TerrainOverviewItemViewModel> GetTerrainsForSearch(TerrainSearchViewModel terrainSearch)
        {
            List<TerrainOverviewItemViewModel> filteredTerrains = new List<TerrainOverviewItemViewModel>();
            IQueryable<Terrain> terrains = repository.GetTerrains();
            if (terrainSearch != null)
            {
                if (!string.IsNullOrEmpty(terrainSearch.Name))
                {
                    terrains = terrains.Where(t => t.Name == terrainSearch.Name);
                }
                if (terrainSearch.AmountPersons != null && int.TryParse(terrainSearch.AmountPersons, out int amountPersons))
                {
                    terrains = terrains.Where(t => t.AmountPersons >= amountPersons);
                }
                if (terrainSearch.Province != null && terrainSearch.Province.Count() > 0)
                {
                    terrains = terrains.Where(t => terrainSearch.Province.Any(p => p == t.Place.Province));
                }
                if (terrainSearch.Foreign)
                {
                    terrains = terrains.Where(t => t.Place.Country.ToUpper() == "BELGIE");
                }
                if (terrainSearch.Forest)
                {
                    terrains = terrains.Where(t => t.Forest);
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

            foreach (Terrain terrain in terrains)
            {
                filteredTerrains.Add(new MapperService<TerrainOverviewItemViewModel>().Map(terrain));
            }
            return filteredTerrains;
        }

        public void PostNewTerrain(TerrainViewModel terrainViewModel)
        {
            Terrain terrain = MapViewModelToTerrain(terrainViewModel);
            repository.PostNewTerrain(terrain);
        }

        #region Mapper

        private Terrain MapViewModelToTerrain(TerrainViewModel terrainViewModel)
        {
            Terrain terrain = new MapperService<Terrain>().Map(terrainViewModel);
            terrain.Person = terrainViewModel.Person == null ? null : new MapperService<Person>().Map(terrainViewModel.Person);
            terrain.Place = terrainViewModel.Place == null ? null : new MapperService<Place>().Map(terrainViewModel.Place);
            return terrain;
        }

        private TerrainViewModel MapTerrainToViewModel(Terrain terrain)
        {
            TerrainViewModel terrainViewModel = new MapperService<TerrainViewModel>().Map(terrain);
            terrainViewModel.Person = terrain.Person == null ? null : new MapperService<PersonViewModel>().Map(terrain.Person);
            terrainViewModel.Place = terrain.Place == null ? null : new MapperService<PlaceViewModel>().Map(terrain.Place);
            return terrainViewModel;
        }

        #endregion Mapper
    }
}
