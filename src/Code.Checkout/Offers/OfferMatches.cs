using System;
using System.Collections.Generic;

namespace Code.Checkout.Offers
{
    /// <summary>
    /// A DTO that holds the resulting information about 
    /// </summary>
    public class OfferMatches
    {
        public OfferMatches() : this(new List<IPriceModifer>(), new List<CheckoutItem>())
        {
        }

        public OfferMatches(IList<CheckoutItem> items) : this(new List<IPriceModifer>(), items)
        {
        }

        public OfferMatches(IList<IPriceModifer> modifiers, IList<CheckoutItem> items)
        {
            Modifiers = modifiers;
            Items = items;
        }

        public IList<IPriceModifer> Modifiers { get; set; }

        public IList<CheckoutItem> Items { get; set; }
    }
}