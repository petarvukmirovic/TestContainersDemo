using BlogsExample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsExample
{
	public class BlogContext : DbContext 
	{
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Post> Posts { get; set; }

		public BlogContext(DbContextOptions<BlogContext> options) : base(options)
		{
		}
	}
}
