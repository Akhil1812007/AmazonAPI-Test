﻿using Amazon.Models;

namespace Amazon.Repository
{
    public interface ICartRepository
    {
        Task<Cart> AddToCart(Cart cart);
        Task DeleteFromCart(int id);
        Task<Cart> GetCartById(int id);
        Task<List<Cart>>? GetAllCart(int CustomerId);
        Task<Cart> UpdateCart(int id,Cart c);




    }
}
