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

        public async Task<bool> UserHasPermissionAsync(string username, int groupId, int entityId, int moduleId, int submoduleId, string permissionName)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return false;
            }

            var userId = user.Id;

            // Check if the user is part of the group
            var isInGroup = await _context.UserGroups
                .AnyAsync(ug => ug.UserId == userId && ug.GroupId == groupId);

            if (!isInGroup)
            {
                return false;
            }

            // Check if the user is an owner of the entity
            var isEntityOwner = await _context.EntityOwners
                .AnyAsync(eo => eo.UserId == userId && eo.EntityId == entityId);

            if (!isEntityOwner)
            {
                return false;
            }

            // Check if the user has the required permission
            var hasPermission = await _context.UserPermissions
                .AnyAsync(up => up.UserId == userId
                                && up.GroupId == groupId
                                && up.EntityId == entityId
                                && up.ModuleId == moduleId
                                && up.SubmoduleId == submoduleId
                                && up.Permission.Name == permissionName);

            return hasPermission;
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
