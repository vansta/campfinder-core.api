using System;
using System.Collections.Generic;
using System.Linq;
using CampFinder.Models;
using CampFinder.Repositories;
using CampFinder.ViewModels;

namespace CampFinder.Managers
{
    public class PlaceManager
    {
        public Place MapViewModelToPlace(PlaceViewModel place)
        {
            return new Place
            {
                Id = Guid.NewGuid(),
                Street = place.Street,
                HouseNumber = place.HouseNumber,
                City = place.City,
                PostNumber = place.PostNumber,
                Province = place.Province,
                Country = place.Country
            };
        }

        public PlaceViewModel MapPlaceToViewModel(Place place)
        {
            return new PlaceViewModel
            {
                Street = place.Street,
                HouseNumber = place.HouseNumber,
                City = place.City,
                PostNumber = place.PostNumber,
                Province = place.Province,
                Country = place.Country
            };
        }
    }
}
