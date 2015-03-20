using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using PotterKata.BusinessLogic.Facades;
using PotterKata.BusinessLogic.IntegrationTests.TestsFixture;
using PotterKata.BusinessLogic.Models;
using PotterKata.DataAccess.Constants;

namespace PotterKata.BusinessLogic.IntegrationTests
{
	[TestFixture]
	public class StoreFacadeTests
	{
		private IStoreFacade _facade;

		[SetUp]
		public void SetUp()
		{
			_facade = Bootstrapper
				.Initialise()
				.Resolve<IStoreFacade>();
		}

		[Test]
		public void GetBooks_should_return_all_available_books_by_default_in_strict_order()
		{
			var actual = _facade.GetBooks();

			actual.ShouldAllBeEquivalentTo(new List<Book>
			{
				new Book
				{
					Name = BookNames.ThePhilosophersStone, Price = 8
				},
				new Book
				{
					Name = BookNames.TheChamberOfSecrets, Price = 8
				},
				new Book
				{
					Name = BookNames.ThePrisonerOfAzkaban, Price = 8
				},
				new Book
				{
					Name = BookNames.TheGobletOfFire, Price = 8
				},
				new Book
				{
					Name = BookNames.TheOrderOfThePhoenix, Price = 8
				},
				new Book
				{
					Name = BookNames.TheHalfBloodPrince, Price = 8
				},
				new Book
				{
					Name = BookNames.TheDeathlyHallowsPart1, Price = 8
				},
				new Book
				{
					Name = BookNames.TheDeathlyHallowsPart2, Price = 8
				}
			}, options => options.WithStrictOrdering());
		}

		[Test]
		public void GetBooks_should_return_false_if_there_is_no_book_with_same_name_in_storage()
		{
			const string testBookName = "test";

			var actual = _facade.AddBookToWishList(testBookName);

			actual.Should().BeFalse();
		}

		[Test]
		public void GetBooks_should_return_true_if_book_is_added_successfully()
		{
			const string testBookName = BookNames.ThePrisonerOfAzkaban;

			var actual = _facade.AddBookToWishList(testBookName);

			actual.Should().BeTrue();
		}

		[TestCaseSource("CalculateTotalPriceTestCases")]
		public void GetBooks_should_calculate_total_price_properly(List<string> bookNames, decimal expectedTotalPrice)
		{
			//Arrange
			bookNames.ForEach(bn => _facade.AddBookToWishList(bn));

			//Act
			var actualTotalPrice = _facade.CalculateTotalPrice();

			//Assert
			actualTotalPrice.Should().Be(expectedTotalPrice);
		}

		internal IEnumerable<TestCaseData> CalculateTotalPriceTestCases()
		{
			yield return new TestCaseData(new List<string> //6 1 1 1 2 1
			{
				BookNames.TheGobletOfFire,
				BookNames.TheOrderOfThePhoenix,
				BookNames.TheChamberOfSecrets,
				BookNames.TheGobletOfFire,
				BookNames.TheGobletOfFire,
				BookNames.ThePhilosophersStone,
				BookNames.TheGobletOfFire,
				BookNames.TheGobletOfFire,
				BookNames.TheDeathlyHallowsPart1,
				BookNames.TheGobletOfFire,
				BookNames.TheHalfBloodPrince,
				BookNames.TheDeathlyHallowsPart1,
			}, (decimal)81.6);

			yield return new TestCaseData(new List<string>
			{
				BookNames.TheGobletOfFire,
				BookNames.TheOrderOfThePhoenix,
				BookNames.TheGobletOfFire,
				BookNames.TheGobletOfFire,
				BookNames.TheGobletOfFire,
				BookNames.ThePhilosophersStone,
				BookNames.TheHalfBloodPrince,
				BookNames.TheOrderOfThePhoenix,
				BookNames.TheDeathlyHallowsPart1,
				BookNames.TheGobletOfFire,
				BookNames.TheHalfBloodPrince,
				BookNames.TheDeathlyHallowsPart1,
			}, (decimal) 86);

			yield return new TestCaseData(new List<string>
			{
				BookNames.TheGobletOfFire,
				BookNames.TheOrderOfThePhoenix,
				BookNames.TheChamberOfSecrets,
				string.Empty,
				BookNames.TheDeathlyHallowsPart2,
				BookNames.TheGobletOfFire,
				BookNames.ThePhilosophersStone,
				null,
				BookNames.TheDeathlyHallowsPart1,
				BookNames.TheGobletOfFire,
				BookNames.TheHalfBloodPrince,
				BookNames.TheDeathlyHallowsPart1,
			}, (decimal) 60.4);
		}
	}
}