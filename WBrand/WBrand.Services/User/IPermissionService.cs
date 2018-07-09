using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Identity;

namespace WBrand.Services.User
{
    public interface IPermissionService
    {
        IEnumerable<AppRole> GetPermissions();
    }
}
