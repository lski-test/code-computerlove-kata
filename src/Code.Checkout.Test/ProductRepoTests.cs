using FluentAssertions;
using Xunit;

namespace Code.Checkout.Test
{
    /// <summary>
    /// Its arguable whether you would test the mock, being as its a simple implementation. But useful for developing the mock itself.
    /// </summary>
    public class ProductRepoTests
    {
        [Fact]
        public void Check_Correct_Products_Returned()
        {
            var productRepo = new MockProductRepo();

            productRepo.GetBySku("A").Should().NotBeNull().And.Match<Product>(p => p.Price == 50);
            productRepo.GetBySku("B").Should().NotBeNull().And.Match<Product>(p => p.Price == 30);
            productRepo.GetBySku("C").Should().NotBeNull().And.Match<Product>(p => p.Price == 20);
            productRepo.GetBySku("D").Should().NotBeNull().And.Match<Product>(p => p.Price == 15);

            productRepo.GetBySku(null).Should().BeNull();
            productRepo.GetBySku("blah").Should().BeNull();
        }
    }
}