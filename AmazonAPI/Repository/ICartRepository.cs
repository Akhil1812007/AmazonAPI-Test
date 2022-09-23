using AmazonAPI.Models;

namespace AmazonAPI.Repository
{
    public interface ICartRepository
    {
        Task<IsExistReturn> AddToCart(Cart cart);
        Task<bool> DeleteFromCart(int id);
        Task<Cart> GetCartById(int id);
        Task<List<Cart>>? GetAllCart(int CustomerId);
        Task<Cart> UpdateCart(int id,Cart c);




    }
}
