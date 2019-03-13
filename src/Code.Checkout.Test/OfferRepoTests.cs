using System;
using Xunit;
using FluentAssertions;
using System.Linq;
using Code.Checkout.Offers;

namespace Code.Checkout.Test
{
    public class OfferRepoTests
    {
        [Fact]
        public void Offers_Returned()
        {
            var repo = new MockOfferRepo();

            var offers = repo.GetOffers().ToList();

            offers.Count.Should().Be(2);

            offers[0].Should().BeEquivalentTo(new StrictItemOfferMatcher(new[] { "A", "A", "A" }, 130));
            offers[1].Should().BeEquivalentTo(new StrictItemOfferMatcher(new[] { "B", "B" }, 45));
        }
    }
}