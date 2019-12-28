using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TBP.Contracts;
using TBP.Contracts.Authentication;
using TBP.Interfaces;

namespace TBP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public AuthenticationController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginRequestModel request)
        {
            var result = await _service.Login(request.UserName, request.Password);

            if (result.Success)
                return Ok(_mapper.Map<AuthLoginResponseModel>(result));

            return BadRequest(_mapper.Map<ErrorResponseModel>(result));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRegistrationRequestModel request)
        {
            var result = await _service.Register(request.UserName, request.Email, request.Password);

            if (result.Success)
                return Ok();

            return BadRequest(_mapper.Map<ErrorResponseModel>(result));
        }
    }
}