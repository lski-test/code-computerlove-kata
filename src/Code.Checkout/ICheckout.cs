namespace Code.Checkout
{
    public interface ICheckout
    {
        bool Scan(string item);

        decimal TotalPrice { get; }
    }
}