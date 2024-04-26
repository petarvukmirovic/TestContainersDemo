using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsExample.Models
{
	public class Post
	{
		public int PostId { get; set; }
		public required string Title { get; set; }
		public required string Content { get; set; }

		public int? BlogId { get; set; }
		public Blog? Blog { get; set; }
	}
}
