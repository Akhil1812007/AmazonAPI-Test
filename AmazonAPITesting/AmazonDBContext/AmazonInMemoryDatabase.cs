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
            int MerchantInitial = 1000;
            int CustomerInitial = 2000;
            int CartInitial = 3000;
            int ProductInitial = 4000;
            int orderMasterInitial = 5000;



            for (int i = 0; i < 5; i++)
                {
                    databaseContext.Merchants?.Add(
                        new Merchant()
                        {
                            MerchantId = MerchantInitial++,
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
                        CustomerId = 2000,
                        CartId = CartInitial++,
                        ProductQuantity = 12 ,
                        ProductId = 4000,
                    }
                   );
                   databaseContext.Products.Add(
                        new Product()
                        {
                            MerchantId = 1000,
                            ProductId = ProductInitial++,
                            ProductName = "Boost" + i,
                            UnitPrice = 150 + i,
                            ProductQnt = 12,
                        }

                        );
                databaseContext.OrderMasters.Add(
                         new OrderMaster()
                         {
                             OrderMasterId = orderMasterInitial++,
                             CustomerId = 1000,
                             CardNumber = 12345678+ ++i,
                             total = 1200 + i,
                             AmountPaid = 1200 + i,

                         }
                        );

                await databaseContext.SaveChangesAsync();
                    

                }
            
            return databaseContext;


        }
    }
}