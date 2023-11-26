using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excercise2.Repository.EntityClass
{
	public partial class Book
	{
		public int Id { get; set; }
		public string Publisher { get; set; }
		public string Title { get; set; }
		public string AuthorLastName { get; set; }
		public string AuthorFirstName { get; set; }
		public decimal Price { get; set; }
	}
}
