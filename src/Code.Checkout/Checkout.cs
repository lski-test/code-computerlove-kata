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

        public Checkout(IProductRepo productRepo)
        {
            _repo = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
            _items = new List<Product>();
        }

        public decimal TotalPrice { get; private set; }

        public bool Scan(string item)
        {
            throw new System.NotImplementedException();
        }
    }
}