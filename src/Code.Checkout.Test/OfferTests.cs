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

            deals.Modifiers.Should().HaveCount(1);
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

            deals.Modifiers.Should().HaveCount(1);
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

            deals.Modifiers.Should().HaveCount(1);
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

            deals.Modifiers.Should().HaveCount(1);
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

            deals.Modifiers.Should().HaveCount(1);
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

            deals.Modifiers.Should().HaveCount(2);
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

            deals.Modifiers.Should().HaveCount(0);
        }

        [Fact]
        public void Price_Modifier_Raw_Test()
        {
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

            modifiers.Modifiers.Should().HaveCount(1);

            modifiers.Modifiers[0].Modifier(150).Should().Be(130);
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

            modifiers.Modifiers.Should().HaveCount(1);

            modifiers.Modifiers[0].Modifier(180).Should().Be(160);
        }

        [Fact]
        public void Two_Deal_Matchers()
        {
            var matcher1 = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);
            var matcher2 = new MultipleItemsMatcher(new[] { "B", "B" }, 45);

            var items = new[] {
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "B", Price = 30 },
                new CheckoutItem { Sku = "B", Price = 30 }
            };

            var itemsPrice = items.Sum(i => i.Price);

            var modifier1 = matcher1.Match(items);
            var modifier2 = matcher2.Match(items);

            modifier1.Modifiers.Count().Should().Be(1);
            modifier2.Modifiers.Count().Should().Be(1);

            var price = modifier1.Modifiers.Aggregate(itemsPrice, (accumulator, item) => item.Modifier(accumulator));
            price = modifier2.Modifiers.Aggregate(price, (accumulator, item) => item.Modifier(accumulator));

            (itemsPrice - price).Should().Be(35);
        }

        [Fact]
        public void Two_Deal_Matchers_With_Additionals()
        {
            var matcher1 = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130); // reduction of 20
            var matcher2 = new MultipleItemsMatcher(new[] { "B", "B" }, 45); // reduction of 15

            var items = new[] {
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "D", Price = 20 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "C", Price = 20 },
                new CheckoutItem { Sku = "B", Price = 30 },
                new CheckoutItem { Sku = "D", Price = 20 },
                new CheckoutItem { Sku = "B", Price = 30 },
                new CheckoutItem { Sku = "D", Price = 20 }
            };

            var itemsPrice = items.Sum(i => i.Price);

            var modifier1 = matcher1.Match(items);
            var modifier2 = matcher2.Match(items);

            modifier1.Modifiers.Count().Should().Be(1);
            modifier2.Modifiers.Count().Should().Be(1);

            var price = modifier1.Modifiers.Aggregate(itemsPrice, (accumulator, item) => item.Modifier(accumulator));
            price = modifier2.Modifiers.Aggregate(price, (accumulator, item) => item.Modifier(accumulator));

            (itemsPrice - price).Should().Be(35);
        }

        [Fact]
        public void Multiple_Deal_Matchers_With_Additionals()
        {
            var matcher1 = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130); // reduction of 20
            var matcher2 = new MultipleItemsMatcher(new[] { "B", "B" }, 45); // reduction of 15
            var matcher3 = new MultipleItemsMatcher(new[] { "C", "C" }, 30); // reduction of 10

            var items = new[] {
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "A", Price = 50 },
                new CheckoutItem { Sku = "C", Price = 20 },
                new CheckoutItem { Sku = "C", Price = 20 },
                new CheckoutItem { Sku = "B", Price = 30 },
                new CheckoutItem { Sku = "B", Price = 30 },
                new CheckoutItem { Sku = "D", Price = 20 }
            };

            var itemsPrice = items.Sum(i => i.Price);

            var modifier1 = matcher1.Match(items);
            var modifier2 = matcher2.Match(items);
            var modifier3 = matcher3.Match(items);

            modifier1.Modifiers.Count().Should().Be(1);
            modifier2.Modifiers.Count().Should().Be(1);
            modifier3.Modifiers.Count().Should().Be(1);

            var price = modifier1.Modifiers.Aggregate(itemsPrice, (accumulator, item) => item.Modifier(accumulator));
            price = modifier2.Modifiers.Aggregate(price, (accumulator, item) => item.Modifier(accumulator));
            price = modifier3.Modifiers.Aggregate(price, (accumulator, item) => item.Modifier(accumulator));

            (itemsPrice - price).Should().Be(45);
        }

        [Fact]
        public void Chain_Two_Deal_Matchers()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Chain_Two_Deal_Matchers_With_Additionals()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Chain_Multiple_Deal_Matchers_With_Additionals()
        {
            throw new NotImplementedException();
        }
    }
}