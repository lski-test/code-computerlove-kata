using Code.Checkout.Offers;
using Code.Checkout.Products;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Code.Checkout.Test
{
    public class OfferTests
    {
        [Fact]
        public void Chain_Two_Deal_Matchers()
        {
            var matchers = new[] {
                new StrictItemOfferMatcher(new[] { "A", "A", "A" }, 130),
                new StrictItemOfferMatcher(new[] { "B", "B" }, 45)
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
                new StrictItemOfferMatcher(new[] { "A", "A", "A" }, 130),
                new StrictItemOfferMatcher(new[] { "B", "B" }, 45)
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
                new StrictItemOfferMatcher(new[] { "A", "A", "A" }, 130),
                new StrictItemOfferMatcher(new[] { "B", "B" }, 45),
                new StrictItemOfferMatcher(new[] { "C", "C" }, 30)
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
        public void Offers_Apply_No_Offers()
        {
            var processor = new ProcessOffers(new MockOfferRepo(new List<IOfferMatcher>()));
            var products = new[] {
                new Product { Sku = "A", Price = 50 },
                new Product { Sku = "A", Price = 50 },
                new Product { Sku = "A", Price = 50 },
                new Product { Sku = "C", Price = 20 },
                new Product { Sku = "C", Price = 20 },
                new Product { Sku = "B", Price = 30 },
                new Product { Sku = "B", Price = 30 },
                new Product { Sku = "D", Price = 20 }
            };
            var productsTotal = products.Sum(p => p.Price);

            var breakdown = processor.Apply(products);

            (productsTotal - breakdown.SalePrice).Should().Be(0);
        }

        [Fact]
        public void Offers_Apply_With_Offers()
        {
            var processor = new ProcessOffers(new MockOfferRepo());
            var products = new[] {
                new Product { Sku = "A", Price = 50 },
                new Product { Sku = "A", Price = 50 },
                new Product { Sku = "A", Price = 50 },
                new Product { Sku = "C", Price = 20 },
                new Product { Sku = "C", Price = 20 },
                new Product { Sku = "B", Price = 30 },
                new Product { Sku = "B", Price = 30 },
                new Product { Sku = "D", Price = 20 }
            };
            var productsTotal = products.Sum(p => p.Price);

            var breakdown = processor.Apply(products);

            (productsTotal - breakdown.SalePrice).Should().Be(35);
        }

        [Fact]
        private void Offers_Applied_In_Correct_Order()
        {
            var processor = new ProcessOffers(new MockOfferRepo(new List<IOfferMatcher> {
                 new StrictItemOfferMatcher(new[] { "B", "B" }, 130),
                 new StrictItemOfferMatcher(new[] { "A", "B", "B" }, 45, 1)
            }));

            var products = new[] {
                new Product { Sku = "A", Price = 50 },
                new Product { Sku = "A", Price = 50 },
                new Product { Sku = "A", Price = 50 },
                new Product { Sku = "C", Price = 20 },
                new Product { Sku = "C", Price = 20 },
                new Product { Sku = "B", Price = 30 },
                new Product { Sku = "B", Price = 30 },
                new Product { Sku = "D", Price = 20 }
            };
            var productsTotal = products.Sum(p => p.Price);

            var breakdown = processor.Apply(products);

            (productsTotal - breakdown.SalePrice).Should().Be(65);
        }
    }
}