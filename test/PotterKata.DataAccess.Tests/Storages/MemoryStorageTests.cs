using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using PotterKata.DataAccess.Constants;
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
			//Arrange
			var expectedBooks = new List<Book>
			{
				new Book
				{
					Name = BookNames.ThePhilosophersStone
				},
				new Book
				{
					Name = BookNames.TheChamberOfSecrets
				},
				new Book
				{
					Name = BookNames.ThePrisonerOfAzkaban
				},
				new Book
				{
					Name = BookNames.TheGobletOfFire
				},
				new Book
				{
					Name = BookNames.TheOrderOfThePhoenix
				},
				new Book
				{
					Name = BookNames.TheHalfBloodPrince
				},
				new Book
				{
					Name = BookNames.TheDeathlyHallowsPart1
				},
				new Book
				{
					Name = BookNames.TheDeathlyHallowsPart2
				}
			};

			//Act
			_storage.Books.ShouldAllBeEquivalentTo(expectedBooks, options => options.WithStrictOrdering());
		}

		[Test]
		public void WishList_should_be_not_null_by_default()
		{
			_storage.WishList.Should().NotBeNull();
		}
	}
}