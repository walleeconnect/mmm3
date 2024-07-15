using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.Controllers
{
    public class DisableTwoFactorRequest
    {
        [Required]
        public string UserId { get; set; }
    }

    public class EnableTwoFactorRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string VerificationCode { get; set; }
    }

    public class SetPermissionsRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public IEnumerable<UserPermissionDto> Permissions { get; set; }
    }

    public class RemoveRoleRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }

    public class DeleteUserRequest
    {
        [Required]
        public string UserId { get; set; }
    }

    public class AssignRoleRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }

    public class UpdateUserRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string DisplayName { get; set; }
    }

    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string DisplayName { get; set; }
        public string Role { get; set; }
        public List<string> Permissions { get; set; }
    }

    public class RemoveUserClaimRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string ClaimType { get; set; }

        [Required]
        public string ClaimValue { get; set; }
    }

    public class AddUserClaimRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string ClaimType { get; set; }

        [Required]
        public string ClaimValue { get; set; }
    }

}
