using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Common.Exceptions;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Data;
using WBrand.Data.User;
using WBrand.Services.User;

namespace WBrand.Services.Facade.User
{
    public class AppGroupService : BaseServices<AppGroup>, IAppGroupService
    {
        IAppGroupRepository _appGroupRepo;
        IAppUserGroupRepository _appUserGroupRepo;
        public AppGroupService(IAppGroupRepository appGroupRepo,
            IAppUserGroupRepository appUserGroupRepo) : base(appGroupRepo)
        {
            _appGroupRepo = appGroupRepo;
            _appUserGroupRepo = appUserGroupRepo;
        }

        public bool InsertUserToGroups(IEnumerable<AppUserGroup> groups, string userId)
        {
            var modelAppUserGroup = _appUserGroupRepo.Table.Where(x => x.UserId == userId);
            _appUserGroupRepo.DeleteAsync(modelAppUserGroup);
            foreach (var userGroup in groups)
            {
                _appUserGroupRepo.Insert(userGroup);
            }
            return true;
        }

        public AppGroup Delete(int id)
        {
            var appGroup = this._appGroupRepo.Table.FirstOrDefault(x => x.Id == id);
            return _appGroupRepo.Delete(appGroup);
        }

        public IEnumerable<AppGroup> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _appGroupRepo.TableNoTracking;
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Name.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<AppGroup> GetAll()
        {
            return _appGroupRepo.TableNoTracking.ToList();
        }

        public AppGroup GetDetail(int id)
        {
            return _appGroupRepo.TableNoTracking.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<AppGroup> GetListGroupByUserId(string userId)
        {
            return _appGroupRepo.GetListGroupByUserId(userId).ToList();
        }

        public IEnumerable<AppUser> GetListUserByGroupId(int groupId)
        {
            return _appGroupRepo.GetListUserByGroupId(groupId).ToList();
        }

        public override AppGroup Insert(AppGroup appGroup)
        {
            if (_appGroupRepo.TableNoTracking.Any(x => x.Name == appGroup.Name))
                throw new NameDuplicatedException("Tên không được trùng");
            return _appGroupRepo.Insert(appGroup);
        }

        public override AppGroup Update(AppGroup appGroup)
        {
            if (_appGroupRepo.TableNoTracking.Any(x => x.Name == appGroup.Name && x.Id != appGroup.Id))
                throw new NameDuplicatedException("Tên không được trùng");
            return _appGroupRepo.Update(appGroup);
        }
    }
}
