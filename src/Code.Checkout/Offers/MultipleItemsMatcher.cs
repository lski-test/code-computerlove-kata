using System;
using System.Collections.Generic;

namespace Code.Checkout.Offers
{
    public class MultipleItemsMatcher : IOfferMatcher
    {
        private IEnumerable<string> _skus;
        private readonly decimal _fixedPrice;

        public int Priority { get; }

        public MultipleItemsMatcher(IEnumerable<string> skus, decimal fixedPrice, short priority = 0)
        {
            _skus = skus ?? throw new ArgumentNullException(nameof(skus));

            _fixedPrice = fixedPrice >= 0 ? fixedPrice : throw new ArgumentException($"{nameof(fixedPrice)} should not be below zero");

            Priority = priority;
        }

        public IEnumerable<IOffer> Match(IEnumerable<CheckoutItem> items)
        {
            throw new NotImplementedException();
        }
    }
}