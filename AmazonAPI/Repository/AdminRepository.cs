using AmazonAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AmazonAPI.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AmazonContext _context;
        public AdminRepository(AmazonContext context)
        {
            _context = context;
        }

        public async Task<Admin> AddAdmin(Admin admin)
        {
            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<Category> AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async  Task DeleteAdmin(int AdminId)
        {
            try
            {
                Admin admin = _context.Admin.Find(AdminId);
                _context.Admin.Remove(admin);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public async Task DeleteCategory(int CategoryId)
        {
            try
            {
                Category? category = _context.Categories.Find(CategoryId);
                _context.Categories .Remove(category);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public async Task<Admin> EditAdmin(int AdminId,Admin admin)
        {

            _context.Update(admin);
            _context.SaveChanges();
            return admin;

        }
        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch
            {
                throw new NotImplementedException();

            }
        }
    }
}
