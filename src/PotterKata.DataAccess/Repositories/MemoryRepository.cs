using System.Collections.Generic;
using PotterKata.DataAccess.Storages;

namespace PotterKata.DataAccess.Repositories
{
	public abstract class MemoryRepository<T> : IRepository<T> where T: class, new()
	{
		protected readonly IMemoryStorage Storage;

		protected MemoryRepository(IMemoryStorage storage)
		{
			Storage = storage;
		}

		public abstract bool Add(T item);

		public abstract IEnumerable<T> GetAll();
	}
}