using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CampFinder.Models;
using CampFinder.Managers;
using Microsoft.AspNetCore.Cors;
using CampFinder.ViewModels;

namespace CampFinder_Core.Api.Controllers
{
    [Route("api/terrain")]
    public class TerrainController : Controller
    {
        private readonly TerrainManager manager = new TerrainManager();

        [EnableCors]
        [HttpGet("all")]
        public JsonResult GetTerrains()
        {
            return Json(manager.GetTerrainViewModels());
        }

        [EnableCors]
        [HttpGet]
        public JsonResult GetTerrainById(Guid id)
        {
            TerrainViewModel terrain = manager.GetTerrainViewModel(id);
            return Json(terrain);
        }

        [EnableCors]
        [HttpPost]
        public void PostNewTerrain([FromBody] TerrainViewModel terrain)
        {
            manager.PostNewTerrain(terrain);
        }

        [EnableCors]
        [HttpPost("search")]
        public JsonResult PostTerrainSearch([FromBody] TerrainSearchViewModel terrainSearch)
        {
            return Json(manager.GetTerrainsForSearch(terrainSearch));
        }
    }
}
