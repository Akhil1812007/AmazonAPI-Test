using AmazonAPI.Models;

namespace AmazonAPI.Models
{
    public class MerchantToken
    {
        public Merchant? merchant { get; set; }
        public string? merchantToken { get; set; }
    }
}
