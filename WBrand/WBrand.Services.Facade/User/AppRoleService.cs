using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Common.Exceptions;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Data.User;
using WBrand.Services.User;

namespace WBrand.Services.Facade.User
{
    public class AppRoleService : IAppRoleService
    {
        IAppRoleRepository _appRoleRepo;
        IAppRoleGroupRepository _appRoleGroupRepo;
        IPermissionService _permissionService;
        public AppRoleService(IAppRoleRepository appRoleRepo,
        IAppRoleGroupRepository appRoleGroupRepo,
        IPermissionService permissionService)
        {
            _appRoleRepo = appRoleRepo;
            _appRoleGroupRepo = appRoleGroupRepo;
            _permissionService = permissionService;
        }
        public bool InsertRolesToGroup(IEnumerable<AppRoleGroup> roleGroups, int groupId)
        {
            var modelAppRoleGroup = _appRoleGroupRepo.Table.Where(x => x.GroupId == groupId).ToList();
            _appRoleGroupRepo.Delete(modelAppRoleGroup);
            foreach (var roleGroup in roleGroups)
            {
                _appRoleGroupRepo.Insert(roleGroup);
            }
            return true;
        }

        public void Delete(string id)
        {
            var modelAppRole = _appRoleRepo.TableNoTracking.Where(x => x.Id == id);
            _appRoleRepo.Delete(modelAppRole);
        }

        public IEnumerable<AppRole> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _appRoleRepo.TableNoTracking;
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Description.Contains(filter));

            totalRow = query.Count();
            return query.OrderBy(x => x.Description).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<AppRole> GetAll()
        {
            return _appRoleRepo.TableNoTracking.ToList();
        }

        public AppRole GetDetail(string id)
        {
            return _appRoleRepo.TableNoTracking.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<AppRole> GetListRoleByGroupId(int groupId)
        {
            return _appRoleRepo.GetListRoleByGroupId(groupId).ToList();
        }

        public AppRole Insert(AppRole appRole)
        {
            if (_appRoleRepo.TableNoTracking.Any(x => x.Name == appRole.Name))
                throw new NameDuplicatedException("Tên không được trùng");
            return _appRoleRepo.Insert(appRole);
        }

        public void InsertRoleFromSystem()
        {
            var permissionDefault = _permissionService.GetPermissions();
            var modelRole = _appRoleRepo.TableNoTracking.ToList();

            permissionDefault = permissionDefault.Where(x => !modelRole.Select(b => b.Name).Contains(x.Name));
            if (permissionDefault.Count() > 0)
            {
                var appRoles = new List<AppRole>();
                foreach (var item in permissionDefault)
                {
                    var newAppRole = new AppRole();
                    newAppRole.Id = Guid.NewGuid().ToString();
                    newAppRole.Name = item.Name;
                    newAppRole.Description = item.Description;
                    appRoles.Add(newAppRole);
                }
                _appRoleRepo.Insert(appRoles);
            }

        }

        public AppRole Update(AppRole AppRole)
        {
            if (_appRoleRepo.TableNoTracking.Any(x => x.Description == AppRole.Description && x.Id != AppRole.Id))
                throw new NameDuplicatedException("Tên không được trùng");
            return _appRoleRepo.Update(AppRole);
        }
    }
}
