using System.Collections.Generic;
using System.Linq;
using PotterKata.DataAccess.Models;

namespace PotterKata.BusinessLogic.Calculation
{
	public class TotalPriceCalculator : ITotalPriceCalculator
	{
		public decimal Calculate(IEnumerable<Book> books)
		{
			if (books != null)
			{
				var validBooks = GetValidBooks(books);
				if (validBooks.Count > 0)
				{
					int seriesCount = validBooks.GroupBy(b => b.Name).Count();

					decimal discountPercentage = GetDiscountPercentage(seriesCount);
					if (discountPercentage > 0)
						return CalculateTotalPrice(validBooks) - CalculateDiscount(seriesCount, discountPercentage);

					return CalculateTotalPrice(validBooks);
				}
			}

			return 0;
		}

		private static List<Book> GetValidBooks(IEnumerable<Book> books)
		{
			return books.Where(b => b != null).ToList();
		}

		private static int CalculateTotalPrice(ICollection<Book> books)
		{
			return books.Count * Book.Price;
		}

		private static decimal CalculateDiscount(int seriesCount, decimal discountPercentage)
		{
			return (seriesCount * Book.Price) * (discountPercentage / 100);
		}

		private decimal GetDiscountPercentage(int seriesCount)
		{
				switch (seriesCount)
				{
					case 2:
						return 5;
					case 3:
						return 10;
					case 4:
						return 15;
					case 5:
						return 25;
					case 6:
						return 30;
					case 7:
						return 35;
					default:
						return 0;
				}
		}
	}
}