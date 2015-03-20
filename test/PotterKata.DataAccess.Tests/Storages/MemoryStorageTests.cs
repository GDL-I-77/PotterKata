using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using PotterKata.DataAccess.Models;
using PotterKata.DataAccess.Storages;

namespace PotterKata.DataAccess.Tests.Storages
{
	[TestFixture]
	public class MemoryStorageTests
	{
		private IMemoryStorage _storage;

		[SetUp]
		public void SetUp()
		{
			_storage = new MemoryStorage();
		}

		[Test]
		public void Books_should_be_not_null_by_default()
		{
			_storage.Books.Should().NotBeNull();
		}

		[Test]
		public void Books_should_contain_8_default_Harry_Potter_books_in_strict_order()
		{
			var expectedBooks = new List<Book>
			{
				new Book
				{
					Name = "Harry Potter and the Philosopher's Stone",
				},
				new Book
				{
					Name = "Harry Potter and the Chamber of Secrets",
				},
				new Book
				{
					Name = "Harry Potter and the Prisoner of Azkaban",
				},
				new Book
				{
					Name = "Harry Potter and the Goblet of Fire",
				},
				new Book
				{
					Name = "Harry Potter and the Order of the Phoenix",
				},
				new Book
				{
					Name = "Harry Potter and the Half-Blood Prince",
				},
				new Book
				{
					Name = "Harry Potter and the Deathly Hallows – Part 1",
				},
				new Book
				{
					Name = "Harry Potter and the Deathly Hallows – Part 2",
				}
			};

			_storage.Books.ShouldAllBeEquivalentTo(expectedBooks, options => options.WithStrictOrdering());
		}

		[Test]
		public void WishList_should_be_not_null_by_default()
		{
			_storage.WishList.Should().NotBeNull();
		}
	}
}