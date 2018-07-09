using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Identity;

namespace WBrand.Services.User
{
    public interface IAppUserService
    {
        IPagedList<AppUser> GetAll(int pageIndex = 1, int pageSize = 20, string filter = "", int userType = 0);
        IEnumerable<AppUser> GetByIds(List<string> userIds);
        AppUser GetById(string Id);
    }
}
