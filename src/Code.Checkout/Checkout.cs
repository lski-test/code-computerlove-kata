namespace Code.Checkout
{
    public class Checkout : ICheckout
    {
        public decimal TotalPrice { get; private set; }

        public void Scan(string item)
        {
            throw new System.NotImplementedException();
        }
    }
}