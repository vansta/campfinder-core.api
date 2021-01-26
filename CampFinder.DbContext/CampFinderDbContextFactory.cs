using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.DbContext
{
    public class CampFinderDbContextFactory
    {
        private readonly IConfiguration configuration;
        public CampFinderDbContextFactory(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public CampFinderDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<CampFinderDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<CampFinderDbContext>()
                .UseSqlServer(configuration.GetConnectionString("CampFinderDb"), opt => opt.EnableRetryOnFailure());

            return new CampFinderDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
