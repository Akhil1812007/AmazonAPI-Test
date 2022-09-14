using Amazon.Models;
using Amazon.Repository;
using Amazon.Controllers;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

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
        public void MerchantController_GetMerchants_ListMerchantAsync()
        {
            //Arrange
            var MerchantList = A.Fake<Task<List<Merchant>>>();
            A.CallTo(() => _merchantRepository.GetMerchant()).Returns(MerchantList);
            var MerchantController = new MerchantController(_merchantRepository);
            var expected = A.Fake<Task<ActionResult<List<Merchant>>>>();

            //Act
            var result = MerchantController.GetMerchants();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<ActionResult<List<Merchant>>>>();


        }

    }
}
