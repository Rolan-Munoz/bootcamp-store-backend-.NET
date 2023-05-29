using System;
using bootcamp_store_backend.Domain.Entities;

namespace bootcamp_store_backend.Infraestructure.Persistence
{
	public class DevelopmentDataLoader
	{
		private readonly StoreContext storeContext;
		private readonly byte[] defaultImage = Convert.FromBase64String("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");

        public DevelopmentDataLoader(StoreContext storeContext)
		{
			this.storeContext = storeContext;
		}


		public void LoadData()
		{
			if (!storeContext.Categories.Any())
			{
				LoadCategories();
			}

			storeContext.SaveChanges();
            if (!storeContext.Items.Any())
            {
                LoadItems();
            }
			storeContext.SaveChanges();
        }

        

        private void LoadCategories()
		{
			var categories = new Category[]
			{
				new Category{ Name="Chaquetas", Image=defaultImage},
				new Category{ Name="Calzado", Image=defaultImage },
			};
			foreach ( Category category in categories)
			{
				storeContext.Categories.Add(category);
			}

		}

		private void LoadItems()
        {
			var items = new Item[]
			{
				new Item{ Name="Chaqueta Multi Bolsillos", Price=39.95, CategoryId=1, Image=defaultImage},
				new Item{ Name="Chaqueta cintura ajustable con lino", CategoryId=1, Price=34.95, Image=defaultImage},
				new Item{ Name="Zapato Atado Cuña", CategoryId=2, Price=28.95, Image=defaultImage},
			};
			foreach( Item item in items)
			{
				storeContext.Items.Add(item);
			}
        }
	}
}

