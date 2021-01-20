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

namespace CampFinder_Core.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/terrain")]
    public class TerrainController : BaseController
    {
        private readonly TerrainManager manager;// = new TerrainManager();
        public TerrainController(IConfiguration configuration)
        {
            manager = new TerrainManager(configuration);
        }

        [HttpGet]
        public async Task<IActionResult> GetTerrainById(Guid id)
        {
            try
            {
                Log.Information($"Get terrain {id}");
                TerrainViewModel terrain = await manager.GetTerrainViewModel(id);
                return Ok(terrain);
            }
            catch (Exception ex)
            {
                LogErrors(ex);
                return NotFound(ex.Message);
            }
        }

        [HttpGet("search")]
        public IActionResult GetTerrainSearch([FromQuery] TerrainSearchViewModel terrainSearch)
        {
            return Ok(manager.GetTerrainsForSearch(terrainSearch));
        }

        [HttpPost]
        public async Task<IActionResult> PostNewTerrain([FromBody] TerrainViewModel terrain)
        {
            try
            {
                if (terrain.Id == Guid.Empty)
                {
                    Log.Information($"terrain posted: {terrain.Name}");
                    await manager.PostNewTerrain(terrain);
                    return Ok($"Terrein {terrain.Name} aangemaakt");
                }
                else
                {
                    Log.Information($"Terrain updated: {terrain.Name}");
                    await manager.UpdateTerrain(terrain);
                    return Ok($"Terrein {terrain.Name} aangepast");
                }
            }
            catch (Exception ex)
            {
                LogErrors(ex);
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("search")]
        //public IActionResult PostTerrainSearch([FromBody] TerrainSearchViewModel terrainSearch)
        //{
        //    return Ok(manager.GetTerrainsForSearch(terrainSearch));
        //}

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTerrain(Guid id)
        {
            try
            {
                Log.Information($"Removing {id}");
                return Ok(await manager.Delete<Terrain>(id));
            }
            catch (Exception ex)
            {
                LogErrors(ex);
                return BadRequest(ex.Message);
            }            
        }
    }
}
