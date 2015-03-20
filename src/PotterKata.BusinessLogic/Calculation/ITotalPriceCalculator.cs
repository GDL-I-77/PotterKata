using System.Collections.Generic;
using PotterKata.DataAccess.Models;

namespace PotterKata.BusinessLogic.Calculation
{
	public interface ITotalPriceCalculator
	{
		/// <summary>
		/// Calculate total price of all books
		/// </summary>
		/// <param name="books">Collection of books</param>
		/// <returns>Total price</returns>
		decimal Calculate(ICollection<Book> books);
	}
}