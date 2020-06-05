using CampFinder.Models;
using CampFinder.Repositories;
using CampFinder.ViewModels;
using System;
using System.Collections.Generic;

namespace CampFinder.Managers
{
    public class ReviewManager : BaseManager
    {
        private readonly ReviewRepository repository = new ReviewRepository();
        public IEnumerable<ReviewViewModel> GetReviewsById(Guid id)
        {
            IEnumerable<Review> reviews = repository.GetReviewsById(id);
            List<ReviewViewModel> reviewViewModels = mapper.Map<List<ReviewViewModel>>(reviews);
            return reviewViewModels;
        }

        public ReviewViewModel PostNewReview(ReviewViewModel reviewViewModel)
        {
            try
            {
                Review review = mapper.Map<Review>(reviewViewModel);
                repository.PostnewReview(review);
                return mapper.Map<ReviewViewModel>(review);
            }
            catch(Exception ex)
            {
                LogErrors(ex);
                throw ex;
            }
        }
    }
}
