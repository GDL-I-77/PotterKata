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
			decimal actual = _calculator.Calculate(null);

			actual.Should().Be(0);
		}

		[Test]
		public void Calculate_should_return_0_if_books_colection_is_empty()
		{
			decimal actual = _calculator.Calculate(new List<Book>());

			actual.Should().Be(0);
		}

		[Test]
		public void Calculate_should_throw_NullReferenceException_exception()
		{
			Action act = (() => _calculator.Calculate(new List<Book> { null}));

			act.ShouldThrow<NullReferenceException>();
		}

		[Test]
		public void Calculate_should_return_total_price_without_percent_discount_if_wish_list_contains_1_series_of_books()
		{
			var books = new List<Book>
			{
				new Book {Name = "1" },
			};

			decimal actual = _calculator.Calculate(books);

			actual.Should().Be(8);
		}

		[Test]
		public void Calculate_should_return_total_price_with_5_percent_discount_if_wish_list_contains_2_series_of_books()
		{
			var books = new List<Book>
			{
				new Book {Name = "1" },
				new Book {Name = "2" },
			};

			decimal actual = _calculator.Calculate(books);

			actual.Should().Be((decimal)15.2);
		}

		[Test]
		public void Calculate_should_return_total_price_with_10_percent_discount_if_books_colection_contains_3_books_with_same_key()
		{
			var books = new List<Book>
			{
				new Book {Name = "1" },
				new Book {Name = "2" },
				new Book {Name = "3" },
			};

			decimal actual = _calculator.Calculate(books);

			actual.Should().Be((decimal)21.6);
		}

		[Test]
		public void Calculate_should_return_total_price_with_15_percent_discount_if_books_colection_contains_4_books_with_same_key()
		{
			var books = new List<Book>
			{
				new Book {Name = "1" },
				new Book {Name = "2" },
				new Book {Name = "3" },
				new Book {Name = "4" },
			};

			decimal actual = _calculator.Calculate(books);

			actual.Should().Be((decimal)27.2);
		}

		[Test]
		public void Calculate_should_return_total_price_with_25_percent_discount_if_books_colection_contains_5_books_with_same_key()
		{
			var books = new List<Book>
			{
				new Book {Name = "1" },
				new Book {Name = "2" },
				new Book {Name = "3" },
				new Book {Name = "4" },
				new Book {Name = "5" },
			};

			decimal actual = _calculator.Calculate(books);

			actual.Should().Be(30);
		}

		[Test]
		public void Calculate_should_return_total_price_with_30_percent_discount_if_books_colection_contains_6_books_with_same_key()
		{
			var books = new List<Book>
			{
				new Book {Name = "1" },
				new Book {Name = "2" },
				new Book {Name = "3" },
				new Book {Name = "4" },
				new Book {Name = "5" },
				new Book {Name = "6" },
			};

			decimal actual = _calculator.Calculate(books);

			actual.Should().Be((decimal)33.6);
		}

		[Test]
		public void Calculate_should_return_total_price_with_35_percent_discount_if_books_colection_contains_7_books_with_same_name()
		{
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

			decimal actual = _calculator.Calculate(books);

			actual.Should().Be((decimal)36.4);
		}

		[Test]
		public void Calculate_should_return_properly_calculated_total_price_if_wishlist_contains_mix_of_books_series()
		{
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

			decimal actual = _calculator.Calculate(books);

			actual.Should().Be((decimal)68.4);
		}
	}
}