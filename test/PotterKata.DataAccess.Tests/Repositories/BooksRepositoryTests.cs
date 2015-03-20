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
	public class BooksRepositoryTests
	{
		private BooksRepository _repository;
		private Mock<IMemoryStorage> _storage;

		[SetUp]
		public void SetUp()
		{
			_storage = new Mock<IMemoryStorage>();
			_repository = new BooksRepository(_storage.Object);
		}

		[TestCaseSource(typeof(TestCases), "GetAll")]
		public void GetAll_should_return_data_from_storage_without_any_changes(List<Book> booksFromStorage, List<Book> expectedBooks)
		{
			//Arrange
			_storage.SetupGet(s => s.Books).Returns(booksFromStorage);

			//Act
			IEnumerable<Book> actual = _repository.GetAll();

			//Assert
			actual.ShouldAllBeEquivalentTo(expectedBooks, options => options.WithStrictOrdering());
		}

		[Test]
		public void Add_should_retrun_false_if_book_is_null()
		{
			//Act
			var actual = _repository.Add(null);

			//Assert
			actual.Should().BeFalse();
		}

		[Test]
		public void Add_should_return_false_if_books_list_contains_input_book()
		{
			//Arrange
			var books = new List<Book>
			{
				new Book { Name = "test name 1" },
				new Book { Name = "test name 2" }
			};
			_storage.SetupGet(s => s.Books).Returns(books);

			//Act
			var actual = _repository.Add(books.First());

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
		public void Add_should_return_true_if_books_list_does_not_contain_input_book()
		{
			//Arrange
			var books = new List<Book>();
			_storage.SetupGet(s => s.Books).Returns(books);

			//Act
			var actual = _repository.Add(new Book());

			//Assert
			actual.Should().BeTrue();
		}

		[Test]
		public void Add_should_add_book_to_books_list_if_books_list_does_not_contain_input_book()
		{
			//Arrange
			var inputBook = new Book { Name = "test name 1" };
			var books = new List<Book>();
			_storage.SetupGet(s => s.Books).Returns(books);

			//Act
			_repository.Add(inputBook);

			//Assert
			books.Should().Contain(inputBook);
		}
	}
}