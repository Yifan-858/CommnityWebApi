using AutoMapper;
using CommnityWebApi.Core.Interfaces;
using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;
using CommnityWebApi.Data.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepo, IConfiguration configuration,IMapper mapper)
        {
            _userRepo = userRepo;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<string> RegisterUser([FromBody] SignUpDTO signUpDTO)
        {
            var isExisting = await _userRepo.GetUserByEmail(signUpDTO.Email);
            if(isExisting != null)
            {
                throw new Exception("Email is already registered.");
            }

            string passwordHash = signUpDTO.Password + "Hash";
            var user = await _userRepo.CreateUser(signUpDTO.UserName, signUpDTO.Email, passwordHash);

            return user.UserName;
        }

        public string GenerateToken(User user)
        {
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            claims.Add(new Claim(ClaimTypes.Role,"User"));
                
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                     issuer: issuer,
                     audience: audience, 
                     claims: claims, 
                     expires: DateTime.UtcNow.AddMinutes(20), 
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

        public async Task<User> GetUserById(int userId)
        {
            return await _userRepo.GetUserById(userId);
        }

        public async Task<User> UpdateUserProfile(int userId, string? userName, string? email, string? passwordHash )
        {
            var user = await _userRepo.UpdateUser(userId, userName, email, passwordHash);

            return user;
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _userRepo.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            await _userRepo.DeleteUser(user);
        }
    }
}
