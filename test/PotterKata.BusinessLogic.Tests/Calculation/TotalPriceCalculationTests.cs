using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using PotterKata.BusinessLogic.Calculation;
using PotterKata.DataAccess.Models;

namespace PotterKata.BusinessLogic.Tests.Calculation
{
	[TestFixture]
	public class TotalPriceCalculatorTests
	{
		private TotalPriceCalculator _calculator;

		[SetUp]
		public void SetUp()
		{
			_calculator = new TotalPriceCalculator();
		}

		[Test]
		public void Calculate_should_return_0_if_books_colection_is_null()
		{
			//Act
			decimal actual = _calculator.Calculate(null);

			//Assert
			actual.Should().Be(0);
		}

		[Test]
		public void Calculate_should_return_0_if_books_colection_is_empty()
		{
			//Act
			decimal actual = _calculator.Calculate(new List<Book>());

			//Assert
			actual.Should().Be(0);
		}

		[Test]
		public void Calculate_should_return_total_price_without_percent_discount_if_wish_list_contains_1_series_of_books()
		{
			//Arrange
			var books = new List<Book>
			{
				new Book {Name = "1" },
			};

			//Act
			decimal actual = _calculator.Calculate(books);

			//Assert
			actual.Should().Be(8);
		}

		[Test]
		public void Calculate_should_return_total_price_with_5_percent_discount_if_wish_list_contains_2_series_of_books()
		{
			//Arrange
			var books = new List<Book>
			{
				new Book {Name = "1" },
				new Book {Name = "2" },
			};

			//Act
			decimal actual = _calculator.Calculate(books);

			//Assert
			actual.Should().Be((decimal)15.2);
		}

		[Test]
		public void Calculate_should_return_total_price_with_10_percent_discount_if_books_colection_contains_3__series_of_books()
		{
			//Arrange
			var books = new List<Book>
			{
				new Book {Name = "1" },
				new Book {Name = "2" },
				new Book {Name = "3" },
			};

			//Act
			decimal actual = _calculator.Calculate(books);

			//Assert
			actual.Should().Be((decimal)21.6);
		}

		[Test]
		public void Calculate_should_return_total_price_with_15_percent_discount_if_books_colection_contains_4_series_of_books()
		{
			//Arrange
			var books = new List<Book>
			{
				new Book {Name = "1" },
				new Book {Name = "2" },
				new Book {Name = "3" },
				new Book {Name = "4" },
			};

			//Act
			decimal actual = _calculator.Calculate(books);

			//Assert
			actual.Should().Be((decimal)27.2);
		}

		[Test]
		public void Calculate_should_return_total_price_with_25_percent_discount_if_books_colection_contains_5_series_of_books()
		{
			//Arrange
			var books = new List<Book>
			{
				new Book {Name = "1" },
				new Book {Name = "2" },
				new Book {Name = "3" },
				new Book {Name = "4" },
				new Book {Name = "5" },
			};

			//Act
			decimal actual = _calculator.Calculate(books);

			//Assert
			actual.Should().Be(30);
		}

		[Test]
		public void Calculate_should_return_total_price_with_30_percent_discount_if_books_colection_contains_6_series_of_books()
		{
			//Arrange
			var books = new List<Book>
			{
				new Book {Name = "1" },
				new Book {Name = "2" },
				new Book {Name = "3" },
				new Book {Name = "4" },
				new Book {Name = "5" },
				new Book {Name = "6" },
			};

			//Act
			decimal actual = _calculator.Calculate(books);

			//Assert
			actual.Should().Be((decimal)33.6);
		}

		[Test]
		public void Calculate_should_return_total_price_with_35_percent_discount_if_books_colection_contains_7_series_of_books()
		{
			//Arrange
			var books = new List<Book>
			{
				new Book {Name = "1" },
				new Book {Name = "2" },
				new Book {Name = "3" },
				new Book {Name = "4" },
				new Book {Name = "5" },
				new Book {Name = "6" },
				new Book {Name = "7" },
			};

			//Act
			decimal actual = _calculator.Calculate(books);

			//Assert
			actual.Should().Be((decimal)36.4);
		}

		[Test]
		public void Calculate_should_return_properly_calculated_total_price_if_wishlist_contains_mix_of_books_series()
		{
			//Arrange
			var books = new List<Book>
			{
				new Book {Name = "1" },
				new Book {Name = "2" },
				new Book {Name = "3" },
				new Book {Name = "6" },
				new Book {Name = "4" },
				new Book {Name = "4" },
				new Book {Name = "5" },
				new Book {Name = "5" },
				new Book {Name = "2" },
				new Book {Name = "6" },
				new Book {Name = "7" },
			};

			//Act
			decimal actual = _calculator.Calculate(books);

			//Assert
			actual.Should().Be((decimal)68.4);
		}

		[Test]
		public void Calculate_should_not_throw_NullreferenceException_if_books_list_contains_null_objects()
		{
			//Act
			Action act = (() => _calculator.Calculate(new List<Book>
			{
				new Book {Name = "1" },
				null,
				new Book {Name = "1" },
			}));

			//Assert
			act.ShouldNotThrow<NullReferenceException>();
		}
	}
}