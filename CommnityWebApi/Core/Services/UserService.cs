using CommnityWebApi.Core.Interfaces;
using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;
using CommnityWebApi.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CommnityWebApi.Core.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepo userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public async Task<User> RegisterUser(string userName, string email, string password)
        {
            var isExisting = await _userRepo.GetUserByEmail(email);
            if(isExisting != null)
            {
                throw new Exception("Email is already registered.");
            }

            string passwordHash = password + "Hash";
            var user = await _userRepo.CreateUser(userName, email, passwordHash);

            return user;
        }

        public string GenerateToken(User user)
        {
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role,"Admin"));
                
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                     issuer: issuer,
                     audience: audience, 
                     claims: claims, 
                     expires: DateTime.Now.AddMinutes(20), 
                     signingCredentials: signinCredentials);

     
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

        public async Task<User> Login([FromBody] LoginDTO login)
        {
            User? user = await _userRepo.GetUserByEmail(login.Email);

            if(user == null)
            {
                throw new Exception("Invalid login.");
            }

            var hashedPassword = login.Password + "Hash";

            if(user.PasswordHash != hashedPassword)
            {
                throw new Exception("Invalid login.");
            }

            return user;
        }
    }
}
