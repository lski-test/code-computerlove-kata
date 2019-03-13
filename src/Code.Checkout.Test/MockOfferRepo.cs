using Code.Checkout.Offers;
using System;
using System.Collections.Generic;

namespace Code.Checkout.Test
{
    public class MockOfferRepo : IOfferRepo
    {
        private List<IOfferMatcher> _matchers;

        public MockOfferRepo()
        {
            _matchers = new List<IOfferMatcher> {
                 new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130),
                 new MultipleItemsMatcher(new[] { "B", "B" }, 45)
            };
        }

        public IEnumerable<IOfferMatcher> GetOffers()
        {
            return _matchers;
        }
    }
}