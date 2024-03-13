using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Data.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        public ItemsController(AppDbContext db)
        {
            _db = db;
        }

        private readonly AppDbContext _db;

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _db.Items.ToListAsync();
            return Ok(items);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOneItem(int Id)
        {
            var item = await _db.Items.FindAsync(Id);
            if (item == null)
            {
                return NotFound($"Item \"{Id}\" Not Found");
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddItems(mdlItem mdl)
        {
            using var stream = new MemoryStream();
            await mdl.Image.CopyToAsync(stream);

            var item = new Items
            {
                categoryId = mdl.CategoryId,
                Name = mdl.Name,
                Price = mdl.Price,
                Notes= mdl.Notes,
                Image = stream.ToArray()
            };
            await _db.Items.AddAsync(item);
            await _db.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateItem(int Id, mdlItem mdl)
        {
            var item = await _db.Items.FindAsync(Id);
            if (item == null)
            {
                return NotFound($"Item \"{Id}\" Not Found");
            }
            using var stream = new MemoryStream();
            await mdl.Image.CopyToAsync(stream);
            item.categoryId = mdl.CategoryId;
            item.Name = mdl.Name;
            item.Price = mdl.Price;
            item.Notes = mdl.Notes;
            item.Image = stream.ToArray();
            await _db.SaveChangesAsync();
            return Ok(item);
        }
        

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteItem(int Id)
        {
            var item = await _db.Items.FindAsync(Id);
            if (item == null)
            {
                return NotFound($"Item \"{Id}\" Not Found");
            }
            _db.Items.Remove(item);
            await _db.SaveChangesAsync();
            return Ok($"Item \"{Id}\" Deleted");
        }
        
    }
}