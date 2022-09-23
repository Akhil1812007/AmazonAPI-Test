using AmazonAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace AmazonAPI.Repository
{
    public class IsExistReturn
    {
        public bool isExist;
        public  Cart cart;
        public IsExistReturn(bool isExist,Cart cart)
        {
            this.isExist = isExist;
            this.cart = cart;
        }
    }

    public class CartRepository : ICartRepository 
    {
        
        private readonly AmazonContext _context;
        public CartRepository( AmazonContext context)
        {
            _context = context;
        }

        public async  Task<IsExistReturn>? AddToCart(Cart cart)
        {
            
            IsExistReturn ans = await isCartExist(cart);
            
            if (ans.isExist)
            {
                return ans;
            }
            else
            {
                ans.cart = cart;
                _context.carts.Add(cart);
                await _context.SaveChangesAsync();

                return ans;
            }
            
        }
       
        public async  Task<bool> DeleteFromCart(int id)
        {
            try
            {
                Cart? cart = _context.carts.Find(id);
                _context.carts.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;   
            }
            
        }

        public async   Task<List<Cart>>? GetAllCart(int customerId)

        {
            Customer customer = _context.Customers.Find(customerId);
            List<Cart> cartList = null;
            if (customer!= null)
            {

                 cartList = await (from c in _context.carts.Include(x => x.Product) where c.CustomerId == customerId select c).ToListAsync();
                return cartList;
            }
            return cartList;  
        }

        public async  Task<Cart> GetCartById(int cartid)
        {
            var result= await (from c in _context.carts.Include(x => x.Product) where c.CartId == cartid select c).SingleAsync();
            return result;
        }
        private async  Task<IsExistReturn> isCartExist(Cart ct)
        {
            var cart = ( from  c in _context.carts where c.ProductId==ct.ProductId && c.CustomerId==ct.CustomerId select c).FirstOrDefault();
            if(cart == null)
            {
                return new IsExistReturn(false,null);
            }
            else
            {
                //var c=UpdateCart(cart.CartId, cart);
                cart.ProductQuantity += ct.ProductQuantity;

                _context.carts.Update(cart);
              
                await _context.SaveChangesAsync();
                return new IsExistReturn(true, cart); 
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
