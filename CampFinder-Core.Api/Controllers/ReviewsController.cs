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
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewManager manager = new ReviewManager();
        [HttpGet]
        public IActionResult GetReviewsById(Guid id)
        {
            try
            {
                Log.Information($"Get reviews {id.ToString()}");
                IEnumerable<ReviewViewModel> reviews = manager.GetReviewsById(id);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return BadRequest($"Kon review niet ophalen: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult PostnewReview([FromBody] ReviewViewModel review)
        {
            try
            {
                Log.Information($"Post review");
                return Ok(manager.PostNewReview(review));
            }
            catch (Exception ex)
            {
                return BadRequest($"Kon review niet aanmaken: {ex.Message}");
            }
        }
    }
}