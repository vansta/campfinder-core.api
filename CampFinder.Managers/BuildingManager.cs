using System;
using System.Collections.Generic;
using System.Linq;
using CampFinder.AutoMapperConfiguration;
using CampFinder.Models;
using CampFinder.Repositories;
using CampFinder.ViewModels;
using Serilog;

namespace CampFinder.Managers
{
    public class BuildingManager: CampPlaceManager<Building>
    {
        private readonly CampPlaceRepository repository = new CampPlaceRepository();

        public IEnumerable<Building> GetBuildings()
        {
            try
            {
                return repository.Get<Building>();
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                return null;
            }
        }

        public IEnumerable<BuildingOverviewItemViewModel> GetBuildingOverview()
        {
            try
            {
                IEnumerable<Building> Buildings = repository.Get<Building>();
                List<BuildingOverviewItemViewModel> BuildingOverview = new List<BuildingOverviewItemViewModel>();
                foreach (Building building in Buildings)
                {
                    BuildingOverview.Add(new MapperService<BuildingOverviewItemViewModel>().Map(building));
                }
                return BuildingOverview;
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                return null;
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
            }
        }

        public IEnumerable<BuildingOverviewItemViewModel> PostBuildingSearch(BuildingSearchViewModel buildingSearch)
        {
            List<BuildingOverviewItemViewModel> filteredBuildings = new List<BuildingOverviewItemViewModel>();
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

                foreach (Building building in buildings)
                {
                    filteredBuildings.Add(new MapperService<BuildingOverviewItemViewModel>().Map(building));
                }
            }
            catch(Exception ex)
            {
                LogErrors(ex);
            }
            return filteredBuildings;
        }

        public BuildingViewModel GetBuildingViewModel(Guid Id)
        {
            try
            {
                Building building = repository.GetById<Building>(Id);
                BuildingViewModel buildingViewModel = MapModelToViewModel<BuildingViewModel>(building);
                return buildingViewModel;
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                return null;
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
            }
        }
    }
}
