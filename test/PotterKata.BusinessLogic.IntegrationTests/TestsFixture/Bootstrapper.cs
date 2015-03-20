using Microsoft.Practices.Unity;
using PotterKata.BusinessLogic.Calculation;
using PotterKata.BusinessLogic.Facades;
using PotterKata.DataAccess.Repositories;
using PotterKata.DataAccess.Storages;

namespace PotterKata.BusinessLogic.IntegrationTests.TestsFixture
{
	public static class Bootstrapper
	{
		public static UnityContainer Initialise()
		{
			var container = new UnityContainer();
			container
				.RegisterType<IMemoryStorage, MemoryStorage>(new ContainerControlledLifetimeManager())
				.RegisterType<IBooksRepository, BooksRepository>(new ContainerControlledLifetimeManager())
				.RegisterType<IWishListRepository, WishListRepository>(new ContainerControlledLifetimeManager())
				.RegisterType<ITotalPriceCalculator, TotalPriceCalculator>(new ContainerControlledLifetimeManager())
				.RegisterType<IStoreFacade, StoreFacade>(new ContainerControlledLifetimeManager());

			return container;
		}
	}
}