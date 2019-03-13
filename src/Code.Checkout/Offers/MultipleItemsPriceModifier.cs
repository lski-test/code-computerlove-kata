using System;

namespace Code.Checkout.Offers
{
    public class MultipleItemsPriceModifier : PriceModifier
    {
        /// <summary>
        /// Creates a new price modifier, for the strict multiple items offer
        /// </summary>
        /// <param name="originalItemsTotal">The amount the original items that were found in the offer cost, so it can be calculated with the current total</param>
        public MultipleItemsPriceModifier(decimal originalItemsTotal, decimal newPrice) : base(originalItemsTotal - newPrice)
        {
        }
    }
}