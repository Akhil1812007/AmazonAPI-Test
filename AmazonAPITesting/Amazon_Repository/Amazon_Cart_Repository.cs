using AmazonAPI.Repository;
using AmazonAPITesting.AmazonDBContext;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAPITesting.Amazon_Repository
{
    public class Amazon_Cart_Repository
    {
        [Fact]
        public async Task CartRepository_GetAllCartsByCustomerId_ReturnGetCarts()
        {
            //Arrange
            var Inmemory = new AmazonInMemoryDatabase();
            var dbContext = await Inmemory.GetDatabaseContext();
            var cartRepository = new CartRepository(dbContext);

             
            //Act  --for a customer whose is present
            var result = await cartRepository.GetAllCart(1000);
            //Assert
            
            10.Should().Be(result.Count());
            var tempdata = result.First();
            12.Should().Be(tempdata.ProductQuantity);


             
            //Act -- for the customer who is not present ie customerId not in database
            var result1 = await cartRepository.GetAllCart(1);
            //Assert
            result1.Should().BeNull();
        }
        [Fact]
        public async Task CartRepository_GetCartById_ReturnCarts()
        {
            //Arrange
            var InmemoryDataBase = new AmazonInMemoryDatabase();
            var dbContext = await InmemoryDataBase.GetDatabaseContext();
            var cartRepository = new CartRepository(dbContext);
            //Act
            var result = await cartRepository.GetCartById(1002);
            //Assert
            12.Should().Be(result.ProductQuantity);
        }
    }
}
