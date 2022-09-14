using Amazon.Models;
using AmazonAPI.Models;

namespace Amazon.Repository
{
    public interface IMerchantRepository 
    {
        public Task<List<Merchant>> GetMerchant();
        public Task<Merchant> GetMerchantByID(int MerchantId);
        public Task<Merchant> InsertMerchant(Merchant Merchant);
        public Task DeleteMerchant(int? MerchantId);
        public Task<Merchant> UpdateMerchant(int MerchantId,Merchant merchant);
        public Task<MerchantToken> MerchantLogin(Merchant merchant);

        public Task<List<Product>> GetProductByMerchantId(int id);



    }
}
