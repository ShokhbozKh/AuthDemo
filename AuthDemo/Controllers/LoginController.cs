using AuthDemo.Data;
using AuthDemo.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public LoginController( AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.FirstName) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Login yoki parol to'ldirilmagan!");
            }
            if (!_appDbContext.Users.Any(u => u.FirstName == loginDto.FirstName))
            {
                return NotFound($"Foydalanuvchi topilmadi: {loginDto.FirstName}");
            }
            if(!_appDbContext.Users.Any(u => u.Password == loginDto.Password))
            {
                return Unauthorized("Parol noto'g'ri!");
            }
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.FirstName == loginDto.FirstName);
            if (user == null)
            {
                return NotFound($"Foydalanuvchi topilmadi: {loginDto.FirstName}");
            }
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return Unauthorized("Parol noto'g'ri!");
            }
            // Agar login muvaffaqiyatli bo'lsa, foydalanuvchini kutib olish

            return Ok($"Xush kelibsiz, {user.FirstName}!");
        }
    }
}
