using Microsoft.EntityFrameworkCore;
using NZwalks.API.Models.Domain;

namespace NZwalks.API.Data
{
    //inheriting NZWalksDbContext from DbContext Class
    public class NZWalksDbContext : DbContext
    {
        //Creating a constructor 
        //Passing dbContextOptions cause later on we will pass our own connection through program.cs
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }

        //Dbsets is a property of dbcontext class that represents a collection of entites in the database
        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed data for difficulty EASY MEDIUM HARD

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    id = Guid.Parse("d959df6c-e8ef-4aa7-b4b7-cea3f608bf9a"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    id = Guid.Parse("6b11cf7d-65ce-4a54-a1f8-ad9596774ea7"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    id = Guid.Parse("e400333f-b2e5-4967-843c-f09690a0cc58"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed data for Region
            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.Parse("c2e0849a-53f8-432f-840b-bca4aba3158f"),
                    Code = "MUB",
                    Name = "Mumbai",
                    RegionImageUrl = "https://cdn.britannica.com/26/84526-050-45452C37/Gateway-monument-India-entrance-Mumbai-Harbour-coast.jpg",
                },
                new Region
                {
                    Id = Guid.Parse("4a921308-71d5-4d7a-a842-b5c493f9ff8e"),
                    Code = "DL",
                    Name = "Delhi",
                    RegionImageUrl = "https://cdn.britannica.com/37/189837-050-F0AF383E/New-Delhi-India-War-Memorial-arch-Sir.jpg",
                },
                new Region
                {
                    Id = Guid.Parse("3f9dc9bd-c7b4-4000-8553-cb24e49ab959"),
                    Code = "LKO",
                    Name = "Lucknow",
                    RegionImageUrl = "https://ghoomophiro.com/wp-content/uploads/2022/10/Places-to-visit-in-lucknow-scaled.jpg",
                },
                new Region
                {
                    Id = Guid.Parse("eb9775e6-ba49-4e46-81e0-503cc751ca3f"),
                    Code = "PUN",
                    Name = "Pune",
                    RegionImageUrl = "https://mittalbuilders.com/wp-content/uploads/2020/12/Reasons-to-settle-down-in-Pune-1400x700.png",
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
