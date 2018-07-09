using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Identity;

namespace WBrand.Services.User
{
    public class PermissionService : IPermissionService
    {
        public static readonly AppRole ViewPermissionRecord = new AppRole { Name = nameof(Permission.ManagePermission), Description = "Xem quyền" };

        public IEnumerable<AppRole> GetPermissions()
        {
            return new[]
            {
                ViewPermissionRecord
            };
        }
    }
}
