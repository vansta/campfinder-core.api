using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampFinder.Managers;
using CampFinder.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CampFinder_Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : Controller
    {
        private readonly ReviewManager manager = new ReviewManager();
        [HttpGet]
        public JsonResult GetReviewsById(Guid id)
        {
            Log.Information($"Get reviews {id.ToString()}");
            IEnumerable<ReviewViewModel> reviews = manager.GetReviewsById(id);
            return Json(reviews);
        }

        [HttpPost]
        public JsonResult PostnewReview([FromBody] ReviewViewModel review)
        {
            Log.Information($"Post review");
            return Json(manager.PostNewReview(review));
        }
    }
}