using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Identity;

namespace WBrand.Data.User
{
    public interface IAppRoleRepository : IEfRepository<AppRole>
    {
        IEnumerable<AppRole> GetListRoleByGroupId(int groupId);
    }
}
