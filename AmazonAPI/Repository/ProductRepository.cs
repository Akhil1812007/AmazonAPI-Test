using Amazon.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Amazon.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AmazonContext _context;
        public ProductRepository(AmazonContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;

        }

        public async Task DeleteProduct(int productId)
        {
            try
            {
                Product? product  = _context.Products.Find(productId);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public async Task<Product> EditProduct(Product product)
        {
            _context.Update(product);
            await  _context.SaveChangesAsync();
            return product;
        }
        public async Task<List<Product>> GetAllProduct()
        {
            try
            {
                return await _context.Products.Include(x=>x.Category).ToListAsync();
            }
            catch
            {
                throw new NotImplementedException();

            }

        }
        public async Task<Product> GetProductById(int id)
        {


            return await _context.Products.FindAsync(id);



        }

        public async Task PatchProduct(JsonPatchDocument product, int id)
        {
            var P = _context.Products.FindAsync(id);
            if (product != null)
            {
                product.ApplyTo(P);
                await _context.SaveChangesAsync();  
            }
        }
    }
}
