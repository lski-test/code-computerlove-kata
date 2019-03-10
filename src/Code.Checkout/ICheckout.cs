﻿namespace Code.Checkout
{
    public interface ICheckout
    {
        void Scan(string item);

        decimal GetTotalPrice();
    }
}