﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CampFinder.AutoMapperConfiguration;
using CampFinder.Models;
using CampFinder.Repositories;
using CampFinder.ViewModels;

namespace CampFinder.Managers
{
    public abstract class CampPlaceManager<DomainModel> where DomainModel : CampPlace
    {
        private readonly CampPlaceRepository repository = new CampPlaceRepository();
        public IQueryable<DomainModel> GetSearch(SearchViewModel searchModel)
        {
            IQueryable<DomainModel> models = repository.Get<DomainModel>();

            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.Name))
                {
                    models = models.Where(b => b.Name.Contains(searchModel.Name));
                }
                if (searchModel.AmountPersons != null && int.TryParse(searchModel.AmountPersons, out int amountPersons))
                {
                    models = models.Where(b => b.AmountPersons >= amountPersons);
                }
                if (searchModel.Province != null && searchModel.Province.Count() > 0)
                {
                    models = models.Where(b => searchModel.Province.Any(p => p == b.Place.Province));
                }
                if (searchModel.Forest)
                {
                    models = models.Where(b => b.Forest);
                }
                if (searchModel.Foreign)
                {
                    models = models.Where(b => b.Place.Country.ToUpper() == "BELGIE");
                }
            }
            return models;
        }

        #region Mappers

        internal DomainModel MapViewModelToModel<ViewModel>(ViewModel viewModel) where ViewModel: CampPlaceViewModel
        {
            DomainModel model = new MapperService<DomainModel>().Map(viewModel);
            model.Person = viewModel.Person == null ? null : new MapperService<Person>().Map(viewModel.Person);
            model.Place = viewModel.Place == null ? null : new MapperService<Place>().Map(viewModel.Place);
            return model;
        }

        internal ViewModel MapModelToViewModel<ViewModel>(DomainModel model) where ViewModel : CampPlaceViewModel
        {
            ViewModel viewModel = new MapperService<ViewModel>().Map(model);
            viewModel.Person = model.Person == null ? null : new MapperService<PersonViewModel>().Map(model.Person);
            viewModel.Place = model.Place == null ? null : new MapperService<PlaceViewModel>().Map(model.Place);
            return viewModel;
        }

        #endregion Mappers
    }
}
