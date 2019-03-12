namespace Code.Checkout.Products
{
    public interface IProductRepo
    {
        /// <summary>
        /// Returns the product that matches the passed sku number.
        /// </summary>
        /// <param name="sku">The stock identifier code</param>
        /// <returns>The product matching the Sku or null if not found</returns>
        Product GetBySku(string sku);
    }
}