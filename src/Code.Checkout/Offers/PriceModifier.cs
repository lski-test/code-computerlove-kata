using System;

namespace Code.Checkout.Offers
{
    /// <summary>
    /// A basic price modifier, that reduces the total by the amount stated in the constructor.
    /// </summary>
    public class PriceModifier : IPriceModifer
    {
        private readonly decimal _discountAmount;

        /// <summary>
        /// Creates new modifier
        /// </summary>
        /// <param name="discountAmount">The amount to remove from the total in the <see cref="Modify(decimal)"/> function</param>
        public PriceModifier(decimal discountAmount)
        {
            _discountAmount = discountAmount;
        }

        /// <inheritdoc />
        public decimal Modify(decimal total)
        {
            return total - _discountAmount;
        }
    }
}