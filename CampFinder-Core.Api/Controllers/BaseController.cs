using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampFinder_Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        internal static void LogErrors(Exception ex)
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
