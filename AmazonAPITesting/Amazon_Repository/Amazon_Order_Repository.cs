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
    public class Amazon_Order_Repository
    {
        [Fact]
        public async Task orderRepository_UpdateOrderMaster_ReturnOrderMaster()
        {
            //Arrange
            var Inmemory = new AmazonInMemoryDatabase();
            var dbContext = await Inmemory.GetDatabaseContext();
            var orderRepository = new OrderRepository(dbContext);
            var OrderMaster = new OrderMaster()
            {
                OrderMasterId = 5000,
                OrderDate = DateTime.Now,
                total = 1200,
                AmountPaid = 1200,
                CardNumber = 123456781,
                CustomerId=2000,

            };
            var OrderMaster1 = new OrderMaster()
            {
                OrderMasterId = 5000,
                OrderDate = DateTime.Now,
                total = 1200,
                AmountPaid = 100,
                CardNumber = 123456781,
                CustomerId = 2000,

            };

            var trackedOrderMaster = await dbContext.OrderMasters.FindAsync(5000);
            dbContext.Entry<OrderMaster>(trackedOrderMaster).State = EntityState.Detached;


            //Act

            var result1 = await orderRepository.UpdateOrderMaster(OrderMaster);

            //Assert --- for a customer whose is present in the database with valid cart ,Ordermaster paying the bill 
            result1.AmountPaid.Should().Be(1200);

            //Act
            var trackedOrderMaster1 = await dbContext.OrderMasters.FindAsync(5000);
            dbContext.Entry<OrderMaster>(trackedOrderMaster1).State = EntityState.Detached;
            var result2 = await orderRepository.UpdateOrderMaster(OrderMaster1);

            //Assert --- for a customer whose is present in the database without  valid cart ,Ordermaster paying the bill will return null
            result2.Should().Be(null);

        }
    }
}
