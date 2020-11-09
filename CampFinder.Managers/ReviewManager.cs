using CampFinder.Models;
using CampFinder.Repositories;
using CampFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampFinder.Managers
{
    public class ReviewManager : BaseManager
    {
        private readonly ReviewRepository repository = new ReviewRepository();
        public IEnumerable<ReviewViewModel> GetReviewsById(Guid id)
        {
            return repository.GetReviewsById(id).Select(r => mapper.Map<ReviewViewModel>(r));
        }

        public ReviewViewModel PostNewReview(ReviewViewModel reviewViewModel)
        {
            try
            {
                Review review = mapper.Map<Review>(reviewViewModel);
                repository.PostnewReview(review);
                return reviewViewModel;
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                throw ex;
            }
        }
    }
}
