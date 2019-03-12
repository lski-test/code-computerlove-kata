using Code.Checkout.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Checkout
{
    public class Checkout : ICheckout
    {
        // NB: When not passing items as interfaces, use concrete classes as it reduces virtual calls in the compiler.
        // NB: Or create concrete class to make explicit API contract
        private readonly List<CheckoutItem> _items;

        private readonly IProductRepo _repo;

        public Checkout(IProductRepo productRepo)
        {
            _repo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
            _items = new List<CheckoutItem>();
        }

        public decimal TotalPrice { get; private set; }

        public bool Scan(string sku)
        {
            if (sku == null) { throw new ArgumentNullException(nameof(sku)); }

            var item = _repo.GetBySku(sku);

            if (item == null) { return false; }

            _items.Add(item.ToCheckoutItem());

            TotalPrice = _items.Sum(i => i.Price);

            return true;
        }
    }
}