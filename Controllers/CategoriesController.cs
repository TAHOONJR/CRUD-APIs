using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using WebApi.Data;
using WebApi.Data.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController(AppDbContext db)
        {
            _db = db;
        }

        private readonly AppDbContext _db;

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var cats = await _db.Categories.ToListAsync();
            return Ok(cats);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category cat)
        {
            if (ModelState.IsValid)
            {
                await _db.Categories.AddAsync(cat);
                await _db.SaveChangesAsync();
                return Ok(cat);
            }
            return BadRequest();
        }
    }
}