using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazonAPITesting.AmazonDBContext;
using AmazonAPI.Repository;

namespace AmazonAPITesting.Amazon_Repository
{
    public class Amazon_Customer_Repository
    {
        [Fact]
        public async Task CustomerRepo_GetCustomerById_ReturnCustomerId()
        {
            //Arrange
            var Inmemory = new AmazonInMemoryDatabase();
            var dbContext=await Inmemory.GetDatabaseContext();
            var customersRepository = new CustomerRepository(dbContext);

            //Act
            var result = await customersRepository.GetCustomerById(1001);
            //Assert
            var name = "abc0";
            name.Should().Be(result.CustomerName);

        }
    }
}
