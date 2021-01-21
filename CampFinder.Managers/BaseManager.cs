using CampFinder.AutoMapperConfiguration;
using CampFinder.DbContext;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.Managers
{
    public abstract class BaseManager
    {
        internal AutoMapper.Mapper mapper;
        internal CampFinderDbContextFactory dbContextFactory;
        internal BaseManager(IConfiguration configuration)
        {
            mapper = MapperService.Initialize();
            dbContextFactory = new CampFinderDbContextFactory(configuration);
        }
    }
}
