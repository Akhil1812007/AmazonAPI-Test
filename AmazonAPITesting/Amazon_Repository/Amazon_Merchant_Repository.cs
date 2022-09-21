using AmazonAPI.Models;
using AmazonAPI.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AmazonAPITesting.Amazon_Repository
{
    public class Amazon_Merchant_Repository
    {
        
        private async Task<AmazonContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<AmazonContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var databaseContext = new AmazonContext(options);
            databaseContext.Database.EnsureCreated();
            int temp = 1000;
            if (await databaseContext.Merchants.CountAsync() <= 0)
            {
                for(int i = 0; i < 10; i++)
                {
                    databaseContext.Merchants.Add(
                        new Merchant()
                        {
                            MerchantId = temp++,
                            MerchantEmail = "akhil"+i+"@gmail.com",
                            MerchantName = "akhil"+i,
                            MerchantPassword = "12345",
                            ConfirmPassword = "12345",
                        }

                        );
                    
                    
                    await databaseContext.SaveChangesAsync();

                }
            }
            return databaseContext;


        }
        [Fact]
        public async Task GetMerchantByID_ReturnMerchant()
        {
            //Arrange
            
            var Id = 1001;
            Merchant merchant = new Merchant
            {
                MerchantId = Id,
                MerchantEmail = "akhil1@gmail.com",
                MerchantName = "akhil1",
                MerchantPassword = "12345",
                ConfirmPassword = "12345",
            };
            var dbContext = await GetDatabaseContext();
            var merchantRepository = new MerchantRepository(dbContext);

            //Act
            var result = await merchantRepository.GetMerchantByID(Id);

            //Assert

            var name=result.MerchantName;
            result.Should().BeEquivalentTo(merchant);




        }
        [Fact]
        public async Task UpdateMerchant_Merchant()
        {
            //Arrange

            var Id = 1001;
            Merchant merchant = new Merchant
            {
                MerchantId = Id,
                MerchantEmail = "akhil1@gmail.com",
                MerchantName = "updatedAkhil",
                MerchantPassword = "12345",
                ConfirmPassword = "12345",
            };
            var dbContext = await GetDatabaseContext();
            var merchantRepository = new MerchantRepository(dbContext);
            

            //Act
            var result = await merchantRepository.UpdateMerchant(Id,merchant);
            //Assert

            var name = result.MerchantName;
            "updatedAkhil".Should().BeEquivalentTo(name);

        }
        [Fact]
        public async Task DeleteMercahnt_Returnbool()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var merchantRepository = new MerchantRepository(dbContext);

            //Act
            var result= await merchantRepository.DeleteMerchant(1000);

            //Assert
            result.Should().BeTrue();

        }


    }
}
