using AutoMapper;
using CommnityWebApi.Core.Interfaces;
using CommnityWebApi.Data.DTO;
using CommnityWebApi.Data.Entities;
using CommnityWebApi.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> SignUp(string userName, string email, string password)
        {
            try
            {
                var user = await _userService.RegisterUser(userName, email, password);
                return Ok($"User {userName} is registered!");
            }
            catch(Exception ex) { return BadRequest(ex.Message); }  
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            User user = await _userService.Login(login);
            if (user != null)
            {
                return Ok(_userService.GenerateToken(user));
            }

            return Unauthorized("Invalid login");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepo.GetUserById(id);

            if(user == null)
            {
                return NotFound();
            }

            UserDTO userDTO = _mapper.Map<UserDTO>(user);

            return Ok(userDTO);
        }
    }
}
