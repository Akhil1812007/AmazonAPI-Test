using Amazon.Models;
using AmazonAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Amazon.Repository
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

        public  async Task DeleteMerchant(int? MerchantId)
        {
            try
            {
                Merchant merchant = _context.Merchants.Find(MerchantId);
                _context.Merchants.Remove(merchant);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new NotImplementedException();
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
        public async Task<MerchantToken> MerchantLogin(Merchant m)
        {
            MerchantToken mt=new MerchantToken();
            var merchant =await  (from i in _context.Merchants where i.MerchantEmail == m.MerchantEmail && i.MerchantPassword == m.MerchantPassword select i).FirstOrDefaultAsync();
            if (merchant != null)
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, merchant.MerchantEmail),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };


                var token = GetToken(authClaims);
                string s= new JwtSecurityTokenHandler().WriteToken(token);
                mt.merchantToken = s;
                mt.merchant=merchant;
                return mt;

            }
            return null;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                ); ;

            return token;
        }

        
    }
    }

