using Code.Checkout.Offers;
using System;
using System.Collections.Generic;

namespace Code.Checkout.Test
{
    public class MockOfferRepo : IOfferRepo
    {
        public IEnumerable<IOfferMatcher> GetOffers()
        {
            throw new NotImplementedException();
        }
    }
}