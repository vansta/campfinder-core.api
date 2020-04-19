﻿using System;
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
    public class BuildingsController : Controller
    {
        private readonly BuildingManager manager = new BuildingManager();

        //[EnableCors]
        //[HttpGet("all")]
        //public JsonResult GetBuildings()
        //{
        //    return Json(manager.GetBuildingOverview());
        //}

        
        [HttpGet]
        public JsonResult GetBuildingById(Guid id)
        {
            Log.Information($"Get building {id.ToString()}");
            BuildingViewModel building = manager.GetBuildingViewModel(id);
            return Json(building);
        }

        
        [HttpPost]
        public void PostNewBuilding([FromBody] BuildingViewModel building)
        {
            if (building.Id == Guid.Empty)
            {
                Log.Information($"Building posted: {building}");
                manager.PostNewBuilding(building);
            }
            else
            {
                Log.Information($"Building updated: {building}");
                manager.UpdateBuilding(building);
            }
        }

        
        [HttpPost]
        [Route("search")]
        public JsonResult PostBuildingSearch([FromBody] BuildingSearchViewModel building)
        {
            Log.Information($"building search: {building}");
            if (building != null)
                return Json(manager.PostBuildingSearch(building));
            else
                return Json(manager.GetBuildingOverview());
        }
    }
}
