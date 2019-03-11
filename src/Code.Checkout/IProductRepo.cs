namespace Code.Checkout
{
    public interface IProductRepo
    {
        Product GetBySku(string sku);
    }
}