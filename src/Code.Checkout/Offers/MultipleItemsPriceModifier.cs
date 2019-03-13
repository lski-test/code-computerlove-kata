using System;

namespace Code.Checkout.Offers
{
    public class MultipleItemsPriceModifier : IPriceModifer
    {
        private readonly decimal _originalItemsTotal;
        private readonly decimal _newPrice;

        /// <summary>
        /// Creates a new price modifier, for the strict multiple items offer
        /// </summary>
        /// <param name="originalItemsTotal">The amount the original items that were found in the offer cost, so it can be calculated with the current total</param>
        public MultipleItemsPriceModifier(decimal originalItemsTotal, decimal newPrice)
        {
            _originalItemsTotal = originalItemsTotal;
            _newPrice = newPrice;
        }

        /// <inheritdoc />
        public decimal Modifier(decimal total)
        {
            return total - (_originalItemsTotal - _newPrice);
        }
    }
}