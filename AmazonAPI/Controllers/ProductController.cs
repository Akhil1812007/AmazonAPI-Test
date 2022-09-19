using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmazonAPI.Models;


using AmazonAPI.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace AmazonAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {

            return await _repository.GetAllProduct();
        }



        [Authorize]
        [HttpPost("product")]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            return await _repository.AddProduct(product);
        }

        [HttpPut("{product}")]
        public async Task<ActionResult<Product>> PutProduct(Product product)
        {
            return await _repository.EditProduct(product);

        }
        [HttpPatch("{id}")]
        public async Task PatchProduct([FromBody] JsonPatchDocument product, [FromRoute] int id)
        {
             await _repository.PatchProduct(product, id);

        }






        [HttpDelete("{id}")]
        public async Task DeleteProduct(int id)
        {
            await _repository.DeleteProduct(id);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            return await _repository.GetProductById(id);
        }





    }
}
