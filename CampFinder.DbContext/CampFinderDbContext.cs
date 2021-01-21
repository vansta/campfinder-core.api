using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using CampFinder.Models;

namespace CampFinder.DbContext
{
    public class CampFinderDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly string connectionString;
        //public CampFinderDbContext()
        //{
        //    IConfigurationBuilder builder = new ConfigurationBuilder();
        //    builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

        //    var root = builder.Build();
        //    connectionString = root.GetConnectionString("CampFinderDb");
        //}

        public CampFinderDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CampPlace>(entity =>
            {
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<Terrain>(entity =>
            {
                entity.HasOne(t => t.Place);
                entity.HasOne(t => t.Person);
                entity.HasMany(t => t.Reviews);
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.HasOne(b => b.Place);
                entity.HasOne(b => b.Person);
                entity.HasMany(b => b.Reviews);
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(r => r.Id);
            });
        }

        public DbSet<Place> Places { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Terrain> Terrains { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<CampPlace> CampPlaces { get; set; }
    }
}
