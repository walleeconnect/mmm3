using Microsoft.EntityFrameworkCore;

namespace UserManagement.API
{
    public class UserPermissionService
    {
        private readonly ApplicationDbContext _context;

        public UserPermissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserHasPermissionAsync(string userId, int groupId, int entityId, int moduleId, int submoduleId, string permissionName)
        {
            var permission = await _context.Permissions.SingleOrDefaultAsync(p => p.Name == permissionName);
            if (permission == null)
            {
                return false;
            }

            var userPermission = await _context.UserPermissions
                .Include(up => up.Permission)
                .SingleOrDefaultAsync(up => up.UserId == userId &&
                                            up.GroupId == groupId &&
                                            up.EntityId == entityId &&
                                            up.ModuleId == moduleId &&
                                            up.SubmoduleId == submoduleId &&
                                            up.PermissionId == permission.Id);

            return userPermission != null;
        }

        public async Task<List<UserPermission>> GetPermissions(string userId)
        {

            var groups = _context.Groups;
            //var permission = await _context.Permissions.SingleOrDefaultAsync(p => p.Name == permissionName);
            //if (permission == null)
            //{
            //   // return false;
            //}

            var userPermission = await _context.UserPermissions
                .Include(up => up.Permission)
                .Where(x=>x.UserId == userId)
                .ToListAsync();

            return userPermission;
        }
    }

}
