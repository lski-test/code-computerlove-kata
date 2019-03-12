using Code.Checkout.Offers;
using FluentAssertions;
using System;
using Xunit;

namespace Code.Checkout.Test
{
    public class OfferTests
    {
        [Fact]
        public void Single_Multiplier_Deal_Correctly_Discovered()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);

            var deals = matcher.Match(new[] {
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 }
            });

            deals.Should().HaveCount(1);
        }

        [Fact]
        public void Several_Multiplier_Deals_Correctly_Discovered()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);

            var deals = matcher.Match(new[] {
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 }
            });

            deals.Should().HaveCount(2);
        }

        [Fact]
        public void No_Deals_Correctly_Discovered()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);

            var deals = matcher.Match(new[] {
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "B", Price = 50 },
                new CheckoutItem { Sku = "C", Price = 50 }
            });

            deals.Should().HaveCount(0);
        }
    }
}