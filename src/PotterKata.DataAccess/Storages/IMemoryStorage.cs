using System.Collections.Generic;
using PotterKata.DataAccess.Models;

namespace PotterKata.DataAccess.Storages
{
	public interface IMemoryStorage
	{
		/// <summary>
		/// Contains all available books
		/// </summary>
		List<Book> Books { get; }
	}
}