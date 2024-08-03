using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppApi.DTOs;
using WebAppApi.Entities;
using WebAppApi.Interfaces;
using WebAppApi.Models;

namespace WebAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(UserManager<User> userManager, RoleManager<Role> roleManager,
                              ITokenService tokenService) : ControllerBase
    {
        private User _user = new();
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<Role> _roleManager = roleManager;
        //private readonly IMapper _mapper;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto request)
        {
            try
            {
                _user = await _userManager.Users
                    .SingleOrDefaultAsync(x => x.UserName.Equals(request.UserName));

                if (_user is null)
                    return Unauthorized("Username non valido");

                bool result = await _userManager.CheckPasswordAsync(_user, request.Password);

                if (!result)
                    return Unauthorized("Password non valida");

                return Ok(new UserDto
                {
                    Id = _user.Id,
                    UserName = _user.UserName,
                    Token = await _tokenService.CreateToken(_user),
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                // Controlla se l'utente esiste già
                bool userExists = await UserExist(model.Username);
                if (userExists)
                    return StatusCode(500, new { Message = "Utente già esistente" });

                _user = new()
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(_user, model.Password);

                if (result.Succeeded)
                {
                    // Assegna il ruolo "User" all'utente appena creato
                    if (!await _roleManager.RoleExistsAsync("User"))
                    {
                        await _roleManager.CreateAsync(new Role { Name = "User" });
                    }

                    await _userManager.AddToRoleAsync(_user, "User");

                    return Ok(new UserDto
                    {
                        Id = _user.Id,
                        UserName = _user.UserName,
                        Token = await _tokenService.CreateToken(_user),
                    });
                }
                else 
                    return StatusCode(500, new { Message = "Result not succeeded" });

            } catch (Exception ex)
            {
                return BadRequest(new { Message = $"{ex.Message}" });
            }
            
        }

        private async Task<bool> UserExist(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName.Equals(username));
        }
    }
}
