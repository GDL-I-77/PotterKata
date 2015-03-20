using System.Collections.Generic;
using System.Linq;
using PotterKata.DataAccess.Repositories;
using Book = PotterKata.BusinessLogic.Models.Book;

namespace PotterKata.BusinessLogic.Facades
{
	public class StoreFacade : IStoreFacade
	{
		private readonly IBooksRepository _booksRepository;

		public StoreFacade(IBooksRepository booksRepository)
		{
			_booksRepository = booksRepository;
		}

		public IEnumerable<Book> GetBooks()
		{
			var books = _booksRepository.GetAll();
			if (books != null)
			{
				return books
					.Where(b => b != null)
					.Select(b => new Book { Name = b.Name, Price = DataAccess.Models.Book.Price });
			}

			return Enumerable.Empty<Book>();
		}
	}
}