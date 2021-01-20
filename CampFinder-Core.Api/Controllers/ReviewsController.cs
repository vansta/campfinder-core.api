using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampFinder.Managers;
using CampFinder.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CampFinder_Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewManager manager;// = new ReviewManager();
        public ReviewsController(IConfiguration configuration)
        {
            manager = new ReviewManager(configuration);
        }
        [HttpGet]
        public IActionResult GetReviewsById(Guid id)
        {
            try
            {
                Log.Information($"Get reviews {id}");
                return Ok(manager.GetReviewsById(id));
            }
            catch (Exception ex)
            {
                return BadRequest($"Kon review niet ophalen: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostnewReview([FromBody] ReviewViewModel review)
        {
            try
            {
                Log.Information($"Post review");
                return Ok(await manager.PostNewReview(review));
            }
            catch (Exception ex)
            {
                return BadRequest($"Kon review niet aanmaken: {ex.Message}");
            }
        }
    }
}