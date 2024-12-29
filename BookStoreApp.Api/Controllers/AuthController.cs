using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> userManager;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                logger.LogInformation($"Registration Attempt for {userDto.Email}");
                var user = mapper.Map<ApiUser>(userDto);
                var result = await userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrEmpty(userDto.Role))
                {
                    userDto.Role = "User"; // Default role
                }
                else
                {
                    userDto.Role = "Admin"; // Admin role
                    await userManager.AddToRoleAsync(user, userDto.Role);
                }

               

                return Accepted();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while registering user.");
                return StatusCode(500, "Internal server error");
            }
        }
        
        
        
        
        
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUserDto loginDto)
        {
            try
            {
                logger.LogInformation($"Login Attempt for {loginDto.Email}");
                var user = await userManager.FindByEmailAsync(loginDto.Email);
                var hashedPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);

                if (user == null || !hashedPassword)
                {
                    return Unauthorized("Invalid login attempt.");
                }

                // Generate token (implementation depends on your token generation logic)
                var token = "GeneratedToken"; // Placeholder for token generation logic

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while logging in user.");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
