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
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AmazonAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantRepository _repository;
        private readonly IConfiguration _configuration;
        
 
        public MerchantController(IMerchantRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
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
           Merchant merchant= await  _repository.MerchantLogin(m);
           MerchantToken mt=new MerchantToken ();

            if (merchant != null)
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, merchant.MerchantEmail),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };


                var token = GetToken(authClaims);
                string s = new JwtSecurityTokenHandler().WriteToken(token);
                mt.merchantToken = s;
                mt.merchant = merchant;
                return mt;

            }
             
            return Ok(mt);


        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                ); ;

            return token;
        }




    }
}
