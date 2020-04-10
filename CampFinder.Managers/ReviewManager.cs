using CampFinder.Models;
using CampFinder.Repositories;
using CampFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using CampFinder.AutoMapperConfiguration;

namespace CampFinder.Managers
{
    public class ReviewManager
    {
        private readonly ReviewRepository repository = new ReviewRepository();
        public IEnumerable<ReviewViewModel> GetReviewsById(Guid id)
        {
            IEnumerable<Review> reviews = repository.GetReviewsById(id);
            List<ReviewViewModel> reviewViewModels = new MapperService<List<ReviewViewModel>>().Map(reviews);
            return reviewViewModels;
        }

        public ReviewViewModel PostNewReview(ReviewViewModel reviewViewModel)
        {
            Review review = new MapperService<Review>().Map(reviewViewModel);
            repository.PostnewReview(review);
            return new MapperService<ReviewViewModel>().Map(review);
        }
    }
}
