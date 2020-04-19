﻿using System;
using System.Collections.Generic;
using System.Linq;
using CampFinder.AutoMapperConfiguration;
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
                IEnumerable<Terrain> terrains = repository.Get<Terrain>();
                List<TerrainOverviewItemViewModel> terrainViewModels = new List<TerrainOverviewItemViewModel>();
                foreach (Terrain terrain in terrains)
                {
                    terrainViewModels.Add(new MapperService<TerrainOverviewItemViewModel>().Map(terrain));
                }
                return terrainViewModels;
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                return null;
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
                return null;
            }
        }

        public IEnumerable<TerrainOverviewItemViewModel> GetTerrainsForSearch(TerrainSearchViewModel terrainSearch)
        {
            List<TerrainOverviewItemViewModel> filteredTerrains = new List<TerrainOverviewItemViewModel>();

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

                foreach (Terrain terrain in terrains)
                {
                    filteredTerrains.Add(new MapperService<TerrainOverviewItemViewModel>().Map(terrain));
                }
            }
            catch(Exception ex)
            {
                LogErrors(ex);
            }
            return filteredTerrains;
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
            }
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
