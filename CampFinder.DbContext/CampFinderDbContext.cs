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
                optionsBuilder.UseSqlServer("Server=161.97.179.116;database=CampFinderDb;Initial Catalog=CampFinderDb;User Id=SA; Password=w1LLther*al5lim5hady");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Terrain>(entity =>
            //{
            //    entity.HasOne(t => t.Place);
            //    entity.HasOne(t => t.Person);
            //    entity.HasMany(t => t.Reviews);
            //});

            //modelBuilder.Entity<Building>(entity =>
            //{
            //    entity.HasOne(b => b.Place);
            //    entity.HasOne(b => b.Person);
            //    entity.HasMany(b => b.Reviews);
            //});
        }

        public DbSet<Place> Places { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Terrain> Terrains { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<CampPlace> CampPlaces { get; set; }
    }
}
