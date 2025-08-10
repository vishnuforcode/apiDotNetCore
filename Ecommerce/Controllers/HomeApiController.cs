using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ecommerce.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HomeApiController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public HomeApiController(EcommerceContext context)
        {
            this._context = context;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var data = await _context.Users.ToListAsync();
            return Ok(data);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id != null)
            {
                var data = await _context.Users.FindAsync(id);
                return Ok(data);
            }
            else
            {
                return BadRequest("id not valid");
            }

        }


        [HttpPost("register/")]
        public async Task<IActionResult> Create(AddUserDto user)
        {
            if (user != null)
            {
                User data = new User();
                data.Id = user.Id;
                data.FullName = user.FullName;
                data.Email = user.Email;
                data.Password = user.Password;

                await _context.Users.AddAsync(data);
                await _context.SaveChangesAsync();
                return Ok(data);

            }
            else
            {
                return BadRequest("Invalid data");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            else
            {
                return BadRequest("user not found ");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Users.FindAsync(id);
            if (data != null)
            {
                _context.Users.Remove(data);
                await _context.SaveChangesAsync();
                return Ok("user deleted");
            }
            else
            {
                return BadRequest("Invalid id");
            }
        }
    }

     
}