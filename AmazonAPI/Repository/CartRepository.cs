using AmazonAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonAPI.Repository
{
    
    public class CartRepository : ICartRepository
    {
        
        private readonly AmazonContext _context;
        public CartRepository( AmazonContext context)
        {
            _context = context;
        }

        public async  Task<Cart>? AddToCart(Cart cart)
        {
            if (await isCartExist(cart))
            {
                return cart;
            }
            else
            {
                _context.carts.Add(cart);
                await _context.SaveChangesAsync();
                return cart;
            }
            
        }
       
        public async  Task DeleteFromCart(int id)
        {
            
                Cart? cart = _context.carts.Find(id);
                _context.carts.Remove(cart);
                await _context.SaveChangesAsync();
            
        }

        public async   Task<List<Cart>>? GetAllCart(int customerId)
        {
            List<Cart> cartList = await (from c in _context.carts.Include(x => x.Product) where c.CustomerId == customerId select c).ToListAsync();

            return  cartList;    
        }

        public async  Task<Cart> GetCartById(int cartid)
        {
            var result= await (from c in _context.carts.Include(x => x.Product) where c.CartId == cartid select c).SingleAsync();
            return result;
        }
        private async  Task<bool> isCartExist(Cart ct)
        {
              var cart = ( from  c in _context.carts where c.ProductId==ct.ProductId && c.CustomerId==ct.CustomerId select c).FirstOrDefault();
            if(cart == null)
            {
                return false;
            }
            else
            {
                //var c=UpdateCart(cart.CartId, cart);
                cart.ProductQuantity += ct.ProductQuantity;

                _context.carts.Update(cart);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        public async  Task<Cart> UpdateCart(int id,Cart c)
        {
            _context.Update(c);
            await _context.SaveChangesAsync();
            return c;

        }
    }
}
