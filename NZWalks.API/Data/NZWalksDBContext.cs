using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
	public class NZWalksDBContext : DbContext
	{
		public NZWalksDBContext(DbContextOptions<NZWalksDBContext> dbContextOptions)
			: base(dbContextOptions)
		{
		}

		public DbSet<Difficulty> Difficulties { get; set; }
		public DbSet<Region> Regions { get; set; }
		public DbSet<Walks> Walks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//seed the data for difficulties
			//Easy, Medium and Hard

			var difficulties = new List<Difficulty>()
			{
				new Difficulty{Id = Guid.Parse("f808ddcd-b5e5-4d80-b732-1ca523e48434"), Name = "Hard" },
				new Difficulty{Id = Guid.Parse("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"), Name = "Easy" },
				new Difficulty{Id = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"), Name = "Medium" }
			};

			modelBuilder.Entity<Difficulty>().HasData(difficulties);

			var region = new List<Region>
			{
				new() {Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"), Code = "NSN", Name = "Nelson", RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"  },
				new() {Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"), Code = "AKL", Name = "Auckland", RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"  },
				new() {Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"), Code = "BOP", Name = "Bay Of Plenty", RegionImageUrl = null  },
				new() {Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"), Code = "NTL", Name = "Northland" , RegionImageUrl = null },
				new() {Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"), Code = "STL", Name = "Southland",RegionImageUrl = null },
				new() {Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"), Code = "WGN", Name = "Wellington", RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"  },
			};

			modelBuilder.Entity<Region>().HasData(region);

		}
	}
}
