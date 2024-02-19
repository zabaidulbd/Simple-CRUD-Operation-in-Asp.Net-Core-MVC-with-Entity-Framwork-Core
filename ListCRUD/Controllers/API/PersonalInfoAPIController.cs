using ListCRUD.Data;
using ListCRUD.Models.PersonalInfoViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace UserMS.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalInfoAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonalInfoAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.PersonalInfo.ToListAsync();
            return Ok(items);
        }

        [HttpGet]
        [Route("GetBy/{id}")]
        public async Task<IActionResult> GetBy(Int64 id)
        {
            var item = await _context.PersonalInfo.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(PersonalInfoCRUDViewModel data)
        {
            if (ModelState.IsValid)
            {
                await _context.PersonalInfo.AddAsync(data);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetBy", new { data.Id }, data);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, PersonalInfoCRUDViewModel item)
        {
            if (id != item.Id)
                return BadRequest();

            var existItem = await _context.PersonalInfo.FirstOrDefaultAsync(x => x.Id == id);

            if (existItem == null)
                return NotFound();

            existItem = item;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existItem = await _context.PersonalInfo.FirstOrDefaultAsync(x => x.Id == id);

            if (existItem == null)
                return NotFound();

            _context.PersonalInfo.Remove(existItem);
            await _context.SaveChangesAsync();

            return Ok(existItem);
        }
    }
}



/*
Note:
https://localhost:5001/api/PersonalInfoAPI/GetBy/1
https://localhost:5001/api/PersonalInfoAPI/GetAll
https://localhost:5001/api/PersonalInfoAPI/Delete/2
*/