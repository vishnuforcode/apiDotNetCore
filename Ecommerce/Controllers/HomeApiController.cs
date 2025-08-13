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
        public async Task<IActionResult> Get(int id)
        {
            var data = await _context.Users.FindAsync(id);
            if (data != null)
            {
                
                return Ok(data);
            }
            else
            {
                return BadRequest("no data");   
            }

        }


        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] AddUserDto user)
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto user)
        {
            if (user != null)
            {
                var data = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
                if (data != null)
                {
                    return Ok("login successfully");
                }
                else
                {
                    return BadRequest("user nnot found");
                }
            }
            else
            {
                return BadRequest("invalid credentials");
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] User user)
        {
            var userExist = await _context.Users.FindAsync(id);
            if (ModelState.IsValid && userExist!= null)
            {
                userExist.FullName = user.FullName;
                userExist.Email = user.Email;
                userExist.Password = user.Password;
                // _context.Users.Update(userExist);
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

            if (data == null)
            {
                return BadRequest();

            }
             _context.Users.Remove(data);
                await _context.SaveChangesAsync();
                return Ok();
            
        
        }
    }

     
}