using AutoMapper;
using CommnityWebApi.Core.Interfaces;
using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;
using CommnityWebApi.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CommnityWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserRepo userRepo, IMapper mapper, IUserService userService)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO signUpDTO)
        {
            try
            {
                var userName = await _userService.RegisterUser(signUpDTO);
                return Ok($"User {userName} is registered!");
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                var user = await _userService.Login(login);
                var token = _userService.GenerateToken(user);
                return Ok(token);
            }
            catch (Exception ex)
            { return Unauthorized($"Invalid login: {ex}"); }
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepo.GetUserById(int.Parse(userId));

            if (user == null)
            {
                return NotFound();
            }

            UserDTO userDTO = _mapper.Map<UserDTO>(user);

            return Ok(userDTO);
        }

        [Authorize]
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateMyProfile([FromBody]UpdateUserDTO updateUserDTO)
        {
            var userIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userIdentifier == null)
            {
                return BadRequest("Invalid access");
            }
            //if (!int.TryParse(userIdentifier, out var userId))
            //{
            //    return Unauthorized("Invalid user identity");
            //}
            var userId = int.Parse(userIdentifier);
            var currentUser = await _userService.GetUserById(userId);

            if (currentUser == null)
            {
                return NotFound("User not found"); 
            }

            var passwordHash = updateUserDTO.Password + "Hash";
            var updatedUser = await _userService.UpdateUserProfile
                (userId, updateUserDTO.UserName, updateUserDTO.Email, passwordHash);

            var userDto = _mapper.Map<UserDTO>(updatedUser);
            return Ok(userDto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok($"User with id: {id} is deleted");
            }
            catch (Exception ex) { return NotFound(ex.Message); }
             
        }

        //[Authorize(Roles = "Admin")]
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUserById(int id)
        //{
        //    var user = await _userRepo.GetUserById(id);
        //    if(user == null)
        //    {
        //        return NotFound();
        //    }

        //    AdmincheckDTO userDTO = _mapper.Map<AdmincheckDTO>(user);

            //return Ok(AdminCheckDTO);
        //}
    }
}
