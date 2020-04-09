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

        private readonly PlaceManager placeManager = new PlaceManager();
        private readonly PersonManager personManager = new PersonManager();

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
                BuildingOverview.Add(MapBuildingToOverViewItemViewModel(building));
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
                filteredBuildings.Add(MapBuildingToOverViewItemViewModel(building));
            }
            return filteredBuildings;
        }

        public object GetAllBuildings()
        {
            List<BuildingOverviewItemViewModel> buildingViewModels = new List<BuildingOverviewItemViewModel>();
            IQueryable<Building> buildings  =  repository.GetBuildings();
            foreach(Building building in buildings)
            {
                buildingViewModels.Add(MapBuildingToOverViewItemViewModel(building));
            }
            return buildingViewModels;
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
            //building.Person = buildingViewModel.Person == null ? null : personManager.MapViewModelToPerson(buildingViewModel.Person);
            //building.Place = buildingViewModel.Place == null ? null : placeManager.MapViewModelToPlace(buildingViewModel.Place);

            building.Person = buildingViewModel.Person == null ? null : new MapperService<Person>().Map(building.Person);
            building.Place = buildingViewModel.Place == null ? null : new MapperService<Place>().Map(building.Place);
            return building;

            //return new Building
            //{
            //    Id = Guid.NewGuid(),
            //    Name = buildingViewModel.Name,
            //    Website = buildingViewModel.Website,
            //    AmountPersons = int.Parse(buildingViewModel.AmountPersons),
            //    Forest = buildingViewModel.Forest,
            //    Area = double.Parse(buildingViewModel.Area),
            //    Dormitories = int.Parse(buildingViewModel.Dormitories),
            //    DaySpaces = int.Parse(buildingViewModel.DaySpaces),
            //    KitchenGear = buildingViewModel.KitchenGear,
            //    Beds = buildingViewModel.Beds,
            //    Person = buildingViewModel.Person == null ? null : personManager.MapViewModelToPerson(buildingViewModel.Person),
            //    Place = buildingViewModel.Place == null ? null : placeManager.MapViewModelToPlace(buildingViewModel.Place)
            //};
        }

        private BuildingViewModel MapBuildingToViewModel(Building building)
        {
            BuildingViewModel buildingViewModel = new MapperService<BuildingViewModel>().Map(building);
            //buildingViewModel.Person = building.Person == null ? null : personManager.MapPersonToViewModel(building.Person);
            //buildingViewModel.Place = building.Place == null ? null : placeManager.MapPlaceToViewModel(building.Place);

            buildingViewModel.Person = building.Person == null ? null : new MapperService<PersonViewModel>().Map(building.Person);
            buildingViewModel.Place = building.Place == null ? null : new MapperService<PlaceViewModel>().Map(building.Place);
            return buildingViewModel;

        //    return new BuildingViewModel
        //    {
        //        Id = building.Id,
        //        Name = building.Name,
        //        Website = building.Website,
        //        Dormitories = building.Dormitories.ToString(),
        //        AmountPersons = building.AmountPersons.ToString(),
        //        Forest = building.Forest,
        //        Area = building.Area.ToString(),
        //        KitchenGear = building.KitchenGear,
        //        Beds = building.Beds,
        //        DaySpaces = building.DaySpaces.ToString(),
        //        Place = building.Place == null ? null : placeManager.MapPlaceToViewModel(building.Place),
        //        Person = building.Person == null ? null : personManager.MapPersonToViewModel(building.Person)
        //    };
        }

        private BuildingOverviewItemViewModel MapBuildingToOverViewItemViewModel(Building building)
        {
            return new MapperService<BuildingOverviewItemViewModel>().Map(building);
            //return new BuildingOverviewItemViewModel
            //{
            //    Id = building.Id,
            //    Name = building.Name,
            //    Dormitories = building.Dormitories,
            //    AmountPersons = building.AmountPersons,
            //    City = building.Place.City,
            //    Website = building.Website
            //};
        }

        #endregion Mappers
    }
}
