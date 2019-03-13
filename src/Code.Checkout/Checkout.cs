using Code.Checkout.Offers;
using Code.Checkout.Products;
using System;
using System.Collections.Generic;

namespace Code.Checkout
{
    public class Checkout : ICheckout
    {
        // NB: When not passing items as interfaces, use concrete classes as it reduces virtual calls in the compiler.
        // NB: Or create concrete class to make explicit API contract
        private readonly List<Product> _items;

        private readonly IProductRepo _repo;
        private readonly IProcessOffers _offers;

        public Checkout(IProductRepo productRepo, IProcessOffers offers)
        {
            _repo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
            _items = new List<Product>();
            _offers = offers;
        }

        public decimal TotalPrice { get; private set; }

        public bool Scan(string sku)
        {
            if (sku == null) { throw new ArgumentNullException(nameof(sku)); }

            var item = _repo.GetBySku(sku);

            if (item == null) { return false; }

            _items.Add(item);

            var offerBreakdown = _offers.Apply(_items);

            TotalPrice = offerBreakdown.SalePrice;

            return true;
        }
    }
}