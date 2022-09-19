 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmazonAPI.Models;
using AmazonAPI.Repository;

namespace AmazonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;

        public CartController(ICartRepository repository)
        {
            _repository = repository;
        }

        //getting all carts of a particular customer by using the customer id
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Cart>>>? Getcarts(int id) // id=customer id
        {
            List<Cart> result = await _repository.GetAllCart(id);

            return result;
        }
        //delete a cart
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCart(int id)
        {
            await _repository.DeleteFromCart(id);
            return NoContent();

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Cart>> updateCart(int id, Cart cart)
        {
            return await _repository.UpdateCart(id, cart);
        }
        [HttpPost]
        public async Task<ActionResult<Cart>> AddCart(Cart cart)
        {


            return await _repository.AddToCart(cart);
        }
       





    }
}
