using System;
using System.Collections.Generic;
using System.Linq;
using CampFinder.AutoMapperConfiguration;
using CampFinder.Models;
using CampFinder.Repositories;
using CampFinder.ViewModels;

namespace CampFinder.Managers
{
    public class BuildingManager
    {
        private readonly CampPlaceRepository repository = new CampPlaceRepository();

        public IEnumerable<Building> GetBuildings()
        {
            return repository.GetBuildings();
        }
        public IEnumerable<BuildingOverviewItemViewModel> GetBuildingOverview()
        {
            IEnumerable<Building> Buildings = repository.GetBuildings();
            List<BuildingOverviewItemViewModel> BuildingOverview = new List<BuildingOverviewItemViewModel>();
            foreach (Building building in Buildings)
            {
                BuildingOverview.Add(new MapperService<BuildingOverviewItemViewModel>().Map(building));
            }
            return BuildingOverview;
        }

        public void PostNewBuilding(BuildingViewModel buildingViewModel)
        {
            Building building = MapViewModelToBuilding(buildingViewModel);
            repository.PostNewBuilding(building);
        }

        public IEnumerable<BuildingOverviewItemViewModel> PostBuildingSearch(BuildingSearchViewModel buildingSearch)
        {
            List<BuildingOverviewItemViewModel> filteredBuildings = new List<BuildingOverviewItemViewModel>();
            IQueryable<Building> buildings = repository.GetBuildings();

            if (buildingSearch != null)
            {
                if (!string.IsNullOrEmpty(buildingSearch.Name))
                {
                    buildings = buildings.Where(b => b.Name == buildingSearch.Name);
                }
                if (buildingSearch.AmountPersons != null && int.TryParse(buildingSearch.AmountPersons, out int amountPersons))
                {
                    buildings = buildings.Where(b => b.AmountPersons >= amountPersons);
                }
                if (buildingSearch.Province != null && buildingSearch.Province.Count() > 0)
                {
                    buildings = buildings.Where(b => buildingSearch.Province.Any(p => p == b.Place.Province));
                }
                if (buildingSearch.Forest)
                {
                    buildings = buildings.Where(b => b.Forest);
                }
                if (buildingSearch.Foreign)
                {
                    buildings = buildings.Where(b => b.Place.Country.ToUpper() == "BELGIE");
                }
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
            Building building = repository.GetBuilding(Id);
            BuildingViewModel buildingViewModel = MapBuildingToViewModel(building);
            return buildingViewModel;
        }

        #region Mappers

        private Building MapViewModelToBuilding(BuildingViewModel buildingViewModel)
        {
            Building building = new MapperService<Building>().Map(buildingViewModel);
            building.Person = buildingViewModel.Person == null ? null : new MapperService<Person>().Map(building.Person);
            building.Place = buildingViewModel.Place == null ? null : new MapperService<Place>().Map(building.Place);
            return building;
        }

        private BuildingViewModel MapBuildingToViewModel(Building building)
        {
            BuildingViewModel buildingViewModel = new MapperService<BuildingViewModel>().Map(building);
            buildingViewModel.Person = building.Person == null ? null : new MapperService<PersonViewModel>().Map(building.Person);
            buildingViewModel.Place = building.Place == null ? null : new MapperService<PlaceViewModel>().Map(building.Place);
            return buildingViewModel;
        }

        #endregion Mappers
    }
}
