using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //Post method /api/Auth/Register
        //Registraton method for users
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {

            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

            var identityResult =  await userManager.CreateAsync(identityUser,registerRequestDto.Password);

            if(identityResult.Succeeded)
            {
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Created Successfuly");
                    }
                }
                
            }
            return BadRequest("Something went wrong");
        }

        //Post methda /api/Auth/Login
        //Login method for users
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user =  await userManager.FindByEmailAsync(loginRequestDto.UserName);
            if(user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user,loginRequestDto.Password);

                if(checkPasswordResult)
                {

                    var roles = await userManager.GetRolesAsync(user);

                    if(roles!= null)
                    {
                        //Create token 
                        var jwtToken = tokenRepository.CreateJwtTokenAsync(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }
                }
            }
            return BadRequest("Authentication Failed");
        }
    }
}
