using BlogsExample;
using BlogsExample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MsSql;

namespace TestContainersDemo.DatabaseSetup
{
	public class DatabaseFixture : IDisposable
	{
		private readonly MsSqlContainer dbContainer;
		private bool startedSql = false;
		private bool startedInMemory = false;

		public DatabaseFixture()
		{
			dbContainer = new MsSqlBuilder()
				.WithImage("mcr.microsoft.com/mssql/server:2022-latest")
				.WithName("db")
				.Build();
		}

		// not thread safe and not production ready!
		public async Task<BlogContext> CreateSqlDbContext()
		{
			BlogContext dbContext;
			if(!startedSql)
			{
				dbContext = await StartContainerAndCreateDb();
				startedSql = true;
			}
			else
			{
				dbContext = CreateDefaultSqlServerContext();
			}
			return dbContext;
		}

		// not thread safe and not production ready!
		public async Task<BlogContext> CreateInMemoryContext()
		{
			BlogContext dbContext = CreateDefaultInMemoryContext();
			if (!startedInMemory)
			{
				await InitializeDb(dbContext, migrate:false);
				startedInMemory = true;
			}
			return dbContext;
		}

		private async Task<BlogContext> StartContainerAndCreateDb()
		{
			await dbContainer.StartAsync();
			var dbCtx = CreateDefaultSqlServerContext();
			await InitializeDb(dbCtx);			
			return dbCtx;
		}

		private async Task InitializeDb(BlogContext dbCtx, bool migrate = true)
		{
			if(migrate)
			{
				await dbCtx.Database.MigrateAsync();
			}
			dbCtx.Blogs.Add(new Blog()
			{
				Url = "https://www.GOOGLE.com",

			});
			var blogWithPosts = new Blog() { Url = "HTTPS://WWW.DNB.NL" };
			blogWithPosts.Posts.Add(new Post()
			{
				Title = "TITLE test",
				Content = "test CONTENT"
			});
			blogWithPosts.Posts.Add(new Post()
			{
				Title = "title TEST",
				Content = "TEST content"
			});
			dbCtx.Blogs.Add(blogWithPosts);
			await dbCtx.SaveChangesAsync();
		}

		private BlogContext CreateDefaultSqlServerContext() =>
			new BlogContext(new DbContextOptionsBuilder<BlogContext>()
						        .UseSqlServer(dbContainer.GetConnectionString())
								.Options);

		private BlogContext CreateDefaultInMemoryContext() =>
			new BlogContext(new DbContextOptionsBuilder<BlogContext>()
								.UseInMemoryDatabase("test-db")
								.Options);

		public void Dispose()
		{
			dbContainer.DisposeAsync().AsTask().Wait();
		}

	}

	[CollectionDefinition("Database collection")]
	public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
	{
		// This class has no code, and is never created. Its purpose is simply
		// to be the place to apply [CollectionDefinition] and all the
		// ICollectionFixture<> interfaces.
	}
}


