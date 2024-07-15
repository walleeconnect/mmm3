namespace UserManagement.API
{
    public static class PermissionsHelper
    {
        public static Permissions AggregatePermissions(List<Permissions> permissions)
        {
            Permissions aggregatedPermissions = Permissions.None;
            foreach (var permission in permissions)
            {
                aggregatedPermissions |= permission;
            }
            return aggregatedPermissions;
        }

        public static bool ValidatePermissions(List<Permissions> permissions)
        {
            foreach (var permission in permissions)
            {
                if (!Enum.IsDefined(typeof(Permissions), permission))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
