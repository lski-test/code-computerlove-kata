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
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 }
            });

            deals.Modifiers.Should().HaveCount(1);
        }

        [Fact]
        public void Single_Multiplier_Deal_Different_Items_Discovered()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "B", "C" }, 130);

            var deals = matcher.Match(new[] {
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "B", Price = 50 },
                new OfferItem { Sku = "C", Price = 50 }
            });

            deals.Modifiers.Should().HaveCount(1);
        }

        [Fact]
        public void Single_Multiplier_Deal_Out_of_Order_Discovered()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "C", "B" }, 130);

            var deals = matcher.Match(new[] {
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "B", Price = 50 },
                new OfferItem { Sku = "C", Price = 50 }
            });

            deals.Modifiers.Should().HaveCount(1);
        }

        [Fact]
        public void Single_Multiplier_Deal_With_Remainders()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);

            var deals = matcher.Match(new[] {
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 }
            });

            deals.Modifiers.Should().HaveCount(1);
        }

        [Fact]
        public void Single_Multiplier_Deal_With_Other_Items()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);

            var deals = matcher.Match(new[] {
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "C", Price = 15 },
                new OfferItem { Sku = "B", Price = 30 },
                new OfferItem { Sku = "A", Price = 50 }
            });

            deals.Modifiers.Should().HaveCount(1);
        }

        [Fact]
        public void Several_Multiplier_Deals_Correctly_Discovered()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);

            var deals = matcher.Match(new[] {
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 }
            });

            deals.Modifiers.Should().HaveCount(2);
        }

        [Fact]
        public void No_Deals_Correctly_Discovered()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);

            var deals = matcher.Match(new[] {
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "B", Price = 50 },
                new OfferItem { Sku = "C", Price = 50 }
            });

            deals.Modifiers.Should().HaveCount(0);
        }

        [Fact]
        public void Price_Modifier_Raw_Test()
        {
            var mod = new MultipleItemsPriceModifier(20, 15);

            mod.Modify(100).Should().Be(95);
        }

        [Fact]
        public void Price_Modifier_From_Single_Multiplier_Deal()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);

            var modifiers = matcher.Match(new[] {
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 }
            });

            modifiers.Modifiers.Should().HaveCount(1);

            modifiers.Modifiers[0].Modify(150).Should().Be(130);
        }

        [Fact]
        public void Price_Modifier_From_Single_Multiplier_Deal_With_Additional()
        {
            var matcher = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);

            var items = new[] {
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "B", Price = 30 }
            };

            var modifiers = matcher.Match(items);

            modifiers.Modifiers.Should().HaveCount(1);

            modifiers.Modifiers[0].Modify(180).Should().Be(160);
        }

        [Fact]
        public void Two_Deal_Matchers()
        {
            var matcher1 = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130);
            var matcher2 = new MultipleItemsMatcher(new[] { "B", "B" }, 45);

            var items = new[] {
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "B", Price = 30 },
                new OfferItem { Sku = "B", Price = 30 }
            };

            var itemsPrice = items.Sum(i => i.Price);

            var modifier1 = matcher1.Match(items);
            var modifier2 = matcher2.Match(items);

            modifier1.Modifiers.Count().Should().Be(1);
            modifier2.Modifiers.Count().Should().Be(1);

            var price = modifier1.Modifiers.Aggregate(itemsPrice, (accumulator, item) => item.Modify(accumulator));
            price = modifier2.Modifiers.Aggregate(price, (accumulator, item) => item.Modify(accumulator));

            (itemsPrice - price).Should().Be(35);
        }

        [Fact]
        public void Two_Deal_Matchers_With_Additionals()
        {
            var matcher1 = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130); // reduction of 20
            var matcher2 = new MultipleItemsMatcher(new[] { "B", "B" }, 45); // reduction of 15

            var items = new[] {
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "D", Price = 20 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "C", Price = 20 },
                new OfferItem { Sku = "B", Price = 30 },
                new OfferItem { Sku = "D", Price = 20 },
                new OfferItem { Sku = "B", Price = 30 },
                new OfferItem { Sku = "D", Price = 20 }
            };

            var itemsPrice = items.Sum(i => i.Price);

            var modifier1 = matcher1.Match(items);
            var modifier2 = matcher2.Match(items);

            modifier1.Modifiers.Count().Should().Be(1);
            modifier2.Modifiers.Count().Should().Be(1);

            var price = modifier1.Modifiers.Aggregate(itemsPrice, (accumulator, item) => item.Modify(accumulator));
            price = modifier2.Modifiers.Aggregate(price, (accumulator, item) => item.Modify(accumulator));

            (itemsPrice - price).Should().Be(35);
        }

        [Fact]
        public void Multiple_Deal_Matchers_With_Additionals()
        {
            var matcher1 = new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130); // reduction of 20
            var matcher2 = new MultipleItemsMatcher(new[] { "B", "B" }, 45); // reduction of 15
            var matcher3 = new MultipleItemsMatcher(new[] { "C", "C" }, 30); // reduction of 10

            var items = new[] {
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "C", Price = 20 },
                new OfferItem { Sku = "C", Price = 20 },
                new OfferItem { Sku = "B", Price = 30 },
                new OfferItem { Sku = "B", Price = 30 },
                new OfferItem { Sku = "D", Price = 20 }
            };

            var itemsPrice = items.Sum(i => i.Price);

            var modifier1 = matcher1.Match(items);
            var modifier2 = matcher2.Match(items);
            var modifier3 = matcher3.Match(items);

            modifier1.Modifiers.Count().Should().Be(1);
            modifier2.Modifiers.Count().Should().Be(1);
            modifier3.Modifiers.Count().Should().Be(1);

            var price = modifier1.Modifiers.Aggregate(itemsPrice, (accumulator, item) => item.Modify(accumulator));
            price = modifier2.Modifiers.Aggregate(price, (accumulator, item) => item.Modify(accumulator));
            price = modifier3.Modifiers.Aggregate(price, (accumulator, item) => item.Modify(accumulator));

            (itemsPrice - price).Should().Be(45);
        }

        [Fact]
        public void Chain_Two_Deal_Matchers()
        {
            var matchers = new[] {
                new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130),
                new MultipleItemsMatcher(new[] { "B", "B" }, 45)
            };

            var items = new[] {
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "B", Price = 30 },
                new OfferItem { Sku = "B", Price = 30 }
            };

            var itemsPrice = items.Sum(i => i.Price);

            var matches = matchers.Aggregate(new OfferMatches(items), (accumulator, item) => item.Match(accumulator));

            var price = matches.Modifiers.Aggregate(itemsPrice, (accumulator, item) => item.Modify(accumulator));

            (itemsPrice - price).Should().Be(35);
        }

        [Fact]
        public void Chain_Two_Deal_Matchers_With_Additionals()
        {
            var matchers = new[] {
                new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130),
                new MultipleItemsMatcher(new[] { "B", "B" }, 45)
            };

            var items = new[] {
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "C", Price = 20 },
                new OfferItem { Sku = "C", Price = 20 },
                new OfferItem { Sku = "B", Price = 30 },
                new OfferItem { Sku = "B", Price = 30 },
                new OfferItem { Sku = "D", Price = 20 }
            };

            var itemsPrice = items.Sum(i => i.Price);

            var matches = matchers.Aggregate(new OfferMatches(items), (accumulator, item) => item.Match(accumulator));

            var price = matches.Modifiers.Aggregate(itemsPrice, (accumulator, item) => item.Modify(accumulator));

            (itemsPrice - price).Should().Be(35);
        }

        [Fact]
        public void Chain_Multiple_Deal_Matchers_With_Additionals()
        {
            var matchers = new[] {
                new MultipleItemsMatcher(new[] { "A", "A", "A" }, 130),
                new MultipleItemsMatcher(new[] { "B", "B" }, 45),
                new MultipleItemsMatcher(new[] { "C", "C" }, 30)
            };

            var items = new[] {
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "A", Price = 50 },
                new OfferItem { Sku = "C", Price = 20 },
                new OfferItem { Sku = "C", Price = 20 },
                new OfferItem { Sku = "B", Price = 30 },
                new OfferItem { Sku = "B", Price = 30 },
                new OfferItem { Sku = "D", Price = 20 }
            };

            var itemsPrice = items.Sum(i => i.Price);

            var matches = matchers.Aggregate(new OfferMatches(items), (accumulator, item) => item.Match(accumulator));

            var price = matches.Modifiers.Aggregate(itemsPrice, (accumulator, item) => item.Modify(accumulator));

            (itemsPrice - price).Should().Be(45);
        }

        [Fact]
        public void Offers_Combine_Multiple_Matchers()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Offers_Apply_Offers()
        {
            throw new NotImplementedException();
        }

        [Fact]
        private void Offers_Applied_In_Correct_Order()
        {
            throw new NotImplementedException();
        }
    }
}