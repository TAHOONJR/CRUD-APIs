using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> AddCategory(string Name, string Description)
        {
            Category c = new() { Name = Name, Description = Description };
            await _db.Categories.AddAsync(c);
            _db.SaveChanges();
            return Ok(c);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(int Id, string Name, string Description)
        {
            var c = await _db.Categories.FindAsync(Id);
            if (c == null)
            {
                return NotFound($"category Id \"{Id}\" Not exist");
            }
            c.Name = Name;
            c.Description = Description;
            _db.SaveChanges();
            return Ok(c);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            var c = await _db.Categories.FindAsync(Id);
            if (c == null)
            {
                return NotFound($"category Id \"{Id}\" Not exist");
            }
            _db.Categories.Remove(c);
            _db.SaveChanges();
            return Ok($"Category \"{Id}\" Deleted Successfully ");
        }

        [HttpPatch]
        public async Task<IActionResult> PatchCategory(int Id, string Name, string Description)
        {
            var c = await _db.Categories.FindAsync(Id);
            if (c == null)
            {
                return NotFound($"category Id \"{Id}\" Not exist");
            }
            if (Name != null)
            {
                c.Name = Name;
            }
            if (Description != null)
            {
                c.Description = Description;
            }
            _db.SaveChanges();
            return Ok(c);
        }
    }
}