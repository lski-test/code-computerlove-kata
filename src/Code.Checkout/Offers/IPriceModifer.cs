using System;

namespace Code.Checkout.Offers
{
    public interface IPriceModifer
    {
        decimal Modifier(decimal total);
    }
}