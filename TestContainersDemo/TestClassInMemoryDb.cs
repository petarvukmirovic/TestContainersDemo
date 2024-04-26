using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestContainersDemo.DatabaseSetup;

namespace TestContainersDemo
{
	[Collection("Database collection")]
	public class TestClassInMemoryDb
	{
		private readonly DatabaseFixture fixture;

		public TestClassInMemoryDb(DatabaseFixture fixture)
		{
			this.fixture = fixture;
		}

		[Fact]
		public async Task ExampleBlogTestNoAsync()
		{
			// arrange
			var blogsDb = await fixture.CreateInMemoryContext();

			// act
			var wwwCount =
				 blogsDb.Blogs.Where(b => b.Url.Contains("www"))
						      .Count();

			//assert
			Assert.Equal(2, wwwCount);
		}
	}
}
