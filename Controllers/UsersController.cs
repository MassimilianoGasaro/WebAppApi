using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppApi.Entities;
using WebAppApi.Interfaces;

namespace WebAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(UserManager<User> userManager, ITokenService tokenService)
        : ControllerBase
    {
        private User _user = new();
        private readonly UserManager<User> _userManager = userManager;
        //private readonly IMapper _mapper;
        private readonly ITokenService _tokenService = tokenService;

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            List<User> users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

    }
}
