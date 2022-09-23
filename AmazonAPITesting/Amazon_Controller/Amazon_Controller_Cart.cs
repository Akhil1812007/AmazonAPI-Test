using AmazonAPI.Controllers;
using AmazonAPI.Models;
using AmazonAPI.Repository;
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
                CustomerId = 1000,
                ProductId = id,
            };
            A.CallTo(() => cartRepository.AddToCart(cart)).Returns(new IsExistReturn(false,cart));
            var controller = new CartController(cartRepository);
            //Act
            var result = await controller.AddCart(cart);
            //Assert
            result.Value.Should().BeEquivalentTo(cart);
        }
        [Fact]
        public async Task CartController_CartById_ReturnCartById()
        {
            //Arrange
            var id = 100;
            var cart = new Cart()
            {
                CartId = id,
                ProductQuantity = 15,
                CustomerId = 1000,

            };
            A.CallTo(() => cartRepository.UpdateCart(id,cart)).Returns(cart);
            var controller = new CartController(cartRepository);
            //Act
            var result = await controller.updateCart(id,cart);
            //Assert
            15.Should().Be(cart.ProductQuantity);
        }
        [Fact]
        public async Task CartController_DeleteCart_ReturnOk()
        {
            //Arrange
            A.CallTo(() => cartRepository.DeleteFromCart(1000)).Returns(true);
            var controller = new CartController(cartRepository);
            //Act
            var result = await controller.DeleteCart(1000);
            //Assert
            result.Should().BeOfType(typeof(OkResult));

        }
        [Fact]
        public async Task CartController_GetCartByCustId_ReturnListCart()
        {
            //Arrange
            var cartsList = A.Fake<List<Cart>>();
            A.CallTo(() => cartRepository.GetAllCart(1000)).Returns(cartsList);
            var controller = new CartController(cartRepository);
            //Act
            var result = await controller.Getcarts(1000);
            //Assert
            result.Should().BeOfType<ActionResult<List<Cart>>>();


        }
    }
}
