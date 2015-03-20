using System.Collections.Generic;
using System.Linq;
using PotterKata.DataAccess.Models;
using PotterKata.DataAccess.Storages;

namespace PotterKata.DataAccess.Repositories
{
	public class BooksRepository : MemoryRepository<Book>, IBooksRepository
	{
		public BooksRepository(IMemoryStorage storage)
			: base(storage)
		{
		}

		public override bool Add(Book book)
		{
			if (book != null &&
				Storage.Books != null &&
				Storage.Books.All(b => b.Name != book.Name))
			{
				Storage.Books.Add(book);
				return true;
			}

			return false;
		}

		public override IEnumerable<Book> GetAll()
		{
			return Storage.Books;
		}
	}
}