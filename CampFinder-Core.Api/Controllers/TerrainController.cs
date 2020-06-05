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

namespace CampFinder_Core.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/terrain")]
    public class TerrainController : ControllerBase
    {
        private readonly TerrainManager manager = new TerrainManager();

        [HttpGet("all")]
        public IActionResult GetTerrains()
        {
            try
            {
                Log.Information("Get all terrains");
                return Ok(manager.GetTerrainViewModels());
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not get terrains: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetTerrainById(Guid id)
        {
            try
            {
                Log.Information($"Get terrain {id.ToString()}");
                TerrainViewModel terrain = manager.GetTerrainViewModel(id);
                return Ok(terrain);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostNewTerrain([FromBody] TerrainViewModel terrain)
        {
            try
            {
                if (terrain.Id == Guid.Empty)
                {
                    Log.Information($"terrain posted: {terrain.Name}");
                    manager.PostNewTerrain(terrain);
                    return Ok($"Terrein {terrain.Name} aangemaakt");
                }
                else
                {
                    Log.Information($"Terrain updated: {terrain.Name}");
                    manager.UpdateTerrain(terrain);
                    return Ok($"Terrein {terrain.Name} aangepast");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("search")]
        public IActionResult PostTerrainSearch([FromBody] TerrainSearchViewModel terrainSearch)
        {
            return Ok(manager.GetTerrainsForSearch(terrainSearch));
        }

        [HttpDelete("delete")]
        public IActionResult DeleteTerrain(Guid id)
        {
            string response;
            try
            {
                Log.Information($"Removing {id}");
                response = manager.Delete<Terrain>(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
    }
}
