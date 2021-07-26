using backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using backend.Services;
using backend.context;
using AutoMapper;

namespace backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly UserService _userService;
        public AuthController(DatabaseContext context, IMapper mapper)
        {
            _userService = new UserService(context, mapper);
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] User user)
        {
            Task<string> tokenString;
            if (user == null)
            {
                return BadRequest("Invalid request - User object empty!");
            }
            try
            {
                tokenString = _userService.Login(user);
                if (tokenString.Result == "Not authenticated")
                {
                    return Unauthorized();
                }
                return Ok(new { Token = tokenString.Result });
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Unauthorized();
        }
        [HttpPost, Route("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("User not valid");
            }
            if (_userService.DoesUserExist(user.Username)) {
                return BadRequest("Username already exists");
            }
            try
            {
                return Ok(_userService.Register(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
