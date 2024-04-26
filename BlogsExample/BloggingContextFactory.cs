using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsExample
{
	public class BloggingContextFactory : IDesignTimeDbContextFactory<BlogContext>
	{
		public BlogContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<BlogContext>();
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BlogsTest");

			return new BlogContext(optionsBuilder.Options);
		}
	}
}
