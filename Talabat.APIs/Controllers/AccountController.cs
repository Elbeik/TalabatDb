using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Extenions;
using Talabat.Domine.Entites.Identity;
using Talabat.Domine.IServices;

namespace Talabat.APIs.Controllers
{
    public class AccountController : GenericController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]

        public async Task<ActionResult<UserDto>> Login(loginDto loginDto)
        {
            var userEmail = await _userManager.FindByEmailAsync(loginDto.Email);
            if(userEmail == null) return Unauthorized(new ApiResponse(401));

            var password = await _signInManager.CheckPasswordSignInAsync(userEmail, loginDto.Password, false);
            if (!password.Succeeded) return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto()
            {
                DisplayName = userEmail.DisplayName,
                Email = userEmail.Email,
                Token = await _tokenService.CreatToken(userEmail, _userManager)
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if(CheckEmailExisteing(registerDto.Email).Result.Value)
                return BadRequest(new ApiValidationErrorRespone() { ErrorsResponse = new[] {"this email is use"}});

            var user = new AppUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split("@")[0],
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreatToken(user, _userManager)
            });


        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreatToken(user, _userManager)
            });
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto addressDto)
        {
            var mapAddress =  _mapper.Map<AddressDto, Address>(addressDto);

            var user = await _userManager.FindUserWithAddressByEmailAsync(User);

            user.Address = mapAddress;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400, "Error Occured Duraing Update"));

            return Ok(_mapper.Map<Address, AddressDto>(user.Address));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("address")]

        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);

            return Ok(_mapper.Map<Address, AddressDto>(user.Address));
        }

        [HttpGet("emailexisting")]
        public async Task<ActionResult<bool>> CheckEmailExisteing(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }


    }
}
