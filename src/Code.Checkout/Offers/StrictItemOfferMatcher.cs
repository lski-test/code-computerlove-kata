using Code.Utils.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Checkout.Offers
{
    public class StrictItemOfferMatcher : IOfferMatcher
    {
        private IReadOnlyList<string> _skus;
        private readonly decimal _fixedPrice;

        public int Priority { get; }

        public StrictItemOfferMatcher(IReadOnlyList<string> skus, decimal fixedPrice, short priority = 0)
        {
            _skus = skus ?? throw new ArgumentNullException(nameof(skus));

            _fixedPrice = fixedPrice >= 0 ? fixedPrice : throw new ArgumentException($"{nameof(fixedPrice)} should not be below zero");

            Priority = priority;
        }

        public OfferMatches Match(IList<OfferItem> items)
        {
            return Match(new OfferMatches(items));
        }

        public OfferMatches Match(OfferMatches results)
        {
            while (true)
            {
                var offer = ParseOffer(results.Items);

                if (offer == null)
                {
                    break;
                }

                results.Modifiers.Add(offer);
            }

            return results;
        }

        private StrictItemsPriceModifier ParseOffer(IList<OfferItem> items)
        {
            var foundPositions = new List<int>();

            foreach (var sku in _skus)
            {
                // Look for the first item that is not already assigned to a deal, is not already been found and has a matching Sku.
                var position = items.FindIndex((item, idx) => !item.InDeal && !foundPositions.Any(p => p == idx) && item.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));

                if (position == -1)
                {
                    return null;
                }

                foundPositions.Add(position);
            }

            // If the found positions match the amount of skus, then we have a match
            if (foundPositions.Count == _skus.Count)
            {
                var itemsTotal = foundPositions
                    .Select(pos => {
                        var item = items[pos];
                        item.InDeal = true;
                        return item;
                    }).Sum(item =>
                        item.Price
                    );

                return new StrictItemsPriceModifier(itemsTotal, _fixedPrice);
            }

            return null;
        }
    }
}