//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace UserManagement.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly RoleManager<ApplicationRole> _roleManager;
//        private readonly IConfiguration _configuration;

//        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//            _configuration = configuration;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterModel model)
//        {
//            return Unauthorized();
//            //if (!PermissionsHelper.ValidatePermissions(model.UserPermissions))
//            //{
//            //    return BadRequest(new { error = "Invalid permissions" });
//            //}

//            //var user = new ApplicationUser
//            //{
//            //    UserName = model.Username,
//            //    Email = model.Email,
//            //    TenantId = model.TenantId,
//            //    UserPermissions = PermissionsHelper.AggregatePermissions(model.UserPermissions),
//            //    Role = model.Role
//            //};

//            //var result = await _userManager.CreateAsync(user, model.Password);

//            //if (result.Succeeded)
//            //{
//            //    return Ok(new { result = "User created successfully" });
//            //}

//            //return BadRequest(new { error = result.Errors });
//        }


//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginModel model)
//        {
//            return Unauthorized();
//            //    var user = await _userManager.FindByNameAsync(model.Username);

//            //    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
//            //    {
//            //        var authClaims = new List<Claim>
//            //{
//            //    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
//            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//            //    new Claim("TenantId", user.TenantId.ToString()),
//            //    new Claim("Permissions", ((int)user.UserPermissions).ToString()),
//            //    new Claim(ClaimTypes.Role, user.Role)
//            //};

//            //        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

//            //        var token = new JwtSecurityToken(
//            //            issuer: _configuration["Jwt:Issuer"],
//            //            audience: _configuration["Jwt:Audience"],
//            //            expires: DateTime.Now.AddHours(3),
//            //            claims: authClaims,
//            //            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
//            //        );

//            //        return Ok(new
//            //        {
//            //            token = new JwtSecurityTokenHandler().WriteToken(token),
//            //            expiration = token.ValidTo
//            //        });
//            //    }
//            //    return Unauthorized();
//        }

//        [HttpGet]
//        public async Task<IActionResult> HasPermissions(string permission, ClaimsPrincipal userPrincipal, string[] resource)
//        {
//            //Find user
//            //var user = await _userManager.FindByNameAsync(userPrincipal.Claims);

//            //get all permissions for this user for all resource

//            //check if he has permission requested

//            //return success
//            return Unauthorized();

//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllResources(string userId)
//        {
//            return Unauthorized();
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllPermissions(string userId)
//        {
//            return Unauthorized();
//        }


//}

//    public class RegisterModel
//    {
//        public string Username { get; set; }
//        public string Email { get; set; }
//        public string Password { get; set; }
//        public int TenantId { get; set; }
//        public List<Permissions> UserPermissions { get; set; }
//        public string Role { get; set; }
//    }

//    public class LoginModel
//    {
//        public string Username { get; set; }
//        public string Password { get; set; }
//    }

//}
