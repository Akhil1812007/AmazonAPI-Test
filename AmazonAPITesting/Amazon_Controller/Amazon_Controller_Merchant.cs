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
        public async Task MerchantController_GetMGetProductByMerchantId_Merchant()
        {
            //Arrange
            var merchantId = 1000;
            Merchant merchant = new Merchant
            {
                MerchantId = merchantId,
                MerchantEmail = "akhil@gmail.com",
                MerchantName = "akhil",
                MerchantPassword = "12345",
                ConfirmPassword = "12345",
            };
            A.CallTo(() => _merchantRepository.GetMerchantByID(1000)).Returns(merchant);
            var MerchantController = new MerchantController(_merchantRepository);

            //Act
            ActionResult<Merchant> TempResult =await  MerchantController.GetMerchant(merchantId);
            var result = TempResult.Value;
            //Assert

            result.Should().BeOfType<Merchant>();



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
            result.Should().BeSameAs(name.Equals(result.MerchantName));
            

        }





    }
}
