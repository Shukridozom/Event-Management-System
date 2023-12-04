using AutoMapper;
using EventManagementSystem.Dtos;
using EventManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : AppControllerBase
    {
        public AccountsController(AppDbContext context, IConfiguration config, IMapper mapper)
            :base(context, config, mapper)
        {

        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAccount(int id)
        {
            var user = context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            return Ok(mapper.Map<User, UserDto>(user));
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto userDto)
        {

            var userWithSameCredentials = context.Users
                .SingleOrDefault(u => u.Username.ToLower() == userDto.Username.ToLower()
                || u.Email.ToLower() == userDto.Email.ToLower());
            if(userWithSameCredentials != null)
            {
                if (userWithSameCredentials.Username == userDto.Username)
                    return Conflict(GenerateJsonErrorResponse("username", "Username already exists"));
                else
                    return Conflict(GenerateJsonErrorResponse("email", "Email already exists"));
            }

            var user = mapper.Map<RegisterDto, User>(userDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            context.Users.Add(user);

            context.SaveChanges();

            return CreatedAtAction(nameof(GetAccount), new {Id = user.Id}, mapper.Map<User, UserDto>(user));
        }
    }
}
