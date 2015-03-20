using System.Collections.Generic;
using System.Linq;
using PotterKata.DataAccess.Models;
using PotterKata.DataAccess.Storages;

namespace PotterKata.DataAccess.Repositories
{
	public class WishListRepository : MemoryRepository<Book>, IWishListRepository
	{
		public WishListRepository(IMemoryStorage storage)
			: base(storage)
		{
		}

		public override bool Add(Book book)
		{
			if (book != null &&
				Storage.Books.Any(b => b.Name == book.Name))
			{
				Storage.WishList.Add(book);
				return true;
			}

			return false;
		}

		public override IEnumerable<Book> GetAll()
		{
			return Storage.WishList;
		}
	}
}