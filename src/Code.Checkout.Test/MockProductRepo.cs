using System;

namespace Code.Checkout.Test
{
    public class MockProductRepo : IProductRepo
    {
        public Product GetBySku(string sku)
        {
            throw new NotImplementedException();
        }
    }
}