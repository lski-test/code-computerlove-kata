using System.Collections.Generic;

namespace Code.Checkout.Offers
{
    public interface IOfferMatcher
    {
        /// <summary>
        /// If an offer has a higher prority it should check the checkout items first. As a item should only be in one deal.
        /// (This is obviously a guess and something that would be known when building it)
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Find what items match the offer and return the amount of offers (along with their modifiers to the total price)
        /// </summary>
        IEnumerable<IPriceModifer> Match(IReadOnlyList<CheckoutItem> items);
    }
}