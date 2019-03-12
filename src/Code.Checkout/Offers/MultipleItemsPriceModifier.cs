using System;

namespace Code.Checkout.Offers
{
    public class MultipleItemsPriceModifier : IPriceModifer
    {
        public MultipleItemsPriceModifier()
        {

        }

        public decimal Modifier(decimal total)
        {
            throw new NotImplementedException();
        }
    }
}