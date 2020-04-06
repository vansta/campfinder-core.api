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
    public class TerrainController : Controller
    {
        private readonly TerrainManager manager = new TerrainManager();

        [HttpGet("all")]
        public JsonResult GetTerrains()
        {
            Log.Information("Get all terrains");
            return Json(manager.GetTerrainViewModels());
        }

        [HttpGet]
        public JsonResult GetTerrainById(Guid id)
        {
            Log.Information($"Get terrain {id.ToString()}");
            TerrainViewModel terrain = manager.GetTerrainViewModel(id);
            return Json(terrain);
        }

        [HttpPost]
        public void PostNewTerrain([FromBody] TerrainViewModel terrain)
        {
            Log.Information($"terrain posted: {terrain}");
            manager.PostNewTerrain(terrain);
        }

        [HttpPost("search")]
        public JsonResult PostTerrainSearch([FromBody] TerrainSearchViewModel terrainSearch)
        {
            Log.Information($"terrain search: {terrainSearch}");
            return Json(manager.GetTerrainsForSearch(terrainSearch));
        }
    }
}
