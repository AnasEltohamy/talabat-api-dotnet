using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using talabat.API.DTOs.BasketAndOrderDtos;
using talabat.API.DTOs.IdentityDtos;
using talabat.API.Errors;
using talabat.API.Helpers;
using talabat.Core.Entites.Identity;
using talabat.Core.Services.Contract;

namespace talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountController(
            SignInManager<AppUser> signInManager ,
            UserManager<AppUser> userManager,
            IAuthService authService,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authService = authService;
            _mapper = mapper;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User == null) 
            {
                return Unauthorized(new ApiResponse(401));
            }
            var Result = await _signInManager.CheckPasswordSignInAsync(User ,loginDto.Password,false);
            if (Result.Succeeded is false)
            {
                return Unauthorized(new ApiResponse(401, "Chick The Password OR Email "));
            }
            return Ok(new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _authService.CreateTokenAsync(User, _userManager)
            });

        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (CheckEmailExists(registerDto.Email).Result.Value)
            {

                return BadRequest(new ApiResponse(400, "This email is already exists !! "));
            }
            var User = new AppUser()
            {
                DisplayName = registerDto.Name,
                Email = registerDto.Email,
                UserName = registerDto.Email.Split("@")[0],
                PhoneNumber = registerDto.PhoneNumber
            };

            var Result = await _userManager.CreateAsync(User ,registerDto.Password);
            if (Result.Succeeded)
            {
                return Ok(new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await _authService.CreateTokenAsync(User, _userManager)
                });

            }
            else
            {
                return Unauthorized(new ApiResponse(400, "Email already exists"));

            }
        
        }



        //Get Current User
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user =await _userManager.FindByEmailAsync(userEmail);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token =await _authService.CreateTokenAsync(user ,_userManager)
            });
        }



        //Get User Address
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindUserWithoutAddressByEmailAsync(Email);
            if (user is null)
            {

                return NotFound(new ApiResponse(404));
            }
            
            

            return Ok(_mapper.Map<AddressDto>(user.Address));

        }




        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto UpdateAddressDTO)
        {

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindUserWithoutAddressByEmailAsync(userEmail);


            //if (user?.Address is null)
            //    return BadRequest(new ApiResponse(400, "User has no address"));

            //// Update tracked entity instead of replacing it
            //_mapper.Map(UpdateAddress, user.Address);




            if (user?.Address is null)
            {
                // Update tracked entity instead of replacing it
                 user.Address = _mapper.Map<AddressDto, Address>(UpdateAddressDTO);

            }
            else
            {
                _mapper.Map(UpdateAddressDTO, user.Address);  
            }
            var Result = await _userManager.UpdateAsync(user);
            if (Result.Succeeded)
            {
                return Ok(UpdateAddressDTO);
            }
            else
            {
                return BadRequest(new ApiResponse(400, "Address not updated"));
            }



        }




        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
