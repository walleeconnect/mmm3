using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenceController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly UserPermissionService _userPermissionService;
        private readonly ApplicationDbContext _context;


        public ReferenceController(UserManager<ApplicationUser> userManager,
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


        [HttpGet("groups")]
        public async Task<IActionResult> GetGroups()
        {
            try
            {
              var result = await  _context.Groups.ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
            }
            return BadRequest("");
        }
    }
}
