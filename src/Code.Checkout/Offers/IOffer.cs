using System;

namespace Code.Checkout.Offers
{
    public interface IOffer
    {
        decimal Modifier(decimal total);
    }
}