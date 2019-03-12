using System.Collections.Generic;

namespace Code.Checkout.Offers
{
    public interface IOfferMatcher
    {
        int Priority { get; }

        IEnumerable<IOffer> Match(IEnumerable<CheckoutItem> items);
    }
}