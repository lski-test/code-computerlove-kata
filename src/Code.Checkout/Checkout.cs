using System.Collections.Generic;

namespace Code.Checkout
{
    public class Checkout : ICheckout
    {
        // NB: When not passing items as interfaces, use concrete classes as it reduces virtual calls in the compiler
        private readonly List<Product> _items;

        public Checkout()
        {
            _items = new List<Product>();
        }

        public decimal TotalPrice { get; private set; }

        public void Scan(string item)
        {
            throw new System.NotImplementedException();
        }
    }
}