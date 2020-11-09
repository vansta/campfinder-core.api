using System;
using System.Collections.Generic;
using System.Linq;
using CampFinder.Models;
using CampFinder.Repositories;
using CampFinder.ViewModels;

namespace CampFinder.Managers
{
    public class BuildingManager: CampPlaceManager<Building>
    {
        private readonly CampPlaceRepository repository = new CampPlaceRepository();

        public IEnumerable<BuildingOverviewItemViewModel> GetBuildingOverview()
        {
            try
            {
                return repository.Get<Building>().Select(b => mapper.Map<BuildingOverviewItemViewModel>(b));
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                throw ex;
            }
        }

        public void PostNewBuilding(BuildingViewModel buildingViewModel)
        {
            try
            {
                Building building = MapViewModelToModel(buildingViewModel);
                repository.PostNew(building);
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                throw ex;
            }
        }

        public IEnumerable<BuildingOverviewItemViewModel> PostBuildingSearch(BuildingSearchViewModel buildingSearch)
        {
            IQueryable<Building> buildings = new List<Building>().AsQueryable();
            try
            {
                if (buildingSearch != null)
                {
                    buildings = GetSearch(buildingSearch);
                    if (buildingSearch.Beds)
                    {
                        buildings = buildings.Where(b => b.Beds);
                    }
                    if (buildingSearch.KitchenGear)
                    {
                        buildings = buildings.Where(b => b.KitchenGear);
                    }
                }
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                throw ex;
            }
            return buildings.Select(b => mapper.Map<BuildingOverviewItemViewModel>(b));
        }

        public BuildingViewModel GetBuildingViewModel(Guid Id)
        {
            try
            {
                Building building = repository.GetById<Building>(Id);
                return MapModelToViewModel<BuildingViewModel>(building);
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                throw ex;
            }
        }

        public void UpdateBuilding(BuildingViewModel buildingViewModel)
        {
            try
            {
                Building building = MapViewModelToModel(buildingViewModel);
                repository.UpdateCampPlace(building);
            }
            catch (Exception ex)
            {
                LogErrors(ex);
                throw ex;
            }
        }
    }
}
