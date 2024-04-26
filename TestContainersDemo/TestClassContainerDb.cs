using Microsoft.EntityFrameworkCore;
using TestContainersDemo.DatabaseSetup;

namespace TestContainersDemo
{
	[Collection("Database collection")]
	public class TestClassContainerDb
	{
		private readonly DatabaseFixture fixture;

		public TestClassContainerDb(DatabaseFixture fixture)
		{
			this.fixture = fixture;
		}

		[Fact]
		public async Task ExampleBlogTest()
		{
			// arrange
			var blogsDb = await fixture.CreateSqlDbContext();

			// act
			var wwwCount =
				await blogsDb.Blogs.Where(b => b.Url.Contains("www"))
								   .CountAsync();

			//assert
			Assert.Equal(2, wwwCount);
		}
	}
}