using Code.Checkout.Products;
using System;

namespace Code.Checkout
{
    public class CheckoutItem
    {
        public decimal Price { get; set; }

        public string Sku { get; set; }

        public bool InDeal { get; set; }
    }

    public static class CheckoutItemExtensions
    {
        /// <summary>
        /// Converts a product into a item that can used internally in a Checkout.
        /// </summary>
        /// <param name="product"></param>
        public static CheckoutItem ToCheckoutItem(this Product product)
        {
            if (product == null) {
                throw new ArgumentNullException(nameof(product));
            }

            return new CheckoutItem
            {
                Sku = product.Sku,
                Price = product.Price,
                InDeal = false
            };
        }

        // NB: I would use this version going forward with c# 8.0 and NonNullable reference types.
        // NB: Oh and I am only putting this here now as this is an interview example. I wouldnt clutter code with this. At worst leave a TODO, at best put a backlog item.
        // public static CheckoutItem ToCheckoutItem(this Product product) => new CheckoutItem{
        //    Sku = product.Sku,
        //    Price = product.Price,
        //    InDeal = false
        // };
    }
}