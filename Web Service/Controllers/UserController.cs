using AutoMapper;
using ConcertBookingApp.Data.Database;
using Microsoft.AspNetCore.Mvc;
using Shared_Resources.DTOs;
using SharedResources.Models;

namespace Web_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(UnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("FindUser/{fullName}/{email}")]
        public async Task<IActionResult> DoesUserExist(string fullName, string email)
        {
            try
            {
                User user = await _unitOfWork.User.FindUser(fullName, email);

                if (user != null)
                {
                    UserDTO userdto = _mapper.Map<UserDTO>(user);
                    return Ok(userdto);
                }
                else
                    return StatusCode(404, "NotFound");
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                return StatusCode(500, "InternalServerError");
            }

        }

        [HttpPost("SaveUser")]
        public async Task<IActionResult> SaveUser(UserDTO userDTO)
        {
            try
            {
                User user = _mapper.Map<User>(userDTO);
                int userId = await _unitOfWork.User.AddUser(user);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
