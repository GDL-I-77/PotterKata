using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PotterKata.DataAccess.Models;
using PotterKata.DataAccess.Repositories;
using PotterKata.DataAccess.Storages;

namespace PotterKata.DataAccess.Tests.Repositories
{
	[TestFixture]
	public class WishListRepositoryTests
	{
		private WishListRepository _repository;
		private Mock<IMemoryStorage> _storage;

		[SetUp]
		public void SetUp()
		{
			_storage = new Mock<IMemoryStorage>();
			_repository = new WishListRepository(_storage.Object);
		}

		[TestCaseSource(typeof(TestCases), "GetAll")]
		public void GetAll_should_return_data_from_storage_without_any_changes(List<Book> booksFromStorage, List<Book> expectedBooks)
		{
			//Arrange
			_storage.SetupGet(s => s.WishList).Returns(booksFromStorage);

			//Act
			IEnumerable<Book> actual = _repository.GetAll();

			//Assert
			actual.ShouldAllBeEquivalentTo(expectedBooks, options => options.WithStrictOrdering());
		}

		[Test]
		public void Add_should_return_false_if_book_is_null()
		{
			//Act
			var actual = _repository.Add(null);

			//Assert
			actual.Should().BeFalse();
		}

		[Test]
		public void Add_should_return_false_if_books_list_does_not_contain_input_book_at_all()
		{
			//Arrange
			var books = new List<Book>();
			_storage.SetupGet(s => s.Books).Returns(books);

			//Act
			var actual = _repository.Add(new Book());

			//Assert
			actual.Should().BeFalse();
		}

		[Test]
		public void Add_should_throw_NullReferenceException_if_books_list_is_null_and_input_book_is_not_null()
		{
			//Act
			Action act = (() => _repository.Add(new Book()));

			//Assert
			act.ShouldNotThrow<NullReferenceException>();
		}

		[Test]
		public void Add_should_return_true_if_books_list_contains_input_book_and_this_book_is_not_null_and_wishlist_is_not_null_too()
		{
			//Arrange
			var books = new List<Book>
				{
					new Book { Name = "1"},
					new Book { Name = "2"}
				};
			_storage.SetupGet(s => s.Books).Returns(books);
			_storage.SetupGet(s => s.WishList).Returns(new List<Book>());

			//Act
			var actual = _repository.Add(books.Last());

			//Assert
			actual.Should().BeTrue();
		}

		[Test]
		public void Add_should_add_item_to_the_wishlist_if_books_list_contains_input_book_and_this_book_is_not_null_and_wishlist_is_not_null_too()
		{
			//Arrange
			var books = new List<Book>
				{
					new Book { Name = "1"},
					new Book { Name = "2"}
				};
			var inputBook = books.Last();
			var wishList = new List<Book>();
			_storage.SetupGet(s => s.Books).Returns(books);
			_storage.SetupGet(s => s.WishList).Returns(wishList);

			//Act
			_repository.Add(inputBook);

			//Assert
			wishList.Should().Contain(inputBook);
		}

		[Test]
		public void Add_should_throw_NullReferenceException_if_books_list_is_not_null_and_input_book_is_not_null_too_but_wishlist_is_null()
		{
			//Arrange
			var books = new List<Book>
				{
					new Book { Name = "1"},
					new Book { Name = "2"}
				};
			_storage.SetupGet(s => s.Books).Returns(books);

			//Act
			Action act = (() => _repository.Add(new Book()));

			//Assert
			act.ShouldNotThrow<NullReferenceException>();
		}
	}
}