using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly UserPermissionService _userPermissionService;
        private readonly ApplicationDbContext _context;
        public UserAccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IConfiguration configuration,
                                 UserPermissionService userPermissionService,
                                 ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userPermissionService = userPermissionService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var user = new ApplicationUser { UserName = model.Username, Email = model.Email, Role=model.Role };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                    return Ok(new { result = "User registered successfully" });
                }
            }
            catch (Exception ex)
            {
            }
            return BadRequest("");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpGet("check-permission")]
        public async Task<IActionResult> CheckPermission(int groupId, int entityId, int moduleId, int submoduleId, string permissionName)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var hasPermission = await _userPermissionService.UserHasPermissionAsync(userId, groupId, entityId, moduleId, submoduleId, permissionName);

            if (hasPermission)
            {
                return Ok(new { result = "Permission granted" });
            }

            return Forbid();
        }

        [Authorize(Roles = "GroupOwner, EntityOwner")]
        [HttpPost("map-permissions")]
        public async Task<IActionResult> MapPermissions([FromBody] MapPermissionModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var userPermission = new UserPermission
            {
                UserId = user.Id,
                GroupId = model.GroupId,
                EntityId = model.EntityId,
                ModuleId = model.ModuleId,
                SubmoduleId = model.SubmoduleId,
                PermissionId = model.PermissionId
            };

            _context.UserPermissions.Add(userPermission);
            await _context.SaveChangesAsync();

            return Ok(new { result = "Permission mapped successfully" });
        }

        [HttpGet("get-permissions")]
        public async Task<IActionResult> GetAllPermissions(string UserId)
        {
            var hasPermission = await _userPermissionService.GetPermissions(UserId);
            var user = await _userManager.FindByNameAsync(UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }


            //_context.UserPermissions.Add(userPermission);
            //await _context.SaveChangesAsync();

            return Ok(new { result = "Permission mapped successfully" });
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));

                 var token = new JwtSecurityToken(
                     issuer: _configuration["Jwt:Issuer"],
                     audience: _configuration["Jwt:Audience"],
                     expires: DateTime.Now.AddHours(3),
                     claims: claims,
                     signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                 );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


    public class RegisterModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class MapPermissionModel
    {
        public string UserId { get; set; }
        public int ModuleId { get; set; }
        public int? SubmoduleId { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int? CityId { get; set; }
        public int PermissionId { get; set; }
        public int GroupId { get; set; }
        public int EntityId { get; set; }
    }

}
