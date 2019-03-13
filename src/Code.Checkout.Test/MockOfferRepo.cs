using Code.Checkout.Offers;
using System;
using System.Collections.Generic;

namespace Code.Checkout.Test
{
    public class MockOfferRepo : IOfferRepo
    {
        private List<IOfferMatcher> _matchers;

        public MockOfferRepo(List<IOfferMatcher> matchers = null)
        {
            _matchers = matchers ?? new List<IOfferMatcher> {
                 new StrictItemOfferMatcher(new[] { "A", "A", "A" }, 130),
                 new StrictItemOfferMatcher(new[] { "B", "B" }, 45)
            };
        }

        public IEnumerable<IOfferMatcher> GetOffers()
        {
            return _matchers;
        }
    }
}