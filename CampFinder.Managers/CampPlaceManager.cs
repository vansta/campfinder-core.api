using System;
using System.Linq;
using System.Threading.Tasks;
using CampFinder.DbContext;
using CampFinder.Models;
using CampFinder.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CampFinder.Managers
{
    public abstract class CampPlaceManager<DomainModel> : BaseManager where DomainModel : CampPlace
    {
        public CampPlaceManager(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<CampPlaceViewModel> GetCampPlace(Guid id)
        {
            using CampFinderDbContext context = dbContextFactory.CreateDbContext();
            CampPlace campplace = await context.CampPlaces
                .FindAsync(id);

            return mapper.Map<CampPlaceViewModel>(campplace);
        }

        internal static IQueryable<T> GetSearch<T>(IQueryable<T> models, SearchViewModel searchModel) where T : CampPlace
        {
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                models = models.Where(b => b.Name.Contains(searchModel.Name));
            }
            if (searchModel.AmountPersons != null && int.TryParse(searchModel.AmountPersons, out int amountPersons))
            {
                models = models.Where(b => b.AmountPersons >= amountPersons);
            }
            if (searchModel.Province != null && searchModel.Province.Any())
            {
                models = models.Where(c => !string.IsNullOrEmpty(c.Place.Province) && searchModel.Province.Any(p => p.Equals(c.Place.Province)));
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
                models = models
                    .Where(c => c.Reviews != null && c.Reviews.Select(r => r.Score).Average() >= searchModel.MinimumScore);
                //models = models.ToList().Where(c => c.AverageScore >= searchModel.MinimumScore).AsQueryable();
            }
            if (searchModel.Accessibility > 0)
            {
                models = models.Where(c => c.Place.Accessibility <= searchModel.Accessibility);
            }

            return models;
        }

        public async Task<string> Delete<T>(Guid id) where T : CampPlace, new()
        {
            using CampFinderDbContext context = dbContextFactory.CreateDbContext();
            T campplace = await context.Set<T>()
                .Include(c => c.Person)
                .Include(c => c.Place)
                .Include(c => c.Reviews)
                .SingleAsync(c => c.Id == id);

            context.Remove(campplace);

            //await repository.Delete(campPlace);
            //await repository.DeletePerson(campPlace.Person);
            //await repository.DeletePlace(campPlace.Place);
            //await reviewRepository.Delete(campPlace.Reviews);


            return $"{campplace.Name} has been deleted";
        }

        //#region Mappers

        //internal DomainModel MapViewModelToModel<ViewModel>(ViewModel viewModel) where ViewModel : CampPlaceViewModel
        //{
        //    DomainModel model = mapper.Map<DomainModel>(viewModel);
        //    model.Person = viewModel.Person == null ? null : mapper.Map<Person>(viewModel.Person);
        //    model.Place = viewModel.Place == null ? null : mapper.Map<Place>(viewModel.Place);
        //    return model;
        //}

        //internal ViewModel MapModelToViewModel<ViewModel>(DomainModel model) where ViewModel : CampPlaceViewModel
        //{
        //    ViewModel viewModel = mapper.Map<ViewModel>(model);
        //    viewModel.Person = model.Person == null ? null : mapper.Map<PersonViewModel>(model.Person);
        //    viewModel.Place = model.Place == null ? null : mapper.Map<PlaceViewModel>(model.Place);
        //    return viewModel;
        //}

        //#endregion Mappers        
    }
}
