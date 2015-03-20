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

		[SetUp]
		public void SetUp()
		{
			_booksRepository = new Mock<IBooksRepository>();
			_facade = new StoreFacade(_booksRepository.Object);
		}

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
	}
}