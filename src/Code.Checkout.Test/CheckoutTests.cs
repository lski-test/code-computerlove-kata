using Code.Checkout.Offers;
using Code.Checkout.Products;
using FluentAssertions;
using System;
using Xunit;

namespace Code.Checkout.Test
{
    public class CheckoutTests
    {
        [Fact]
        public void No_Items_Should_Be_Zero()
        {
            var checkout = new Checkout(new MockProductRepo(), new ProcessOffers(new MockOfferRepo()));

            checkout.TotalPrice.Should().Be(0);
        }

        [Fact]
        public void Single_Item_Should_Have_Correct_Total()
        {
            var checkout = new Checkout(new MockProductRepo(), new ProcessOffers(new MockOfferRepo()));

            checkout.Scan("A");

            checkout.TotalPrice.Should().Be(50);
        }

        [Fact]
        public void Multiple_Items_Should_Have_Correct_Total()
        {
            var checkout = new Checkout(new MockProductRepo(), new ProcessOffers(new MockOfferRepo()));

            checkout.Scan("A");
            checkout.Scan("B");

            checkout.TotalPrice.Should().Be(80);
        }

        [Fact]
        public void Multiple_Items_With_Incorrect_Sku_Should_Have_Correct_Total()
        {
            var checkout = new Checkout(new MockProductRepo(), new ProcessOffers(new MockOfferRepo()));

            checkout.Scan("A");
            checkout.Scan("unknown");
            checkout.Scan("B");

            checkout.TotalPrice.Should().Be(80);
        }

        [Fact]
        public void Scan_With_Incorrect_Sku_Should_Feedback_Correctly()
        {
            var checkout = new Checkout(new MockProductRepo(), new ProcessOffers(new MockOfferRepo()));

            checkout.Scan("A").Should().BeTrue();
            checkout.Scan("unknown").Should().BeFalse();
            checkout.Scan("B").Should().BeTrue();
        }

        [Fact]
        public void Product_Should_Convert_To_CheckoutItem()
        {
            new Product {
                Sku = "A",
                Price = 10
            }
            .ToOfferItem()
            .Should()
            .BeEquivalentTo(new OfferItem {
                Sku = "A",
                Price = 10,
                InDeal = false
            });
        }

        [Fact]
        public void Items_Matching_Single_Offer_Correct_Price()
        {
            var checkout = new Checkout(new MockProductRepo(), new ProcessOffers(new MockOfferRepo()));

            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            checkout.TotalPrice.Should().Be(130);
        }

        [Fact]
        public void Items_Matching_Single_Offer_With_Additional_Items_Correct_Price()
        {
            var checkout = new Checkout(new MockProductRepo(), new ProcessOffers(new MockOfferRepo()));

            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");

            checkout.TotalPrice.Should().Be(160);
        }

        [Fact]
        public void Items_Matching_Single_Offer_With_Additional_Items_Unordered_Correct_Price()
        {
            var checkout = new Checkout(new MockProductRepo(), new ProcessOffers(new MockOfferRepo()));

            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("A");

            checkout.TotalPrice.Should().Be(160);
        }

        [Fact]
        public void Items_Matching_Multiple_Offers_Correct_Price()
        {
            var checkout = new Checkout(new MockProductRepo(), new ProcessOffers(new MockOfferRepo()));

            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("B");

            checkout.TotalPrice.Should().Be(175);
        }

        [Fact]
        public void Items_Matching_Multiple_Offers_With_Additional_Items_Unordered_Correct_Price()
        {
            var checkout = new Checkout(new MockProductRepo(), new ProcessOffers(new MockOfferRepo()));

            checkout.Scan("D");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("D");
            checkout.Scan("A");
            checkout.Scan("C");
            checkout.Scan("A");
            checkout.Scan("B");

            checkout.TotalPrice.Should().Be(225);
        }
    }
}