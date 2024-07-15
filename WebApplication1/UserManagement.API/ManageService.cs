using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace UserManagement.API
{
    public interface IUserManagerService
    {
        Task<ApplicationUser> CreateUserAsync(string userName, string email, string displayName, string password);
        Task<ApplicationUser> UpdateUserAsync(string userId, string email, string displayName);
        Task<bool> DeleteUserAsync(string userId);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task AssignRoleAsync(string userId, string roleName);
        Task RemoveRoleAsync(string userId, string roleName);
        Task SetPermissionsAsync(string userId, IEnumerable<UserPermissionDto> permissions);
        Task EnableTwoFactorAuthenticationAsync(string userId, string verificationCode);
        Task DisableTwoFactorAuthenticationAsync(string userId);
        Task<IEnumerable<Claim>> GetUserClaimsAsync(string userId);
        Task AddUserClaimAsync(string userId, Claim claim);
        Task RemoveUserClaimAsync(string userId, Claim claim);
    }

    public class UserManagerService : IUserManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UserManagerService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<ApplicationUser> CreateUserAsync(string userName, string email, string displayName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
               // DisplayName = displayName
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return user;
        }

        public async Task<ApplicationUser> UpdateUserAsync(string userId, string email, string displayName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.Email = email;
           // user.DisplayName = displayName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return user;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task RemoveRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task SetPermissionsAsync(string userId, IEnumerable<UserPermissionDto> permissions)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            //var userPermissions = await _context.UserPermissions
            //    .Where(up => up.UserId == userId)
            //    .ToListAsync();

            //_context.UserPermissions.RemoveRange(userPermissions);

            foreach (var permissionDto in permissions)
            {
                //var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.Name == permissionDto.PermissionName);
               // var module = await _context.Modules.FirstOrDefaultAsync(m => m.Name == permissionDto.ModuleName);
               // var typeOfLaw = await _context.TypesOfLaw.FirstOrDefaultAsync(t => t.Name == permissionDto.TypeOfLawName);

               // if (permission == null || module == null || typeOfLaw == null)
                {
                    throw new Exception("Permission, module, or type of law not found");
                }

                //_context.UserPermissions.Add(new UserPermission
                //{
                //    UserId = userId,
                //    PermissionId = permission.Id,
                //    ModuleId = module.Id,
                //    TypeOfLawId = typeOfLaw.Id,
                //    CanView = permissionDto.CanView,
                //    CanUpdate = permissionDto.CanUpdate,
                //    CanDelete = permissionDto.CanDelete
                //});
            }

            await _context.SaveChangesAsync();
        }

        public async Task EnableTwoFactorAuthenticationAsync(string userId, string verificationCode)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var isTokenValid = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);
            if (!isTokenValid)
            {
                throw new Exception("Invalid verification code.");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
        }

        public async Task DisableTwoFactorAuthenticationAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false);
        }

        public async Task<IEnumerable<Claim>> GetUserClaimsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return await _userManager.GetClaimsAsync(user);
        }

        public async Task AddUserClaimAsync(string userId, Claim claim)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var result = await _userManager.AddClaimAsync(user, claim);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task RemoveUserClaimAsync(string userId, Claim claim)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var result = await _userManager.RemoveClaimAsync(user, claim);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }


    public class UserPermissionDto
    {
        public string PermissionName { get; set; }
        public string ModuleName { get; set; }
        public string TypeOfLawName { get; set; }
        public bool CanView { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }

}
