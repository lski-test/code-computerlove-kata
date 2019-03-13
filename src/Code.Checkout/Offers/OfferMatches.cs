using System;
using System.Collections.Generic;

namespace Code.Checkout.Offers
{
    /// <summary>
    /// A DTO that holds the resulting information about products and how they need to adjust the final total.
    /// </summary>
    public class OfferMatches
    {
        public OfferMatches() : this(new List<IPriceModifer>(), new List<OfferItem>())
        {
        }

        public OfferMatches(IList<OfferItem> items) : this(new List<IPriceModifer>(), items)
        {
        }

        public OfferMatches(IList<IPriceModifer> modifiers, IList<OfferItem> items)
        {
            Modifiers = modifiers;
            Items = items;
        }

        public IList<IPriceModifer> Modifiers { get; set; }

        public IList<OfferItem> Items { get; set; }
    }
}