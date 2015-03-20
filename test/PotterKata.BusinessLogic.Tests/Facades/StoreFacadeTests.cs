using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PotterKata.BusinessLogic.Facades;
using PotterKata.DataAccess.Models;
using PotterKata.DataAccess.Repositories;

namespace PotterKata.BusinessLogic.Tests.Facades
{
	[TestFixture]
	public class StoreFacadeTests
	{
		private StoreFacade _facade;
		private Mock<IBooksRepository> _booksRepository;
		private Mock<IWishListRepository> _wishListRepository;

		[SetUp]
		public void SetUp()
		{
			_booksRepository = new Mock<IBooksRepository>();
			_wishListRepository = new Mock<IWishListRepository>();
			_facade = new StoreFacade(_booksRepository.Object, _wishListRepository.Object);
		}

		#region GetBooks

		[Test]
		public void GetBooks_should_return_empty_enumerable_If_repository_returns_null()
		{
			//Act
			var actual = _facade.GetBooks();

			//Assert
			actual.Should().Equal(Enumerable.Empty<Models.Book>());
		}

		[Test]
		public void GetBooks_should_return_all_available_not_null_books()
		{
			//Arrange
			_booksRepository.Setup(b => b.GetAll()).Returns(new List<Book>
			{
				new Book { Name = "1"},
				null,
				new Book { Name = "2"},
			});

			//Act
			var actual = _facade.GetBooks();

			//Assert
			actual.ShouldAllBeEquivalentTo(new List<Models.Book>
			{
				new Models.Book { Name = "1", Price = Book.Price},
				new Models.Book { Name = "2", Price = Book.Price},
			});
		}

		#endregion

		#region AddBookToWishList

		[TestCase(null)]
		[TestCase("")]
		public void AddBookToWishList_should_return_false_if_input_name_is_null_or_empty(string inputName)
		{
			//Act
			var actual = _facade.AddBookToWishList(inputName);

			//Assert
			actual.Should().BeFalse();
		}

		[Test]
		public void AddBookToWishList_should_not_throw_NullReferenceException_If_repository_returns_collection_with_null_book_object()
		{
			//Arrange
			var books = new List<Book>
			{
				new Book { Name = "1" },
				null,
				new Book { Name = "2" }
			};
			_booksRepository.Setup(b => b.GetAll()).Returns(books);

			//Act
			Action act = (() => _facade.AddBookToWishList("1"));

			//Assert
			act.ShouldNotThrow<NullReferenceException>();
		}

		[Test]
		public void AddBookToWishList_should_return_false_if_repository_does_not_contain_book_by_input_name()
		{
			//Arrange
			var books = new List<Book>
			{
				new Book { Name = "1" },
				new Book { Name = "2" }
			};
			_booksRepository.Setup(b => b.GetAll()).Returns(books);

			//Act
			var actual = _facade.AddBookToWishList("3");

			//Assert
			actual.Should().BeFalse();
		}

		[Test]
		public void AddBookToWishList_should_add_input_book_if_there_is_book_with_same_name_in_repository()
		{
			//Arrange
			var books = new List<Book>
			{
				new Book { Name = "1" },
				new Book { Name = "2" }
			};
			_booksRepository.Setup(b => b.GetAll()).Returns(books);

			//Act
			_facade.AddBookToWishList("1");

			//Assert
			_wishListRepository.Verify(w => w.Add(books.First()), Times.Once);
		}

		[Test]
		public void AddBookToWishList_should_return_true_if_repository_add_book_to_wish_list_successfully()
		{
			//Arrange
			var books = new List<Book>
			{
				new Book { Name = "1" },
				new Book { Name = "2" }
			};
			_booksRepository.Setup(b => b.GetAll()).Returns(books);
			_wishListRepository.Setup(w => w.Add(books.First())).Returns(true);

			//Act
			var actual = _facade.AddBookToWishList("1");

			//Assert
			actual.Should().BeTrue();
		}

		[Test]
		public void AddBookToWishList_should_return_false_if_repository_does_not_add_book_to_wish_list_successfully()
		{
			//Arrange
			var books = new List<Book>
			{
				new Book { Name = "1" },
				new Book { Name = "2" }
			};
			_booksRepository.Setup(b => b.GetAll()).Returns(books);
			_wishListRepository.Setup(w => w.Add(books.First())).Returns(false);

			//Act
			var actual = _facade.AddBookToWishList("1");

			//Assert
			actual.Should().BeFalse();
		}

		#endregion
	}
}