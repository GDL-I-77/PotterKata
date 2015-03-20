using System.Collections.Generic;
using System.Linq;
using PotterKata.DataAccess.Models;

namespace PotterKata.BusinessLogic.Calculation
{
	public class TotalPriceCalculator : ITotalPriceCalculator
	{
		public decimal Calculate(ICollection<Book> books)
		{
			if (books != null && books.Count > 0)
			{
				int seriesCount = books.GroupBy(b => b.Name).Count();

				decimal discountPercentage = GetDiscountPercentage(seriesCount);
				if (discountPercentage > 0)
					return CalculateTotalPrice(books) - CalculateDiscount(seriesCount, discountPercentage);

				return CalculateTotalPrice(books);
			}

			return 0;
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