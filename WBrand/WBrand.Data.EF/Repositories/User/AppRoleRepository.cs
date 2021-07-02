using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Data.User;

namespace WBrand.Data.EF.Repositories.User
{
    public class AppRoleRepository : BaseRepository<AppRole>, IAppRoleRepository
    {
        public AppRoleRepository(WBrandDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<AppRole> GetListRoleByGroupId(int groupId)
        {
            var query = from g in DbContext.AppRoles.AsNoTracking()
                        join ug in DbContext.AppRoleGroups.AsNoTracking()
                        on g.Id equals ug.RoleId
                        where ug.GroupId == groupId
                        select g;
            return query;
        }
    }
}
