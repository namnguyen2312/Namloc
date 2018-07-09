using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Data.User;
using WBrand.Services.User;

namespace WBrand.Services.Facade.User
{
    public class AppUserService : IAppUserService
    {
        IAppUserRepository _appUserRepo;
        public AppUserService(IAppUserRepository appUserRepo)
        {
            _appUserRepo = appUserRepo;
        }
        public AppUser GetById(string Id)
        {
            return _appUserRepo.TableNoTracking.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<AppUser> GetByIds(List<string> userIds)
        {
            return _appUserRepo.TableNoTracking.Where(x => userIds.Contains(x.Id)).ToList();
        }

        public IPagedList<AppUser> GetAll(int pageIndex = 1, int pageSize = 20, string filter = "", int userType = 0)
        {
            var query = _appUserRepo.TableNoTracking;
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.FullName.Contains(filter)
                || x.UserName.Contains(filter));
            query = query.OrderBy(x => x.UserName);
            return query.ToPagedList(pageIndex, pageSize);
        }
    }
}
