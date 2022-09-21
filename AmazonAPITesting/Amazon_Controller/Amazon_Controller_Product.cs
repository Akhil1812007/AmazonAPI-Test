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
    public class Amazon_Controller_Product
    {
        private readonly IProductRepository _productRepository;
        public Amazon_Controller_Product()
        {
            _productRepository = A.Fake<IProductRepository>();
        }
        [Fact]
        public async Task ProductsController_GetProducts_IEnumerableProducts()
        {
            //Arrange
            var productslist = A.Fake<List<Product>>();
            A.CallTo(() => _productRepository.GetAllProduct()).Returns(productslist);
            var controller = new ProductController(_productRepository);

            //Act
            var result = await controller.GetProducts();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<IEnumerable<Product>>>();

        }
        [Fact]
        public async Task ProductsController_GetProductById_ListProduct()
        {
            //Arrange
            var id = 1000;


            var product = new Product()
            {
                ProductId = id,
                ProductName = "Boost",
                ProductQnt = 12,
                UnitPrice = 120,
            };

            A.CallTo(() => _productRepository.GetProductById(1000)).Returns(product);
            var controller = new ProductController(_productRepository);
            //Act
            var result = await controller.GetProductById(product.ProductId);
            //Assert
            var name = "Boost";
            name.Should().BeSameAs(product.ProductName);
        }
        [Fact]
        public async Task ProductsController_PostProducts_ReturnProduct()
        {
            //Arrange
            var id = 1000;
            var product = new Product()
            {
                ProductId = id,
                ProductName = "Complan",
                ProductQnt = 12,
                UnitPrice = 124,
            };
            A.CallTo(() => _productRepository.AddProduct(product)).Returns(product);
            var controller = new ProductController(_productRepository);
            //Act
            var result = await controller.PostProduct(product);
            //Assert
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(product);

        }
        [Fact]
        public async Task ProductsController_PutProducts_ReturnProduct()
        {
            //Arrange
            var id = 1000;
            var product = new Product()
            {
                ProductId = id,
                ProductName = "Bournvita",
                ProductQnt = 12,
                UnitPrice = 230,
            };
            A.CallTo(() => _productRepository.EditProduct(product)).Returns(product);
            var controller = new ProductController(_productRepository);
            //Act
            var result = await controller.PutProduct(product);
            //Assert
            var qty = 12;
            qty.Should().BeGreaterThan(10);
            result.Value.Should().BeEquivalentTo(product);
        }

    }
}
