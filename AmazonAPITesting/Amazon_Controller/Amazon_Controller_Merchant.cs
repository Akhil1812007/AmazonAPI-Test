using AmazonAPI.Models;
using AmazonAPI.Repository;
using AmazonAPI.Controllers;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using FluentAssertions.Equivalency.Tracing;
using NuGet.Protocol.Core.Types;
using Microsoft.EntityFrameworkCore;

namespace AmazonAPITesting.Amazon_Controller
{
    public class Amazon_Controller_Merchant
    {
        private readonly IMerchantRepository _merchantRepository;
        public Amazon_Controller_Merchant()
        {
            _merchantRepository = A.Fake<IMerchantRepository>();
        }
        [Fact]
        public async Task MerchantController_GetMerchants_ListMerchantAsync()
        {
            //Arrange
            var MerchantList = new List<Merchant>();
            int temp = 1000;
            for(int i=0; i < 10; i++)
            {
                Merchant merchant = new Merchant
                {
                    MerchantId = ++temp,
                    MerchantEmail = "akhil"+i+"@gmail.com",
                    MerchantName = "Akhil"+i,
                    MerchantPassword = "12345",
                    ConfirmPassword = "12345",
                };
                MerchantList.Add(merchant); 
            } 
                

            A.CallTo(() => _merchantRepository.GetMerchant()).Returns(MerchantList);
            var MerchantController = new MerchantController(_merchantRepository);
            //var expected = A.Fake<Task<ActionResult<List<Merchant>>>>();

            //Act
            var result = await  MerchantController.GetMerchants();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<List<Merchant>>>();


        }
        [Fact]
        public async Task MerchantController_GetByMerchantId_Merchant()
        {
            //Arrange
            var merchantId = 1000;
            Merchant merchant = new Merchant()
            {
                MerchantId = merchantId,
                MerchantEmail = "akhil1@gmail.com",
                MerchantName = "Akhil",
                MerchantPassword = "12345",
                ConfirmPassword = "12345",
            };
            A.CallTo(() => _merchantRepository.GetMerchantByID(1000)).Returns(merchant);
            var MerchantController = new MerchantController(_merchantRepository);

            //Act
            var Tempresult =await  MerchantController.GetMerchant(merchantId);
            var result= (Tempresult.Result as OkObjectResult).Value as Merchant;
            
            //Assert
            result.Should().NotBeNull();
            
            
            

            



        }
        [Fact]
        public async Task PutMerchant_ReturnMerchant()
        {
            //Arrange
            var Id = 1001;
            Merchant merchant = new Merchant
            {
                MerchantId = Id,
                MerchantEmail = "akhil1@gmail.com",
                MerchantName = "Akhil1",
                MerchantPassword = "12345",
                ConfirmPassword = "12345",
            };
            A.CallTo(() => _merchantRepository.UpdateMerchant(1001,merchant)).Returns(merchant);
            var MerchantController = new MerchantController(_merchantRepository);
            //Act
            var TempResult= await MerchantController.PutMerchant(Id, merchant);
            var result = TempResult.Value;
            //Assert
            var name = "Akhil1";
            result.Should().BeOfType<Merchant>();
            name.Should().BeEquivalentTo(result.MerchantName);

            "Akhil1".Should().Be(result.MerchantName);

        }
        [Fact]
        public async Task  DeleteMerchant_ReturnVoid()
        {
            //Arrange
            var merchantId = 1000;
           
            A.CallTo(() => _merchantRepository.DeleteMerchant(merchantId)).Returns(true);
            var Merchant = new MerchantController(_merchantRepository);

            //Act
            var result = await Merchant.DeleteMerchant(merchantId);
            //Assert
            result.Should().BeOfType(typeof(OkResult));
        }
        [Fact]
        public async Task MerchantController_PostMerchant_Merchant()
        {
            //Arrange
            Merchant merchant = new Merchant()
            {
                MerchantId = 1001,
                MerchantEmail = "akhil@gmail.com",
                MerchantName = "akhil",
                MerchantPassword = "12345",
                ConfirmPassword = "12345",



            };
            //var traderspost = A.Fake<Traders>();
           
            A.CallTo(() => _merchantRepository.InsertMerchant(merchant)).Returns(merchant);
            var controller = new MerchantController(_merchantRepository);



            //Act
            var result = await controller.PostMerchant(merchant);



            //Assert
            "akhil".Should().Be(result.Value.MerchantName);
            "akhil@gmail.com".Should().Be(result.Value.MerchantEmail);
            
        }
        [Fact]
        public async Task MerchantController_GetProductByMerchantId_Product()
        {
            //Arrange           
            var productId = 200;

            List<Product> p = new List<Product>();
            for (int i = 0; i < 3; i++)
            {
                Product Product = new Product()
                {
                    MerchantId = 1000,
                    ProductId = productId++,
                    ProductName = "Boost",
                    ProductQnt = 1,
                    UnitPrice = 12,
                    CategoryId = 1,
                };
                p.Add(Product);
            }
            A.CallTo(() => _merchantRepository.GetProductByMerchantId(1000)).Returns(p);
            var controller = new MerchantController(_merchantRepository);

            //Act
            var result = await controller.GetProductByMerchantId(1000);
            //Assert
            var id = 1000;
            var count = result.Value.Count();
            id.Should().Be(result.Value[0].MerchantId);
            count.Should().Be(3);
            result.Should().NotBeNull();

        }
        [Fact]
        public async Task MerchantController_GetProductByTraderId_ReturnListMerchant()
        {
            //Arrange           
            var merchantId = 1000;
            var productId =1000;
            List<Product> p = new List<Product>();
            for (int i = 0; i < 10; i++)
            {
                Product Product = new Product()
                {
                    MerchantId = merchantId,
                    ProductId = productId++,
                    ProductName = "Boost",
                    ProductQnt = 1,
                    UnitPrice = 12,
                    CategoryId = 1,
                };
                p.Add(Product);
            }
            A.CallTo(() => _merchantRepository.GetProductByMerchantId(merchantId)).Returns(p);
            var controller = new MerchantController(_merchantRepository);

            //Act
            var result = await controller.GetProductByMerchantId(merchantId);
            //Assert

            1000.Should().Be(result.Value[0].MerchantId);
            10.Should().Be(result.Value.Count);

            result.Should().NotBeNull();


        }

    }
}






    