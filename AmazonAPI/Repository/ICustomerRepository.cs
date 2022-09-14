using Amazon.Models;

namespace Amazon.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerById(int id);
        Task<Customer> AddCustomer(Customer customer);
     
        Task<Customer> UpdateCustomer(int id, Customer customer);
      
        Task<Feedback> AddFeedback(Feedback feedback);
        Task<Customer> CustomerLogin(Customer customer);


    }
}
