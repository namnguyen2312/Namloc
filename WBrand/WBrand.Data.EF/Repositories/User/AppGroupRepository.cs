using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Data.User;

namespace WBrand.Data.EF.Repositories.User
{
    public class AppGroupRepository : BaseRepository<AppGroup>, IAppGroupRepository
    {
        public AppGroupRepository(WBrandDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<AppGroup> GetListGroupByUserId(string userId)
        {

            var query = from g in DbContext.AppGroups.AsNoTracking()
                        join ug in DbContext.AppUserGroups.AsNoTracking()
                        on g.Id equals ug.GroupId
                        where ug.UserId == userId
                        select g;
            return query.ToList();
        }

        public IEnumerable<AppUser> GetListUserByGroupId(int groupId)
        {
            var query = from g in DbContext.AppGroups.AsNoTracking()
                        join ug in DbContext.AppUserGroups.AsNoTracking()
                        on g.Id equals ug.GroupId
                        join u in DbContext.Users
                        on ug.UserId equals u.Id
                        where ug.GroupId == groupId
                        select u;
            return query.ToList();
        }
    }
}
