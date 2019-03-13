using System;

namespace Code.Checkout.Offers
{
    /// <summary>
    /// Handles modifying a total by a certain amount depending on the type of modification needed.
    /// </summary>
    public interface IPriceModifer
    {
        /// <summary>
        /// Accepts the total to be modified and returns the result.
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        decimal Modifier(decimal total);
    }
}