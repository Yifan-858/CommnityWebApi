using AutoMapper;
using CommnityWebApi.Data.DTO;
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

        public UserController(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(string userName, string email, string password)
        {
            string passwordHash = password + "Hash";
            await _userRepo.CreateUser(userName, email, passwordHash);

            return Ok($"User {userName} is registered!");
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
