using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using CampFinder.Models;

namespace CampFinder.DbContext
{
    public class CampFinderDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public CampFinderDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Place> Places { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Terrain> Terrains { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<CampPlace> CampPlaces { get; set; }
    }
}
