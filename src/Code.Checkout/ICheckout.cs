namespace Code.Checkout
{
    public interface ICheckout
    {
        bool Scan(string sku);

        decimal TotalPrice { get; }
    }
}