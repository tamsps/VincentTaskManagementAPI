using Microsoft.EntityFrameworkCore;
using VincentTaskManagementAPI.Models;

namespace VincentTaskManagementAPI.Db
{
		public class VincDbContext:DbContext
		{
				public VincDbContext(DbContextOptions<VincDbContext> options):base(options)
				{
				}
				public DbSet<VincTaskModel> VincTasksModel { get; set; }
		}
}
