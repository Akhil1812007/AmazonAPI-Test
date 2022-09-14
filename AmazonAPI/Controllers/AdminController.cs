using Amazon.Models;
using Amazon.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _repository;
        public AdminController(IAdminRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("{admin}")]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
            return await _repository.AddAdmin(admin);
        }
        [HttpPost("{category}")]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            return await _repository.AddCategory(category);
        }
        [HttpPost("{CategoryId}")]
        public async Task DeleteCategory(int  CategoryId)
        {
             await _repository.DeleteCategory(CategoryId);
        }
        [HttpGet("GetAllCategory")]
        public async Task<ActionResult<List<Category>>> GetAllCategory()
        {
            return await _repository.GetAllCategories();
        }


    }
}
