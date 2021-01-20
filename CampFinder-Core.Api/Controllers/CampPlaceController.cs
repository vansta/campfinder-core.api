using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CampFinder.Models;
using CampFinder.Managers;
using Microsoft.AspNetCore.Cors;
using CampFinder.ViewModels;
using Serilog;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CampFinder_Core.Api.Controllers
{
    [Route("api/campplace")]
    public class CampPlaceController : ControllerBase
    {
        private readonly BuildingManager manager;// = new BuildingManager();
        public CampPlaceController(IConfiguration configuration)
        {
            manager = new BuildingManager(configuration);
        }

        [HttpGet]
        public async Task<IActionResult> GetCampPlaceById(Guid id)
        {
            try
            {
                Log.Information($"Get building {id}");
                return Ok(await manager.GetCampPlace(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
