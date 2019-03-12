using Code.Checkout.Products;
using System;
using System.Collections.Generic;

namespace Code.Checkout.Test
{
    /// <summary>
    /// As this is an interview example then the repo wouldnt be hard coded like this, hence only creating a mocked repo.
    /// </summary>
    public class MockProductRepo : IProductRepo
    {
        private List<Product> _items;

        public MockProductRepo()
        {
            _items = new List<Product> {
                new Product { Sku = "A", Price = 50 },
                new Product { Sku = "B", Price = 30 },
                new Product { Sku = "C", Price = 20 },
                new Product { Sku = "D", Price = 15 }
            };
        }

        public Product GetBySku(string sku)
        {
            return _items.Find(i => i.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));
        }
    }
}