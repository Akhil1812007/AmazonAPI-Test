using AmazonAPI.Models;
using AmazonAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repository;

        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            return await _repository.GetCustomerById(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> PutCustomer(int id, Customer customer)
        {
            return await _repository.UpdateCustomer(id, customer);

        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            return await _repository.AddCustomer(customer);

        }

        [HttpPost("Feedback")]
        public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
        {
            return await _repository.AddFeedback(feedback);
        }
        [HttpPost("login")]
        public async Task<ActionResult<Customer>> CustomerLogin( Customer customer)
        {
            var logincustomer = await _repository.CustomerLogin(customer);
            if(logincustomer == null)
            {
                return BadRequest("Wrong");
            }
            return logincustomer;

        }



    }
}
