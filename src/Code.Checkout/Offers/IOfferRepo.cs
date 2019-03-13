using System;

namespace Code.Checkout.Offers
{
    public interface IOfferRepo
    {
        IOffers GetOffers();
    }
}