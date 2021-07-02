using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Identity;

namespace WBrand.Services.User
{
    public interface IAppGroupService: IBaseServices<AppGroup>
    {
        AppGroup GetDetail(int id);

        IEnumerable<AppGroup> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<AppGroup> GetAll();


        AppGroup Delete(int id);

        bool InsertUserToGroups(IEnumerable<AppUserGroup> groups, string userId);

        IEnumerable<AppGroup> GetListGroupByUserId(string userId);

        IEnumerable<AppUser> GetListUserByGroupId(int groupId);
    }
}
