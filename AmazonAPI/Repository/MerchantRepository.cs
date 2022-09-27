using AmazonAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AmazonAPI.Repository
{
    public  class MerchantRepository : IMerchantRepository
    {
        private readonly AmazonContext _context;
        private readonly IConfiguration _configuration;
        public MerchantRepository(AmazonContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public MerchantRepository(AmazonContext context)
        {
            _context = context;
        }

        public  async Task<bool> DeleteMerchant(int? MerchantId)
        {
            try
            {
                Merchant merchant = _context.Merchants.Find(MerchantId);
                _context.Merchants.Remove(merchant);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Merchant>> GetMerchant()
        {
            try
            {
                return await _context.Merchants.ToListAsync();
            }
            catch
            {
                throw new NotImplementedException();

            }
        }

        public async Task<Merchant> GetMerchantByID(int MerchantId)
        {
            return await _context.Merchants.FindAsync(MerchantId);
          
        }
        
        public async Task<List<Product>> GetProductByMerchantId(int MerchantId)
        {
            var products = await (from i in _context.Products.Include(x=>x.Category).Include(y=>y.Merchant) where i.MerchantId == MerchantId select i).ToListAsync();
            return products;
        }

        public async Task<Merchant> InsertMerchant(Merchant merchant)
        {
            _context.Merchants.Add(merchant);
            await _context.SaveChangesAsync();
            return merchant;


        }
        private bool IsMerchant(int id)
        {
            var isMerchant = _context.Merchants.Find(id);
            if (isMerchant != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
     

        public async Task<Merchant> UpdateMerchant(int MerchantId, Merchant Merchant)
        {
            //Merchant _merchant = await _context.Merchants.FindAsync(MerchantId);
            _context.Update(Merchant);
           
            
            _context.SaveChanges();
            return Merchant;

        }
        public async Task<Merchant> MerchantLogin(Merchant m)
        {
            var merchant =await  (from i in _context.Merchants where i.MerchantEmail == m.MerchantEmail && i.MerchantPassword == m.MerchantPassword select i).FirstOrDefaultAsync();
            if (merchant != null)
            {
                return merchant;

            }
            return null;
        }

        

        
    }
    }

