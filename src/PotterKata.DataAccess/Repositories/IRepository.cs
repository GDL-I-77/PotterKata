using System.Collections.Generic;

namespace PotterKata.DataAccess.Repositories
{
	public interface IRepository<T> where T: class, new()
	{
		/// <summary>
		/// Add item to the repository
		/// </summary>
		/// <param name="item"></param>
		/// <returns>Result of the operation</returns>
		bool Add(T item);

		/// <summary>
		/// Get all items from the repository
		/// </summary>
		/// <returns>All available items</returns>
		IEnumerable<T> GetAll();
	}
}