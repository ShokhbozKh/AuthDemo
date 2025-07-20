using AuthDemo.Data;
using AuthDemo.DTOs;
using AuthDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;

namespace AuthDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegisterController : ControllerBase
    {
        private AppDbContext _dbContext;
        public RegisterController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        [HttpPost]
        public async Task<IActionResult> Registers([FromBody] RegisterDto registerDto)
        {
            if(_dbContext.Users.Any(u=>u.FirstName==registerDto.FirstName))
            {
                return BadRequest($"Foydalunuvchi bor, { registerDto.FirstName}");
            }
            var passwordHashcode = HashCode.Combine(registerDto.Password);

            var newUser = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                Password = passwordHashcode.ToString(),
            };
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();



            return Ok("Ruyxatdan muvofaqiyatli O'tdingiz!!");
        }
           

    }
}
