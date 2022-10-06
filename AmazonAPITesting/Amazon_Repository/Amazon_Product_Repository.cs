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
    public class Amazon_Product_Repository
    {
        [Fact]
        public async Task ProductRepository_GetAllProducts_ReturnGetProduct()
        {
            //Arrange
            var Inmemory = new AmazonInMemoryDatabase();
            var dbContext = await Inmemory.GetDatabaseContext();
            
            var productRepository = new ProductRepository(dbContext);

            //Act
            var result = await productRepository.GetAllProduct();
            //Assert
            var count = result.Count();
            result.Should().HaveCount(5);
            result.Should().NotBeNull();
        }
        [Fact]
        public async Task ProductRepository_AddProduct_ReturnPostProduct()
        {
            //Arrange
            var product = new Product()
            {
                ProductName = "Complan",
                ProductQnt = 10,
                UnitPrice = 180,
            };
            var Inmemory = new AmazonInMemoryDatabase();
            var dbContext = await Inmemory.GetDatabaseContext();
            var productRepository = new ProductRepository(dbContext);
            //Act
            var result = await productRepository.AddProduct(product);
            //Assert
            dbContext.Products.Should().HaveCount(6);
            result.Should().BeEquivalentTo(product);


        }
        [Fact]
        public async Task ProductRepository_EditProduct_ReturnEdit()
        {
            var id = 3;
            var product = new Product()
            {
                ProductId = id,
                ProductName = "Viva",
                UnitPrice = 156,
                ProductQnt = 10,
            };
            var Inmemory = new AmazonInMemoryDatabase();
            var dbContext = await Inmemory.GetDatabaseContext();
            var productRepository = new ProductRepository(dbContext);
            //Act
            var productfind = await dbContext.Products.FindAsync(id);
            dbContext.Entry<Product>(productfind).State = EntityState.Detached;
            var result = await productRepository.EditProduct(product);
            //Assert
            result.Should().BeEquivalentTo(product);
            dbContext.Products.Should().HaveCount(5);
        }
    }
}
