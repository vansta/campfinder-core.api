using System;
using System.Collections.Generic;
using System.Linq;
using CampFinder.Models;
using CampFinder.Repositories;
using CampFinder.ViewModels;

namespace CampFinder.Managers
{
    public class TerrainManager
    {
        private readonly CampPlaceRepository repository = new CampPlaceRepository();

        private readonly PlaceManager placeManager = new PlaceManager();
        private readonly PersonManager personManager = new PersonManager();

        //public IEnumerable<TerrainOverviewItemViewModel> GetTerrainViewModels()
        //{
        //    IEnumerable<Terrain> terrains = repository.GetTerrains();
        //    List<TerrainOverviewItemViewModel> terrainViewModels = new List<TerrainOverviewItemViewModel>();
        //    foreach (Terrain terrain in terrains)
        //    {
        //        terrainViewModels.Add(MapTerrainToOverViewItemViewModel(terrain));
        //    }
        //    return terrainViewModels;
        //}

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
                if (terrainSearch.AmountPersons != null)
                {
                    terrains = terrains.Where(t => t.AmountPersons >= terrainSearch.AmountPersons);
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
                    terrains = terrains.Where(t => t.Electriciy);
                }
            }

            foreach (Terrain terrain in terrains)
            {
                filteredTerrains.Add(MapTerrainToOverViewItemViewModel(terrain));
            }
            return filteredTerrains;
        }

        public void PostNewTerrain(TerrainViewModel terrainViewModel)
        {
            Terrain terrain = MapViewModelToTerrain(terrainViewModel);
            repository.PostNewTerrain(terrain);
        }

        #region Mapper

        private Terrain MapViewModelToTerrain(TerrainViewModel terrain)
        {
            return new Terrain
            {
                Id = Guid.NewGuid(),
                Name = terrain.Name,
                Website = terrain.Website,
                AmountPersons = terrain.AmountPersons,
                Forest = terrain.Forest,
                Area = terrain.Area,
                Electriciy = terrain.Electricity,
                Water = terrain.Water,
                Toilets = terrain.Toilets,
                Place = terrain.Place == null ? null : placeManager.MapViewModelToPlace(terrain.Place),
                Person = terrain.Person == null ? null : personManager.MapViewModelToPerson(terrain.Person)
            };
        }

        private TerrainViewModel MapTerrainToViewModel(Terrain terrain)
        {
            return new TerrainViewModel
            {
                Id = terrain.Id,
                Name = terrain.Name,
                Website = terrain.Website,
                AmountPersons = terrain.AmountPersons,
                Forest = terrain.Forest,
                Area = terrain.Area,

                Water = terrain.Water,
                Electricity = terrain.Electriciy,
                Toilets = terrain.Toilets,

                Place = terrain.Place == null ? null : placeManager.MapPlaceToViewModel(terrain.Place),
                Person = terrain.Person == null ? null : personManager.MapPersonToViewModel(terrain.Person)
            };
        }

        private TerrainOverviewItemViewModel MapTerrainToOverViewItemViewModel(Terrain terrain)
        {
            return new TerrainOverviewItemViewModel
            {
                Id = terrain.Id,
                Name = terrain.Name,
                Water = terrain.Water,
                AmountPersons = terrain.AmountPersons,
                Website = terrain.Website,
                Area = terrain.Area,
                City = terrain.Place.City
            };
        }

        #endregion Mapper
    }
}
