using FlashCardBuddy_API.Models;
using FlashCardBuddy_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlashCardBuddy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private FlashCardBuddyDbContext dbContext = new FlashCardBuddyDbContext();

        private PasswordService passwordService = new PasswordService();

        // DTO Conversions
        static UserDTO convertUserDTO(User u)
        {
            return new UserDTO
            {
                Userid = u.Userid,
                Username = u.Username,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                Email = u.Email
            };
        }

        [HttpGet]
        public async Task<IActionResult> getAllUsers()
        {
            List<UserDTO> result = new List<UserDTO>();

            result = await dbContext.Users.Select(u => convertUserDTO(u)).ToListAsync();

            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> getUser(int userId)
        {
            User result = await dbContext.Users.Where(u => u.Active == true).FirstOrDefaultAsync(u => u.Userid == userId);

            if (result == null || result.Active == false)
            {
                return NotFound("User not found");
            }

            return Ok(convertUserDTO(result));

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginmodel)
        {
            User result = await dbContext.Users.Where(u => u.Active == true).FirstOrDefaultAsync(u => u.Username == loginmodel.username);

            if(result == null || result.Active == false)
            {
                return NotFound();
            }

            bool isPasswordValid = passwordService.verifyPassword(loginmodel.password, result.Password);

            if (!isPasswordValid)
            {
                return Unauthorized("Invalid");
            }
            return Ok(convertUserDTO(result));

        }

        [HttpPost]
        public async Task<IActionResult> PostUser(PostUserDTO user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await dbContext.Users.AnyAsync(a => a.Username == user.Username))
            {
                return BadRequest(user.Username + " is already in use");
            }

            User newUser = new User();

            newUser.Username = user.Username;
            newUser.Firstname = user.Firstname;
            newUser.Lastname = user.Lastname;
            newUser.Email = user.Email;
            newUser.Password = passwordService.HashPassword(user.Password);

            dbContext.Users.Add(newUser);
            await dbContext.SaveChangesAsync();

            return Ok(newUser);

        } 

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            User result = await dbContext.Users.FindAsync(userId);

            if(result == null || result.Active == false)
            {
                return NotFound("User not found");
            }

            result.Active = false;

            dbContext.Users.Update(result);
            await dbContext.SaveChangesAsync();

            return NoContent();

        }
    }

}
