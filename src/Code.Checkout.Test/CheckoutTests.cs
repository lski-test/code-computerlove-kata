using FluentAssertions;
using Xunit;

namespace Code.Checkout.Test
{
    public class CheckoutTests
    {
        [Fact]
        public void No_Items_Should_Be_Zero()
        {
            var checkout = new Checkout(new MockProductRepo());

            checkout.TotalPrice.Should().Be(0);
        }

        [Fact]
        public void Single_Item_Should_Have_Correct_Total()
        {
            var checkout = new Checkout(new MockProductRepo());

            checkout.Scan("A");

            checkout.TotalPrice.Should().Be(50);
        }

        [Fact]
        public void Multiple_Items_Should_Have_Correct_Total()
        {
            var checkout = new Checkout(new MockProductRepo());

            checkout.Scan("A");
            checkout.Scan("B");

            checkout.TotalPrice.Should().Be(80);
        }
    }
}