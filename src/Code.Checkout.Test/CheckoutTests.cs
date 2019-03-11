using FluentAssertions;
using Xunit;

namespace Code.Checkout.Test
{
    public class CheckoutTests
    {
        [Fact]
        public void No_Items_Should_Be_Zero()
        {
            var checkout = new Checkout();

            checkout.TotalPrice.Should().Be(0);
        }

        [Fact]
        public void Single_Item_Should_Have_Correct_Total()
        {
            var checkout = new Checkout();

            checkout.Scan("A");

            checkout.TotalPrice.Should().Be(50);
        }
    }
}