using AutoMapper;
using BookStoreApp.Api.Data;
using BookStoreApp.Api.Models;
using BookStoreApp.Api.Models.User;
using BookStoreApp.Api.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> userManager;
        private readonly IConfiguration configuration;

        private readonly RoleManager<IdentityRole> roleManager;

        public AuthController(ILogger<AuthController> 
            logger, IMapper mapper,
            UserManager<ApiUser> userManager,
            IConfiguration configuration


            )
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                logger.LogInformation($"Registration Attempt for {userDto.Email}");
                var user = mapper.Map<ApiUser>(userDto);
                user.UserName = userDto.Email;
                var result = await userManager.CreateAsync(user, userDto.Password);

                Console.WriteLine(result.Succeeded);

                if (result.Succeeded == false)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                //if (string.IsNullOrEmpty(userDto.Role))
                //{
                //    userDto.Role = "User"; // Default role
                //}
                //else
                //{
                //    userDto.Role = "Admin"; // Admin role
                //   
                //}

           //     await userManager.AddToRoleAsync(user, userDto.Role);

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
        public async Task<ActionResult<AuthResponse>> Login(LoginUserDto loginDto)
        {
            try
            {
                logger.LogInformation($"Login Attempt for {loginDto.Email}");
                var user = await userManager.FindByEmailAsync(loginDto.Email);
                var validPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);

                if (user == null || !validPassword)
                {
                    return Unauthorized("Invalid login attempt." + loginDto);
                }

                // Generate token (implementation depends on your token generation logic)
                string tokenString = await GenerateToken(user); // Placeholder for token generation logic


                var response = new AuthResponse
                {
                    Token = tokenString,
                    UserId = user.Id,
                    Email = loginDto.Email
                };
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while logging in user.");
                return StatusCode(500, "Internal server error");
            }
        }

        private async Task<string> GenerateToken(ApiUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();

            var userClaims = await userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(userClaims)  
            .Union(roleClaims);


            var  token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt32(configuration["JwtSettings:Duration"])),
                signingCredentials: credentials
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }









        // added roles 

        [HttpGet]
        [Route("getUserId({id})")]
        public async Task<ApiUser> GetById(string id)
        {
            return await userManager.FindByIdAsync(id);

        }

        [HttpPost]
        [Route("createRole")]
        public async Task<CreateRoleDto> AddToRolesAsync(CreateRoleDto role)
        {
            var identityRole = mapper.Map<IdentityRole>(role);
            await roleManager.CreateAsync(identityRole);
            return role;
        }

        [HttpPost]
        [Route("createUserRole")]
        public async Task<CreateUserRoleDto> AddUserRole(CreateUserRoleDto createUserRole)
        {
            var user = await GetById(createUserRole.UserId);
            await userManager.AddToRoleAsync(user, createUserRole.Role);
            return createUserRole;
        }














    }
}
