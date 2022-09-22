using AmazonAPI.Controllers;
using AmazonAPI.Models;
using AmazonAPI.Repository;
using FakeItEasy;
using FluentAssertions;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAPITesting.Amazon_Controller
{
    public class Amazon_Controller_Cart
    {
        private readonly ICartRepository cartRepository;
        public Amazon_Controller_Cart()
        {
            cartRepository = A.Fake<ICartRepository>();
        }
        [Fact]

        public async Task CartController_AddCart_ReturnCart()
        {
            //Arrange
            var id = 100;
            var cart = new Cart()
            {
                CartId = id,
                ProductQuantity = 12,
            };
            A.CallTo(() => cartRepository.AddToCart(cart)).Returns(cart);
            var controller = new CartController(cartRepository);
            //Act
            var result = await controller.AddCart(cart);
            //Assert
            result.Should()
    }
}
