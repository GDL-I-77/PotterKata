using System.Collections.Generic;
using System.Linq;
using PotterKata.BusinessLogic.Calculation;
using PotterKata.DataAccess.Repositories;
using Book = PotterKata.BusinessLogic.Models.Book;

namespace PotterKata.BusinessLogic.Facades
{
	public class StoreFacade : IStoreFacade
	{
		private readonly IBooksRepository _booksRepository;
		private readonly IWishListRepository _wishListRepository;
		private readonly ITotalPriceCalculator _totalPriceCalculator;

		public StoreFacade(IBooksRepository booksRepository, IWishListRepository wishListRepository, ITotalPriceCalculator totalPriceCalculator)
		{
			_booksRepository = booksRepository;
			_wishListRepository = wishListRepository;
			_totalPriceCalculator = totalPriceCalculator;
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

		public bool AddBookToWishList(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				var book = _booksRepository
					.GetAll()
					.FirstOrDefault(b => b != null && b.Name == name);

				if (book != null)
					return _wishListRepository.Add(book);
			}

			return false;
		}

		public decimal CalculateTotalPrice()
		{
			var books = _wishListRepository.GetAll();
			return _totalPriceCalculator.Calculate(books);
		}
	}
}