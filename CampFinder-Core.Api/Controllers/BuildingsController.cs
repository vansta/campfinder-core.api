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
    [Route("api/building")]
    public class BuildingsController : ControllerBase
    {
        private readonly BuildingManager manager;// = new BuildingManager();
        public BuildingsController(IConfiguration configuration)
        {
            manager = new BuildingManager(configuration);
        }

        [HttpGet]
        public async Task<IActionResult> GetBuildingById(Guid id)
        {
            try
            {
                Log.Information($"Get building {id}");
                return Ok(await manager.GetBuildingViewModel(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search")]
        public IActionResult GetBuildingSearch([FromQuery] BuildingSearchViewModel building)
        {
            try
            {
                return Ok(manager.GetBuildingSearch(building));
            }
            catch (Exception ex)
            {
                Exception innerException = ex;
                while (innerException != null)
                {
                    Log.Error(innerException.Message);
                    Log.Error(innerException.StackTrace);
                    innerException = ex.InnerException;
                }
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostNewBuilding([FromBody] BuildingViewModel building)
        {
            try
            {
                if (building.Id == Guid.Empty)
                {
                    Log.Information($"Building posted: {building.Name}");
                    await manager.PostNewBuilding(building);
                    return Ok($"Gebouw {building.Name} is aangemaakt");
                }
                else
                {
                    Log.Information($"Building updated: {building.Name}");
                    await manager.UpdateBuilding(building);
                    return Ok($"Gebouw {building.Name} is aangepast");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{building.Name} kon niet worden aangepast of aangemaakt: {ex.Message}");
            }
        }

        
        //[HttpPost("search")]
        //public async Task<IActionResult> PostBuildingSearch([FromBody] BuildingSearchViewModel building)
        //{
        //    try
        //    {
        //        if (building != null)
        //            return Ok(await manager.PostBuildingSearch(building));
        //        else
        //            return Ok(await manager.GetBuildingOverview());
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteBuilding(Guid id)
        {
            Log.Information($"Removing {id}");
            try
            {
                return Ok(await manager.Delete<Building>(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
