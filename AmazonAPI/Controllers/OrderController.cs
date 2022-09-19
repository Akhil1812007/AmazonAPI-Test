using AmazonAPI.Models;
using AmazonAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AmazonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public OrderController(IOrderRepository repository,ICartRepository cartRepository)
        {
            _orderRepository = repository;
            _cartRepository = cartRepository;
            
        }
        

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderMaster>> GetOrderMaster(int id)
        {
            return await _orderRepository.GetOrderMasterById(id);
        }
        [HttpPut("OrderMaster")]
        public async Task<ActionResult<OrderMaster>> PutOrMaster(OrderMaster om)
        {
            return await _orderRepository.UpdateOrderMaster(om);

        }
        [HttpPost("orderMaster")]
        public async Task<ActionResult<OrderMaster>> PostOrMaster(OrderMaster om)
        {
            
            return await _orderRepository.AddOrderMaster(om);


        }
        [HttpPost("orderDetail")]
        public async Task<ActionResult<OrderDetail>> PostOrDetail(OrderDetail od)
        {
            return await _orderRepository.AddOrderDetail(od);


        }
        [HttpGet("order")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            return await _orderRepository.GetOrderDetailById(id);
        }



        [HttpPost("{customerId}")]
       
        public async Task<OrderMaster> Buy(int customerId)
        {

            List<Cart> c =  await _cartRepository.GetAllCart(customerId);
            OrderMaster om = new OrderMaster();
            om.OrderDate = DateTime.Today;
            om.CustomerId = customerId;
            om.total = 0;
            if (c != null)
            {
                foreach (var cart in c)
                {
                    om.total +=  (cart.ProductQuantity * cart.Product.UnitPrice);
                }
            }
            await _orderRepository.AddOrderMaster(om);
            foreach (var item in c)
            {
                OrderDetail detail = new OrderDetail();
                detail.ProductId = item.ProductId;
                detail.ProductQuantity = item.ProductQuantity;
                detail.ProductRate = item.Product.UnitPrice;
                detail.OrderMasterId = om.OrderMasterId;
                await _orderRepository.AddOrderDetail(detail);
            }
            return om;
        }

    }
}
