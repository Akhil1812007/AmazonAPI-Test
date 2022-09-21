using AmazonAPI.Models;
using AmazonAPI.Models;

namespace AmazonAPI.Repository
{
    public interface IMerchantRepository 
    {
        public Task<List<Merchant>> GetMerchant();
        public Task<Merchant> GetMerchantByID(int MerchantId);
        public Task<Merchant> InsertMerchant(Merchant Merchant);
        public Task<bool> DeleteMerchant(int? MerchantId);
        public Task<Merchant> UpdateMerchant(int MerchantId,Merchant merchant);
        public Task<MerchantToken> MerchantLogin(Merchant merchant);

        public Task<List<Product>> GetProductByMerchantId(int id);



    }
}
