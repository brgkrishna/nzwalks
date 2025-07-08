using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
	public class NZWalksAuthDbContext : IdentityDbContext
	{
		public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> dbContextOptions) : base(dbContextOptions)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var readerRoleId = "8af7c8d3-3b99-41dd-bfde-e1cbd62263d4";
			var writeRoleId = "2987c1ac-70d7-4c4f-870c-8fa269ed05a8";

			var roles = new List<IdentityRole>
			{
				new IdentityRole()
				{
					Id = readerRoleId,
					Name = "Reader",
					ConcurrencyStamp = readerRoleId,
					NormalizedName = "Reader".ToUpper()

				},
				new IdentityRole()
				{
					Id = writeRoleId,
					Name = "Writer",
					ConcurrencyStamp = writeRoleId,
					NormalizedName = "Writer".ToUpper()

				}
			};

			builder.Entity<IdentityRole>().HasData(roles);
		}
	}
}
