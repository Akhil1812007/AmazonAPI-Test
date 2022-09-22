using AmazonAPI.Models;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAPITesting.AmazonDBContext
{
    public class AmazonInMemoryDatabase
    {
        public async Task<AmazonContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<AmazonContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var databaseContext = new AmazonContext(options);
            databaseContext.Database.EnsureCreated();
            int temp = 1000;
            int CustomerInitial = 1000;
            int CartInitial = 1000;



            for (int i = 0; i < 10; i++)
                {
                    databaseContext.Merchants.Add(
                        new Merchant()
                        {
                            MerchantId = temp++,
                            MerchantEmail = "akhil" + i + "@gmail.com",
                            MerchantName = "akhil" + i,
                            MerchantPassword = "12345",
                            ConfirmPassword = "12345",
                        }

                        );
                    databaseContext.Customers.Add(
                        new Customer()
                        {
                            CustomerId = CustomerInitial++,
                            CustomerEmail = "abcd" + i + "@gmail.com",
                            CustomerName = "abc" + i,
                            CustomerPassword = "12345678",
                            ConfirmPassword = "12345678",
                        }

                        );
                    databaseContext.carts.Add(
                    new Cart()
                    {
                        CustomerId = 1000,
                        CartId = CartInitial++,
                        ProductQuantity = 12 ,
                        ProductId = 101 + i,
                    }
                   );


                    await databaseContext.SaveChangesAsync();
                    

                }
            
            return databaseContext;


        }
    }
}