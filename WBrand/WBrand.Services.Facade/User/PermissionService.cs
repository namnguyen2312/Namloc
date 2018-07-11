using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Services.User;

namespace WBrand.Services.Facade.User
{
    public class PermissionService : IPermissionService
    {
        public static readonly AppRole ViewPermissionRecord = new AppRole { Name = nameof(Permission.ManagePermission), Description = "Quản trị quyền" };
        public static readonly AppRole ViewUserRecord = new AppRole { Name = nameof(Permission.ManageUser), Description = "Quản trị User" };
        public static readonly AppRole ViewGroupRecord = new AppRole { Name = nameof(Permission.ManageGroup), Description = "Quản trị nhóm user" };

        public IEnumerable<AppRole> GetPermissions()
        {
            return new[]
            {
                ViewPermissionRecord,
                ViewUserRecord,
                ViewGroupRecord
            };
        }
    }
}
