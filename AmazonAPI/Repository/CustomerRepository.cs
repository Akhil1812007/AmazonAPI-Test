using Amazon.Models;

namespace Amazon.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AmazonContext _context;

        public CustomerRepository(AmazonContext context)
        {
            _context = context;
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;

        }
        public  async Task<Customer> CustomerLogin(Customer c)
        {
            var customer=( from i in _context.Customers where i.CustomerEmail == c.CustomerEmail  && i.CustomerPassword== c.CustomerPassword select i).FirstOrDefault();
            return customer;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async  Task<Customer> UpdateCustomer(int id, Customer customer)
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
            return  customer;
        }

        public async Task<Feedback> AddFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }
    }
}
