using System;
using System.Collections.Generic;
using System.Linq;
using CampFinder.Models;
using CampFinder.Repositories;
using CampFinder.ViewModels;

namespace CampFinder.Managers
{
    public class BuildingManager
    {
        private readonly CampPlaceRepository repository = new CampPlaceRepository();

        private readonly PlaceManager placeManager = new PlaceManager();
        private readonly PersonManager personManager = new PersonManager();

        public IEnumerable<Building> GetBuildings()
        {
            return repository.GetBuildings();
        }
        //public IEnumerable<BuildingOverviewItemViewModel> GetBuildingOverview()
        //{
        //    IEnumerable<Building> Buildings = repository.GetBuildings();
        //    List<BuildingOverviewItemViewModel> BuildingOverview = new List<BuildingOverviewItemViewModel>();
        //    foreach (Building building in Buildings)
        //    {
        //        BuildingOverview.Add(MapBuildingToOverViewItemViewModel(building));
        //    }
        //    return BuildingOverview;
        //}

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
                if (buildingSearch.AmountPersons != null)
                {
                    buildings = buildings.Where(b => b.AmountPersons >= buildingSearch.AmountPersons);
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
                filteredBuildings.Add(MapBuildingToOverViewItemViewModel(building));
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
            return new Building
            {
                Id = Guid.NewGuid(),
                Name = buildingViewModel.Name,
                Website = buildingViewModel.Website,
                AmountPersons = buildingViewModel.AmountPersons,
                Forest = buildingViewModel.Forest,
                Area = buildingViewModel.Area,
                Dormitories = buildingViewModel.Dormitories,
                DaySpaces = buildingViewModel.DaySpaces,
                KitchenGear = buildingViewModel.KitchenGear,
                Beds = buildingViewModel.Beds,
                Person = buildingViewModel.Person == null ? null : personManager.MapViewModelToPerson(buildingViewModel.Person),
                Place = buildingViewModel.Place == null ? null : placeManager.MapViewModelToPlace(buildingViewModel.Place)
            };
        }

        private BuildingViewModel MapBuildingToViewModel(Building building)
        {
            return new BuildingViewModel
            {
                Id = building.Id,
                Name = building.Name,
                Website = building.Website,
                Dormitories = building.Dormitories,
                AmountPersons = building.AmountPersons,
                Forest = building.Forest,
                Area = building.Area,
                KitchenGear = building.KitchenGear,
                Beds = building.Beds,
                DaySpaces = building.DaySpaces,
                Place = building.Place == null ? null : placeManager.MapPlaceToViewModel(building.Place),
                Person = building.Person == null ? null : personManager.MapPersonToViewModel(building.Person)
            };
        }

        private BuildingOverviewItemViewModel MapBuildingToOverViewItemViewModel(Building building)
        {
            return new BuildingOverviewItemViewModel
            {
                Id = building.Id,
                Name = building.Name,
                Dormitories = building.Dormitories,
                AmountPersons = building.AmountPersons,
                City = building.Place.City,
                Website = building.Website
            };
        }

        #endregion Mappers
    }
}
