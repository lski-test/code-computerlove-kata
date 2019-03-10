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

            checkout.GetTotalPrice().Should().Be(0);
        }
    }
}