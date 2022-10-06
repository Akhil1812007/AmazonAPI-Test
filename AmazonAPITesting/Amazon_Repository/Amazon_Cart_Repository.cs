using AmazonAPI.Models;
using AmazonAPI.Repository;
using AmazonAPITesting.AmazonDBContext;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
        public async Task CartRepository_GetAllCartByCustomerId_ReturnGetCart()
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
        public async Task CartRepository_GetCartById_ReturnCart()
        {
            //Arrange
            var InmemoryDataBase = new AmazonInMemoryDatabase();
            var dbContext = await InmemoryDataBase.GetDatabaseContext();
            var cartRepository = new CartRepository(dbContext);
            //Act
            var result = await cartRepository.GetCartById(3002);
            //Assert
            12.Should().Be(result.ProductQuantity);
        }
        [Fact] //adding a cart which already not present in the cart of the specified customer
        public async Task CartRepository_AddCartIfNotExist_ReturnAddCart()
        {
            //Arrange
            var cart = new Cart()
            {
                CartId = 3005,
                ProductQuantity = 12,
                ProductId = 4001,
                CustomerId = 2001,
            };
            var InmemoryDataBase = new AmazonInMemoryDatabase();
            var dbContext = await InmemoryDataBase.GetDatabaseContext();
            var cartRepository = new CartRepository(dbContext);
            //Act
            var result = await cartRepository.AddToCart(cart);
            //Assert
            //result.Should().BeEquivalentTo(cart);
            6.Should().Be(dbContext.carts?.Count());
            
            result.cart.CartId.Should().Be(3005);

         
        }
        [Fact] //adding a cart which already not present in the cart of the specified customer
        public async Task CartRepository_UpdateCartIfCartExist_ReturnUpdatedCart()
        {
            //Arrange
            var cart = new Cart()
            {
                ProductQuantity = 12,
                ProductId = 4000,
                CustomerId = 2001,
            };
            var InmemoryDataBase = new AmazonInMemoryDatabase();
            var dbContext = await InmemoryDataBase.GetDatabaseContext();
            var cartRepository = new CartRepository(dbContext);
            //Act
            var result = await cartRepository.AddToCart(cart);
            //Assert
            result.Should().Be(typeof(IsExistReturn));
            10.Should().Be(dbContext.carts?.Count());
            result.cart.CartId.Should().Be(3000);
            result.cart.ProductQuantity.Should().Be(24);


        }
        [Fact]
        public async Task cartRepository_EditCart_ReturnEdit()
        {
            //Arrange
            var id = 3002;
            var cart = new Cart()
            {
                CartId = id,
                ProductQuantity = 15,
            };
            var InmemoryDataBase = new AmazonInMemoryDatabase();
            var dbContext = await InmemoryDataBase.GetDatabaseContext();
            var cartRepository = new CartRepository(dbContext);
            //Act
            var cartfind = await dbContext.carts.FindAsync(id);
            dbContext.Entry<Cart>(cartfind).State = EntityState.Detached;//has to be used only on xUnittesting

            var result = await cartRepository.UpdateCart(id, cart);
            //Assert
            result.Should().BeEquivalentTo(cart);
            10.Should().Be(dbContext.carts.Count());
        }

    }
}
