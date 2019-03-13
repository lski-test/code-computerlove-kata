using System;
using System.Collections.Generic;

namespace Code.Checkout.Offers
{
    public interface IOfferRepo
    {
        IEnumerable<IOfferMatcher> GetOffers();
    }
}