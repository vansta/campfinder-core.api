using CampFinder.AutoMapperConfiguration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.Managers
{
    public abstract class BaseManager
    {
        internal AutoMapper.Mapper mapper;
        internal BaseManager()
        {
            mapper = MapperService.Initialize();
        }
        internal void LogErrors(Exception ex)
        {
            Exception innerException = ex;
            while (innerException != null)
            {
                Log.Error(innerException.Message);
                Log.Error(innerException.StackTrace);
                innerException = innerException.InnerException;
            }
        }
    }
}
