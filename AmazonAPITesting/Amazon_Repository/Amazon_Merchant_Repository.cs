using AmazonAPI.Models;
using AmazonAPI.Repository;
using AmazonAPITesting.AmazonDBContext;
using Castle.Core.Configuration;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Equivalency.Tracing;
using LinqToDB.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

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
                    foreach (var tracker in databaseContext.ChangeTracker.Entries<Merchant>())
                    {
                        //Console.WriteLine(tracker.State); 
                        Console.WriteLine(databaseContext.ChangeTracker.DebugView.ShortView);
                    }

                    await databaseContext.SaveChangesAsync();
                    foreach (var tracker in databaseContext.ChangeTracker.Entries<Merchant>())
                    {
                        //Console.WriteLine(tracker.State); 
                        Console.WriteLine(databaseContext.ChangeTracker.DebugView.ShortView);
                    }
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
        public async Task DeleteMercahnt_Returnbool()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var merchantRepository = new MerchantRepository(dbContext);

            //Act
            var result= await merchantRepository.DeleteMerchant(1000);
            Console.WriteLine(dbContext.ChangeTracker.DebugView.ShortView);
            foreach (var tracker in dbContext.ChangeTracker.Entries<Merchant>())
            {
                Console.WriteLine(tracker.State);
                Console.WriteLine(dbContext.ChangeTracker.DebugView.ShortView);
            }
            //Assert
            result.Should().BeTrue();

        }
        [Fact]
        public async Task MerchantRepo_UpdateMerchant_ReturnMerchant()
        {
            //Arrange
            var id = 1001;
            Merchant traders = new Merchant()
            {
                MerchantId = id,
                MerchantEmail = "abc1@gmail.com",
                MerchantName = "abcrdrrd",
                MerchantPassword = "12345678",
                ConfirmPassword = "12345678",
            };

            var dbContext = await GetDatabaseContext();
            var traderRepository = new MerchantRepository(dbContext);
            //Act
            var trader = await dbContext.Merchants.FindAsync(id);
            dbContext.Entry<Merchant>(trader).State = EntityState.Detached;//has to be used only on xUnittesting
            foreach (var tracker in dbContext.ChangeTracker.Entries<Merchant>())
            {
                Console.WriteLine(tracker.State);
            }
            
            var result = await traderRepository.UpdateMerchant(id, traders);
            //Assert
            result.Should().BeEquivalentTo(traders);
            dbContext.Merchants.Should().HaveCount(10);
            foreach(var tracker in dbContext.ChangeTracker.Entries<Merchant>())
            {
                Console.WriteLine(tracker.State);
            }
        }
        [Fact]
        public async Task MerchantRepo_AddMerchant_ReturnMerchant()
        {
            Merchant merchant = new Merchant()
            {
                MerchantEmail = "Akhil@gmail.com",
                MerchantName = "abc3",
                MerchantPassword = "12345678",
                ConfirmPassword = "12345678",
            };
            //Arrange
            var dbContext = await GetDatabaseContext();
            var traderRepository = new MerchantRepository(dbContext);
            //Act
            var result = await traderRepository.InsertMerchant(merchant);
            //Assert
            result.Should().BeEquivalentTo(merchant);
            dbContext.Merchants.Count().Should().Be(11);
            foreach (var tracker in dbContext.ChangeTracker.Entries<Merchant>())
            {
                //Console.WriteLine(tracker.State); 
                Console.WriteLine(dbContext.ChangeTracker.DebugView.ShortView);
            }

        }
        [Fact]
        public async Task MerchantRepo_GetAllMerchant_ReturnMerchant()
        {
            //Arrange
            
            var dbContext = await GetDatabaseContext(); //This one calls the inmemory database
            var MerchantRepository = new MerchantRepository(dbContext); //repo layer object calling

            //Act
            var result = await MerchantRepository.GetMerchant(); //calling the methods of repository
            //Assert
            var count = result.Count();
            dbContext.Merchants.Should().HaveCount(count);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task MerchantRepository_GetProductByMerchant_ReturnsProduct()
        {
            //Arrange
            var Inmemory = new AmazonInMemoryDatabase();
            var dbContext = await Inmemory.GetDatabaseContext();
            var merchantRepository = new MerchantRepository(dbContext);

            //Act
            var result = await merchantRepository.GetProductByMerchantId(1000);
            //Assert
            var tempProduct = result[1];
            // result.Count().Should().Be(4);
            "Boost2".Should().Be(tempProduct.ProductName);
        }
        private readonly IConfiguration _configuration;

        [Fact]
        public async Task MerchantREpository_MerchantLogin_ReturnMerchant()
        {
            //Arrange
            
            MerchantToken ExpectedMerchantToken = new MerchantToken()
            {
                merchant = new Merchant()
                {
                    MerchantId = 1001,
                    MerchantEmail = "akhil0@gmail.com",
                    MerchantName = "akhil0",
                    MerchantPassword = "12345",
                    ConfirmPassword = "12345",
                },
                merchantToken = "nfkvjdbkjvfjkbfekjbvkjkjbkjbnoiu9898",


            };
            Merchant merchant = new Merchant()
            {
                MerchantEmail = "akhil0@gmail.com",
                MerchantName = "akhil0",
                MerchantPassword = "12345",
                ConfirmPassword = "12345",
            };

            var Inmemory = new AmazonInMemoryDatabase();
            var dbContext = await Inmemory.GetDatabaseContext();
            var InMemeoryConf = _configuration;
            var merchantRepository = new MerchantRepository(dbContext, InMemeoryConf);

            //Act
            var result = await merchantRepository.MerchantLogin(merchant);

            //assert
            result.Should().NotBeNull();
        }

    }
}
