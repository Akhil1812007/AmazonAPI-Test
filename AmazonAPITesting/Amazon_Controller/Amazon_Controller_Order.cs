using AmazonAPI.Controllers;
using AmazonAPI.Models;
using AmazonAPI.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAPITesting.Amazon_Controller
{
    public class Amazon_Controller_Order
    {
        private readonly IOrderRepository _orderrepository;
        private readonly ICartRepository _cartrepository;
        public Amazon_Controller_Order()
        {
            _orderrepository = A.Fake<IOrderRepository>();
            _cartrepository = A.Fake<ICartRepository>();
        }
        [Fact]
        public async Task OrderController_GetOrderMasterById_ReturnOmId()
        {
            //Arrange
            var id = 5001;
            var ordermaster = new OrderMaster()
            {

                OrderMasterId = id,
                CardNumber = 123456780,
                total = 1000,
                AmountPaid = 1000,
                CustomerId = 100,
                OrderDate = DateTime.Now,
            };
            A.CallTo(() => _orderrepository.GetOrderMasterById(id)).Returns(ordermaster);
            var OrderMasterController = new OrderController(_orderrepository, _cartrepository);
            //Act
            var tempResult = await OrderMasterController.GetOrderMaster(id);
            var result = tempResult.Value;
            //Assert
            var card = 123456780;
            card.Should().Be(ordermaster.CardNumber);
            result.Should().As<OrderMaster>();
        }
        [Fact]
        public async Task OrderController_Buy_OrderMaster()
        {
            //Arrange
            var ordermaster = new OrderMaster()
            {
                OrderMasterId=5001,
                OrderDate = DateTime.Now,
                CustomerId = 2001,
                total= 100,
            };
            var cart = new List<Cart>();

            cart.Add(new Cart()
            {
                CartId = 3000,
                CustomerId = 2001,
                ProductId = 40001,
                ProductQuantity = 1,
                Product=new Product()
                {
                    ProductId=4000,
                    UnitPrice=100,
                }


            }) ;
            List<OrderDetail> orderDetail=new List<OrderDetail>();
            orderDetail.Add(new OrderDetail()
            {
                OrderDetailId=6001,
                OrderMasterId=2001,
                ProductId=2001,
                ProductQuantity=1,

            });
            A.CallTo(() => _cartrepository.GetAllCart(2001)).Returns(cart);
            A.CallTo(() => _orderrepository.AddOrderMaster(ordermaster)).Returns(ordermaster);
            A.CallTo(() => _orderrepository.AddOrderDetail(orderDetail[0])).Returns(orderDetail[0]);
            var OrderMasterController = new OrderController(_orderrepository, _cartrepository);




            //Act
            var tempResult = await OrderMasterController.Buy(2001);
            //
            tempResult.Should().NotBeNull();
            tempResult.total.Should().Be(100);
            tempResult.Should();
            
            
        }
    }
}
