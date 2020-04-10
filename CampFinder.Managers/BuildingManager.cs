using System;
using System.Collections.Generic;
using System.Linq;
using CampFinder.AutoMapperConfiguration;
using CampFinder.Models;
using CampFinder.Repositories;
using CampFinder.ViewModels;

namespace CampFinder.Managers
{
    public class BuildingManager: CampPlaceManager<Building>
    {
        private readonly CampPlaceRepository repository = new CampPlaceRepository();

        public IEnumerable<Building> GetBuildings()
        {
            return repository.Get<Building>();
        }

        public IEnumerable<BuildingOverviewItemViewModel> GetBuildingOverview()
        {
            IEnumerable<Building> Buildings = repository.Get<Building>();
            List<BuildingOverviewItemViewModel> BuildingOverview = new List<BuildingOverviewItemViewModel>();
            foreach (Building building in Buildings)
            {
                BuildingOverview.Add(new MapperService<BuildingOverviewItemViewModel>().Map(building));
            }
            return BuildingOverview;
        }

        public void PostNewBuilding(BuildingViewModel buildingViewModel)
        {
            Building building = MapViewModelToModel(buildingViewModel);
            repository.PostNew(building);
        }

        public IEnumerable<BuildingOverviewItemViewModel> PostBuildingSearch(BuildingSearchViewModel buildingSearch)
        {
            List<BuildingOverviewItemViewModel> filteredBuildings = new List<BuildingOverviewItemViewModel>();
            IQueryable<Building> buildings = new List<Building>().AsQueryable();

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
            return filteredBuildings;
        }

        public BuildingViewModel GetBuildingViewModel(Guid Id)
        {
            Building building = repository.GetById<Building>(Id);
            BuildingViewModel buildingViewModel = MapModelToViewModel<BuildingViewModel>(building);
            return buildingViewModel;
        }
    }
}
