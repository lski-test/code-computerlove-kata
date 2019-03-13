using Code.Checkout.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Checkout.Offers
{
    public class ProcessOffers : IProcessOffers
    {
        private IOfferRepo _repo;

        public ProcessOffers(IOfferRepo repo)
        {
            _repo = repo;
        }

        public OfferBreakdown Apply(ICollection<Product> products)
        {
            var offers = _repo.GetOffers();
            var (items, total) = Convert(products);

            // Process the list of products, through each of the offermatchers to find matching items and create modifiers to alter the final total.
            var matches = offers
                .OrderByDescending(k => k.Priority)
                .Aggregate(
                    new OfferMatches(items),
                    (accumulator, item) => 
                        item.Match(accumulator)
                );

            // Use the list of modifiers to return a new value
            return new OfferBreakdown {
                SalePrice = matches.Modifiers.Aggregate(total, (accumulator, item) => item.Modify(accumulator))
            };
        }

        private static (List<OfferItem> items, decimal total) Convert(ICollection<Product> products)
        {
            var total = 0M;
            var items = products.Select(p => {
                total += p.Price;
                return p.ToOfferItem();
            }).ToList();

            return (items: items, total: total);
        }
    }
}