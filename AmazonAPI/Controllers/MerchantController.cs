using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmazonAPI.Models;
using AmazonAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using AmazonAPI.Models;

namespace AmazonAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantRepository _repository;
       

        //checking for correct azure repos
        public MerchantController(IMerchantRepository repository)
        {
            _repository = repository;

        }

        [HttpGet]
        public async Task<ActionResult<List<Merchant>>> GetMerchants()
        {
            return await _repository.GetMerchant();
        }
        
        [HttpGet("MerchantId")]
        public async Task<ActionResult<List<Product>>> GetProductByMerchantId(int MerchantId)
        {
            return await _repository.GetProductByMerchantId(MerchantId);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Merchant>> GetMerchant(int id)
        {
            try
            {
                var merchant= await _repository.GetMerchantByID(id);
                return Ok(merchant);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Merchant>> PutMerchant(int id, Merchant merchant)
        {
            try
            {
                return await _repository.UpdateMerchant(id, merchant);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Merchant>> PostMerchant(Merchant merchant)
        {
            return await _repository.InsertMerchant(merchant);
             
          
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMerchant(int id)
        {
            bool merchant = await _repository.DeleteMerchant(id);
            if (merchant)
            {
                
                return  Ok();
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPost("MerchantLogin")]
        public async Task<ActionResult<MerchantToken>> MerchantLogin(Merchant m)
        {
           MerchantToken? ml= await _repository.MerchantLogin(m);

            if (ml==null)
            {
                return Unauthorized();
            }

            return Ok(ml);


        }
        



    }
}
