using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Checkout.Offers
{
    public class MultipleItemsMatcher : IOfferMatcher
    {
        private IEnumerable<string> _skus;
        private readonly decimal _fixedPrice;

        public int Priority { get; }

        public MultipleItemsMatcher(IReadOnlyList<string> skus, decimal fixedPrice, short priority = 0)
        {
            _skus = skus ?? throw new ArgumentNullException(nameof(skus));

            _fixedPrice = fixedPrice >= 0 ? fixedPrice : throw new ArgumentException($"{nameof(fixedPrice)} should not be below zero");

            Priority = priority;
        }

        public IEnumerable<IPriceModifer> Match(IReadOnlyList<CheckoutItem> items)
        {
            //var itemSearchStartPos = 0;

            //foreach (var item in items)
            //{
            //    var


            //}

            //for (int i = 0, n = items.Count(); i < n; i++)
            //{

            //    var item = items[0];

            //    if (sku.Equals(item.Sku, StringComparison.OrdinalIgnoreCase))
            //    {
            //        matchedItems.Add(item);
            //        itemSearchStartPos = i + 1;
            //        break;
            //    }
            //}


            // need to know at the end is 'next position in items'


            var itemSearchStartPos = 0;
            var offers = new List<IPriceModifer>();

            while (itemSearchStartPos < items.Count) {

                var offer = ParseOffer();

                if (offer != null)
                {
                    offers.Add(offer);
                }
            }

            return offers;

            //var matchedItems = new List<CheckoutItem>();
            //var parsedItems = items.Where(i => !i.InDeal);

            //foreach (var sku in _skus)
            //{
            //    for (int i = itemSearchStartPos, n = items.Count(); i < n; i++) {

            //        var item = items[0];

            //        if (sku.Equals(item.Sku, StringComparison.OrdinalIgnoreCase)) {
            //            matchedItems.Add(item);
            //            itemSearchStartPos = i + 1;
            //            break;
            //        }
            //    }
            //}

            //// If the whole deal is complete then return the deal modifier
            //// NB: Need to modify the items so they are ignored in future checks
            //if (matchedItems.Count() == _skus.Count()) {
            //    // TODO: hould be checking for more items that match the deal
            //    return new[] {
            //        new MultipleItemsPriceModifier {

            //        }
            //    };
            //}

            //return new MultipleItemsPriceModifier[] { };

            MultipleItemsPriceModifier ParseOffer()
            {
                var matchedItems = new List<CheckoutItem>();
                var parsedItems = items.Where(i => !i.InDeal);

                foreach (var sku in _skus)
                {
                    for (int i = itemSearchStartPos, n = items.Count(); i < n; i++)
                    {

                        var item = items[0];

                        if (sku.Equals(item.Sku, StringComparison.OrdinalIgnoreCase))
                        {
                            matchedItems.Add(item);
                            itemSearchStartPos = i + 1;
                            break;
                        }
                    }
                }

                // If the whole deal is complete then return the deal modifier
                // NB: Need to modify the items so they are ignored in future checks
                if (matchedItems.Count() == _skus.Count())
                {
                    // TODO: hould be checking for more items that match the deal
                    return
                        new MultipleItemsPriceModifier {

                        }
                    ;
                }

                return null;
            }
        }
    }
}