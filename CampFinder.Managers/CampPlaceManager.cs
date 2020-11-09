using System;
using System.Linq;
using CampFinder.Models;
using CampFinder.Repositories;
using CampFinder.ViewModels;

namespace CampFinder.Managers
{
    public abstract class CampPlaceManager<DomainModel> : BaseManager where DomainModel : CampPlace
    {
        private readonly CampPlaceRepository repository = new CampPlaceRepository();
        private readonly ReviewRepository reviewRepository = new ReviewRepository();

        internal IQueryable<DomainModel> GetSearch(SearchViewModel searchModel)
        {
            IQueryable<DomainModel> models = repository.Get<DomainModel>();

            try
            {
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
                        models = models.AsEnumerable().Where(b => b.Place.Province != null && searchModel.Province.Any(p => p.Trim().ToUpper() == b.Place.Province.Trim().ToUpper())).AsQueryable();
                    }
                    if (searchModel.Forest)
                    {
                        models = models.Where(b => b.Forest);
                    }
                    if (searchModel.Foreign)
                    {
                        models = models.Where(b => !b.Place.Country.Trim().ToUpper().Contains("BELGI"));
                    }
                    if (searchModel.MinimumScore > 0)
                    {
                        models = models.ToList().Where(c => c.AverageScore >= searchModel.MinimumScore).AsQueryable();
                    }
                    if (searchModel.Accessibility > 0)
                    {
                        models = models.Where(c => c.Place.Accessibility <= searchModel.Accessibility);
                    }
                }
            }
            catch(Exception ex)
            {
                LogErrors(ex);
            }

            return models;
        }

        public string Delete<T>(Guid id) where T : CampPlace, new()
        {
            T campPlace = new T();
            try
            {
                campPlace = repository.GetById<T>(id);
                repository.Delete(campPlace);
                repository.DeletePerson(campPlace.Person);
                repository.DeletePlace(campPlace.Place);
                reviewRepository.Delete(campPlace.Reviews);

                return $"{campPlace.Name} has been deleted";
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                throw new Exception($"Failed to delete {campPlace.Name}: {ex.Message}");
            }
        }

        #region Mappers

        internal DomainModel MapViewModelToModel<ViewModel>(ViewModel viewModel) where ViewModel: CampPlaceViewModel
        {
            DomainModel model = mapper.Map<DomainModel>(viewModel);
            model.Person = viewModel.Person == null ? null : mapper.Map<Person>(viewModel.Person);
            model.Place = viewModel.Place == null ? null : mapper.Map<Place>(viewModel.Place);
            return model;
        }

        internal ViewModel MapModelToViewModel<ViewModel>(DomainModel model) where ViewModel : CampPlaceViewModel
        {
            ViewModel viewModel = mapper.Map<ViewModel>(model);
            viewModel.Person = model.Person == null ? null : mapper.Map<PersonViewModel>(model.Person);
            viewModel.Place = model.Place == null ? null : mapper.Map<PlaceViewModel>(model.Place);
            return viewModel;
        }

        #endregion Mappers        
    }
}
