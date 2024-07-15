using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserManagerService _userManagerService;

        public AccountController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManagerService.CreateUserAsync(model.UserName, model.Email, model.DisplayName, model.Password);
            return Ok(user);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManagerService.UpdateUserAsync(model.UserId, model.Email, model.DisplayName);
            return Ok(user);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserRequest model)
        {
            var result = await _userManagerService.DeleteUserAsync(model.UserId);
            return Ok(result);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest model)
        {
            await _userManagerService.AssignRoleAsync(model.UserId, model.RoleName);
            return Ok();
        }

        [HttpPost("remove-role")]
        public async Task<IActionResult> RemoveRole([FromBody] RemoveRoleRequest model)
        {
            await _userManagerService.RemoveRoleAsync(model.UserId, model.RoleName);
            return Ok();
        }

        [HttpPost("set-permissions")]
        public async Task<IActionResult> SetPermissions([FromBody] SetPermissionsRequest model)
        {
            await _userManagerService.SetPermissionsAsync(model.UserId, model.Permissions);
            return Ok();
        }

        [HttpPost("enable-2fa")]
        public async Task<IActionResult> EnableTwoFactorAuthentication([FromBody] EnableTwoFactorRequest model)
        {
            await _userManagerService.EnableTwoFactorAuthenticationAsync(model.UserId, model.VerificationCode);
            return Ok();
        }

        [HttpPost("disable-2fa")]
        public async Task<IActionResult> DisableTwoFactorAuthentication([FromBody] DisableTwoFactorRequest model)
        {
            await _userManagerService.DisableTwoFactorAuthenticationAsync(model.UserId);
            return Ok();
        }

        [HttpGet("claims/{userId}")]
        public async Task<IActionResult> GetUserClaims(string userId)
        {
            var claims = await _userManagerService.GetUserClaimsAsync(userId);
            return Ok(claims);
        }

        [HttpPost("add-claim")]
        public async Task<IActionResult> AddUserClaim([FromBody] AddUserClaimRequest model)
        {
            await _userManagerService.AddUserClaimAsync(model.UserId, new Claim(model.ClaimType, model.ClaimValue));
            return Ok();
        }

        [HttpPost("remove-claim")]
        public async Task<IActionResult> RemoveUserClaim([FromBody] RemoveUserClaimRequest model)
        {
            await _userManagerService.RemoveUserClaimAsync(model.UserId, new Claim(model.ClaimType, model.ClaimValue));
            return Ok();
        }
    }
}
