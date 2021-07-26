using AutoMapper;
using backend.context;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services
{
    public class UserService
    {
        readonly DatabaseContext _context;
        readonly IMapper _mapper;
        public UserService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> Login(User user)
        {
            var tokenString = "";

            var userLoggingIn = await _context.User.FirstOrDefaultAsync(x => (x.Username == user.Username) && (x.Password == user.Password));
            
            if (userLoggingIn != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signingCredentials
                );

                tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return tokenString;
            }
            
            return "Not authenticated";

        }
        public async Task<UserDTO> Register(User user)
        {
            var addedUser = await _context.User.FirstOrDefaultAsync(x => x.Username == user.Username);
            _context.User.Add(user);
            _context.SaveChanges();
            return _mapper.Map<UserDTO>(addedUser);

        }
        public bool DoesUserExist(string username)
        {
            //var movies = _context.Movie.Where(x => x.Title.ToLower().Contains())
            var user = _context.User.FirstOrDefaultAsync(x => x.Username == username);
            if (user.Result != null)
            {
                return true;
            }
            return false;
        }
    }
}
