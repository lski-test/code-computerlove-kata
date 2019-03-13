using Code.Checkout.Products;
using System;
using System.Collections.Generic;

namespace Code.Checkout.Offers
{
    /// <summary>
    /// Represents a process that applys offers to a list of products and returns a new price.
    /// </summary>
    public interface IProcessOffers
    {
        OfferBreakdown Apply(ICollection<Product> products);
    }
}