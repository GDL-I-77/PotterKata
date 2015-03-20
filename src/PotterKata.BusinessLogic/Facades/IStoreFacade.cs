using System.Collections.Generic;
using PotterKata.BusinessLogic.Models;

namespace PotterKata.BusinessLogic.Facades
{
	public interface IStoreFacade
	{
		/// <summary>
		/// Get all available books in store
		/// </summary>
		/// <returns>Collection of books</returns>
		IEnumerable<Book> GetBooks();

		/// <summary>
		/// Add book to the Wish List
		/// </summary>
		/// <returns>Results of the operation</returns>
		bool AddBookToWishList(string bookKey);
	}
}