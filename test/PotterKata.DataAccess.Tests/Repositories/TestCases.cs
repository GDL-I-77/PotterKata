using System.Collections.Generic;
using NUnit.Framework;
using PotterKata.DataAccess.Models;

namespace PotterKata.DataAccess.Tests.Repositories
{
	internal static class TestCases
	{
		public static IEnumerable<TestCaseData> GetAll()
		{
			yield return new TestCaseData(null, null);
			yield return new TestCaseData(new List<Book>(), new List<Book>());
			yield return new TestCaseData(
				new List<Book>
				{
					new Book {Name = "1"}, 
					null,
					new Book {Name = "2"}
				},
				new List<Book>
				{
					new Book {Name = "1"},
					null,
					new Book {Name = "2"}
				}
			);
		}
	}
}