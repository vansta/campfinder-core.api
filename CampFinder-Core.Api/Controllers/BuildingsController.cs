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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CampFinder_Core.Api.Controllers
{
    [Route("api/building")]
    public class BuildingsController : ControllerBase
    {
        private readonly BuildingManager manager = new BuildingManager();
        
        [HttpGet("all")]
        public IActionResult GetBuildings()
        {
            return Ok(manager.GetBuildingOverview());
        }


        [HttpGet]
        public IActionResult GetBuildingById(Guid id)
        {
            try
            {
                Log.Information($"Get building {id.ToString()}");
                BuildingViewModel building = manager.GetBuildingViewModel(id);
                return Ok(building);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPost]
        public IActionResult PostNewBuilding([FromBody] BuildingViewModel building)
        {
            try
            {
                if (building.Id == Guid.Empty)
                {
                    Log.Information($"Building posted: {building.Name}");
                    manager.PostNewBuilding(building);
                    return Ok($"Gebouw {building.Name} is aangemaakt");
                }
                else
                {
                    Log.Information($"Building updated: {building.Name}");
                    manager.UpdateBuilding(building);
                    return Ok($"Gebouw {building.Name} is aangepast");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{building.Name} kon niet worden aangepast of aangemaakt: {ex.Message}");
            }
        }

        
        [HttpPost("search")]
        public IActionResult PostBuildingSearch([FromBody] BuildingSearchViewModel building)
        {
            try
            {
                if (building != null)
                    return Ok(manager.PostBuildingSearch(building));
                else
                    return Ok(manager.GetBuildingOverview());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public IActionResult DeleteBuilding(Guid id)
        {
            Log.Information($"Removing {id}");
            string respons;
            try
            {
                respons = manager.Delete<Building>(id);
                return Ok(respons);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
