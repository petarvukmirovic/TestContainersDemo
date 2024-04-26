using Microsoft.EntityFrameworkCore;
using TestContainersDemo.DatabaseSetup;

namespace TestContainersDemo
{
	[Collection("Database collection")]
	public class TestClassPostsDb
	{
		private readonly DatabaseFixture fixture;

		public TestClassPostsDb(DatabaseFixture fixture)
		{
			this.fixture = fixture;
		}

		[Fact]
		public async Task ExampleBlogTest()
		{
			// arrange
			var blogsDb = await fixture.CreateSqlDbContext();

			// act
			var titleCount =
				await blogsDb.Posts.Where(p => p.Title.StartsWith("title"))
								   .CountAsync();

			//assert
			Assert.Equal(2, titleCount);
		}
	}
}