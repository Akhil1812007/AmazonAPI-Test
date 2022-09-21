using AmazonAPI.Controllers;
using AmazonAPI.Models;
using AmazonAPI.Repository;
using Castle.Core.Resource;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAPITesting.Amazon_Controller
{
    public class Amazon_Controller_Customer
    {
        private readonly ICustomerRepository customerRepository;
        public Amazon_Controller_Customer()
        {
            customerRepository = A.Fake<ICustomerRepository>();
        }
        [Fact]
        public async Task CustomerController_GetCustomerById_ReturnCustomer()
        {
            //Arrange
            var id = 1000;
            Customer Customer = new Customer()
            {
                CustomerId = id,
                CustomerName = "abcd",
                CustomerEmail = "abc@gmail.com",
                CustomerCity = "xxxxx",
                CustomerPassword = "12345678",
                ConfirmPassword = "12345678",

            };
            A.CallTo(() => customerRepository.GetCustomerById(id)).Returns(Customer);
            var controller = new CustomerController(customerRepository);

            //Act
            var result = await controller.GetCustomer(id);
            //Assert
            var name = "abcd";
            name.Should().BeSameAs(Customer.CustomerName);
        }
        [Fact]
        public async Task CustomerController_PostCustomer_ReturnCustomer()
        {
            //Arrange
            Customer Customer = new Customer()
            {
                CustomerId = 1000,
                CustomerName = "abcd",
                CustomerEmail = "abc@gmail.com",
                CustomerCity = "swswsw",
                CustomerPassword = "1234",
                ConfirmPassword = "1234",
            };
            A.CallTo(() => customerRepository.AddCustomer(Customer)).Returns(Customer);
            var controller = new CustomerController(customerRepository);

            //Act
            var result = await controller.PostCustomer(Customer);
            //Assert
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(Customer);
        }
        [Fact]
        public async Task CustomerController_PutCustomer_ReturnOk()
        {
            //Arrange
            var id = 1000;
            var Customer = new Customer()
            {
                CustomerId = id,
                CustomerName = "abcd",
                CustomerEmail = "abc@gmail.com",
                CustomerCity = "xsxsx",
                CustomerPassword = "12345",
                ConfirmPassword = "12345",
            };
            A.CallTo(() => customerRepository.UpdateCustomer(id, Customer)).Returns(Customer);
            var controller = new CustomerController(customerRepository);
            //Act
            var result = await controller.PutCustomer(id, Customer);
            //Assert
            var name = "abcd";
            name.Should().BeSameAs(Customer.CustomerName);
            result.Value.Should().BeEquivalentTo(Customer);
        }
    }
}
