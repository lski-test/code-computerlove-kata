using Code.Checkout.Offers;
using FluentAssertions;
using System;
using System.Linq;
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
        public void Single_Multiplier_Deal_Different_Items_Discovered()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "B", "C" }, 130);

            var deals = matcher.Match(new[] {
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "B", Price = 50 },
                new CheckoutItem { Sku = "C", Price = 50 }
            });

            deals.Should().HaveCount(1);
        }

        [Fact]
        public void Single_Multiplier_Deal_Out_of_Order_Discovered()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "C", "B" }, 130);

            var deals = matcher.Match(new[] {
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "B", Price = 50 },
                new CheckoutItem { Sku = "C", Price = 50 }
            });

            deals.Should().HaveCount(1);
        }

        [Fact]
        public void Single_Multiplier_Deal_With_Remainders()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);

            var deals = matcher.Match(new[] {
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 }
            });

            deals.Should().HaveCount(1);
        }

        [Fact]
        public void Single_Multiplier_Deal_With_Other_Items()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);

            var deals = matcher.Match(new[] {
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "C", Price = 15 },
                new CheckoutItem { Sku = "B", Price = 30 },
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

        [Fact]
        public void Price_Modifier_Raw_Test() {

            var mod = new MultipleItemsPriceModifier(20, 15);

            mod.Modifier(100).Should().Be(95);
        }

        [Fact]
        public void Price_Modifier_From_Single_Multiplier_Deal()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);

            var modifiers = matcher.Match(new[] {
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 }
            });

            modifiers.Should().HaveCount(1);

            modifiers.ElementAt(0).Modifier(150).Should().Be(130);
        }

        [Fact]
        public void Price_Modifier_From_Single_Multiplier_Deal_With_Additional()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);

            var items = new[] {
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "B", Price = 30 }
            };

            var modifiers = matcher.Match(items);

            modifiers.Should().HaveCount(1);

            modifiers.ElementAt(0).Modifier(180).Should().Be(160);
        }
    }
}