using Microsoft.EntityFrameworkCore;
using MyApplication.API.Models.Domain;

namespace MyApplication.API.Data;

public class ApplicationDbContext : DbContext
{

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
   {

	}

   public DbSet<Difficulty> Difficulties { get; set; }

   public DbSet<Region> Regions { get; set; }

   public DbSet<Walk> Walks { get; set; }

   public DbSet<Image> Images { get; set; }


   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      base.OnModelCreating(modelBuilder);

      // Seed data for Difficulties
      // Easy, Medium, Hard
      var difficulties = new List<Difficulty>()
      {
          new Difficulty()
          {
              Id = Guid.Parse("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
              Name = "Easy"
          },
          new Difficulty()
          {
              Id = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
              Name = "Medium"
          },
          new Difficulty()
          {
              Id = Guid.Parse("f808ddcd-b5e5-4d80-b732-1ca523e48434"),
              Name = "Hard"
          }
      };

      // Seed difficulties to the database
      modelBuilder.Entity<Difficulty>().HasData(difficulties);

      // Seed data for Regions
      var regions = new List<Region>
      {
         new Region
         {
             Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
             Name = "Auckland",
             Code = "AKL",
             RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
         },
         new Region
         {
             Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
             Name = "Northland",
             Code = "NTL",
             RegionImageUrl = null
         },
         new Region
         {
             Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
             Name = "Bay Of Plenty",
             Code = "BOP",
             RegionImageUrl = null
         },
         new Region
         {
             Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
             Name = "Wellington",
             Code = "WGN",
             RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
         },
         new Region
         {
             Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
             Name = "Nelson",
             Code = "NSN",
             RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
         },
         new Region
         {
             Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
             Name = "Southland",
             Code = "STL",
             RegionImageUrl = null
         },
      };

      modelBuilder.Entity<Region>().HasData(regions);

      var walks = new List<Walk>()
      {
         new Walk()
         {
            Id = Guid.Parse("B6E6DBB4-49FE-425F-8786-B4DCC1EDBDDA"),
            Name = "Mount Victoria Lookout Walk",
            Description = "this is the descrion of Mount Victoria Lookout Walk",
            LengthInKm = 5,
            WalkImageUrl = "some-dummy.png",
            DifficultyId = Guid.Parse("54466F17-02AF-48E7-8ED3-5A4A8BFACF6F"),
            RegionId = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263")
         },
         new Walk()
         {
            Id = Guid.Parse("1036043C-1955-4B53-BF46-A3651DBE33F6"),
            Name = "Great China Wall",
            Description = "this is the descrion of Mount Victoria Lookout Walk",
            LengthInKm = 10,
            WalkImageUrl = "some-dummy.png",
            DifficultyId = Guid.Parse("F808DDCD-B5E5-4D80-B732-1CA523E48434"),
            RegionId = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de")
         },
      };

      modelBuilder.Entity<Walk>().HasData(walks);

   }

}
