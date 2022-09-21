using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazonAPITesting.AmazonDBContext;
using AmazonAPI.Repository;
using AmazonAPI.Models;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;

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
            var CustomerRepository = new CustomerRepository(dbContext);

            //Act
            var result = await CustomerRepository.GetCustomerById(1001);
            //Assert
            var name = "abc0";
            name.Should().Be(result.CustomerName);
            

        }
        [Fact]
        public async Task CustomerRepo_AddCustomer_ReturnCustomer()
        {
            //Arrange
            var customer = new Customer()
            {
                CustomerName = "abcd",
                CustomerEmail = "abc5@gmail.com",
                CustomerCity = "madhapur",
                CustomerPassword = "1234",
                ConfirmPassword = "1234",

            };
            var Inmemory = new AmazonInMemoryDatabase();
            var dbContext = await Inmemory.GetDatabaseContext();
            var CustomerRepository = new CustomerRepository(dbContext);
            //Act
            var result = await CustomerRepository.AddCustomer(customer);
            //Assert
            result.Should().BeEquivalentTo(customer);
            dbContext.Customers.Should().HaveCount(11);
            
        }
        [Fact]
        public async Task CustomerRepo_UpdateCustomer_ReturnEdit()
        {
            //Arrange
            var id = 1001;
            var customer = new Customer()
            {
                CustomerId = id,
                CustomerName = "abcdefgh",
                CustomerEmail = "abcdefgh@gmail.com",
                CustomerCity = "asdfgh",
                CustomerPassword = "12345",
                ConfirmPassword = "12345",
            };
            var Inmemory = new AmazonInMemoryDatabase();
            var dbContext = await Inmemory.GetDatabaseContext();
            var CustomerRepository = new CustomerRepository(dbContext);

            //Act
            
            var  customerfind = await dbContext.Customers.FindAsync(customer.CustomerId);
            dbContext.Entry<Customer>(customerfind).State = EntityState.Detached;
            
            var result = await CustomerRepository.UpdateCustomer(id, customer);
            //Assert
            result.Should().BeEquivalentTo(customer);
            dbContext.Customers.Should().HaveCount(10);

        }
    }
}
