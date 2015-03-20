using System.Collections.Generic;
using PotterKata.DataAccess.Constants;
using PotterKata.DataAccess.Models;

namespace PotterKata.DataAccess.Storages
{
	public class MemoryStorage : IMemoryStorage
	{
		private readonly List<Book> _books = new List<Book>
		{
			new Book
			{
				Name = BookNames.ThePhilosophersStone
			},
			new Book
			{
				Name = BookNames.TheChamberOfSecrets
			},
			new Book
			{
				Name = BookNames.ThePrisonerOfAzkaban
			},
			new Book
			{
				Name = BookNames.TheGobletOfFire
			},
			new Book
			{
				Name = BookNames.TheOrderOfThePhoenix
			},
			new Book
			{
				Name = BookNames.TheHalfBloodPrince
			},
			new Book
			{
				Name = BookNames.TheDeathlyHallowsPart1
			},
			new Book
			{
				Name = BookNames.TheDeathlyHallowsPart2
			}
		};

		private readonly List<Book> _wishList = new List<Book>();

		public List<Book> Books
		{
			get { return _books; }
		}

		public List<Book> WishList
		{
			get { return _wishList; }
		}
	}
}