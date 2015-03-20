using System.Collections.Generic;
using PotterKata.DataAccess.Models;

namespace PotterKata.DataAccess.Storages
{
	public class MemoryStorage : IMemoryStorage
	{
		private readonly List<Book> _books = new List<Book>
		{
			new Book
			{
				Name = "Harry Potter and the Philosopher's Stone",
			},
			new Book
			{
				Name = "Harry Potter and the Chamber of Secrets",
			},
			new Book
			{
				Name = "Harry Potter and the Prisoner of Azkaban",
			},
			new Book
			{
				Name = "Harry Potter and the Goblet of Fire",
			},
			new Book
			{
				Name = "Harry Potter and the Order of the Phoenix",
			},
			new Book
			{
				Name = "Harry Potter and the Half-Blood Prince",
			},
			new Book
			{
				Name = "Harry Potter and the Deathly Hallows – Part 1",
			},
			new Book
			{
				Name = "Harry Potter and the Deathly Hallows – Part 2",
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