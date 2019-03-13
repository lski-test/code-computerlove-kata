using Code.Checkout.Products;
using System;

namespace Code.Checkout.Offers
{
    /// <summary>
    /// Represents a product passing through Offer Matchers
    /// </summary>
    public class OfferItem
    {
        public decimal Price { get; set; }

        public string Sku { get; set; }

        public bool InDeal { get; set; }
    }

    public static class OfferItemExtensions
    {
        /// <summary>
        /// Converts a product into a item that can used internally in a Checkout.
        /// </summary>
        /// <param name="product"></param>
        public static OfferItem ToOfferItem(this Product product)
        {
            if (product == null) {
                throw new ArgumentNullException(nameof(product));
            }

            return new OfferItem
            {
                Sku = product.Sku,
                Price = product.Price,
                InDeal = false
            };
        }
    }
}