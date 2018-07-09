using AutoMapper;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WBrand.Common.Exceptions;
using WBrand.Common.Page;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Core.Domain.Models.User;
using WBrand.Services.Facade.Identity;
using WBrand.Services.User;
using WBrand.Web.Kernel.ViewModel.User;

namespace WBrand.Web.Api.Controllers.User
{
    [RoutePrefix("api/appUser")]
    public class AppUserController : BaseApiController
    {
        ApplicationUserManager _userManager;
        IAppGroupService _appGroupService;
        IAppRoleService _appRoleService;
        IAppUserService _appUserService;
        IMapper _mapper;
        public AppUserController(
            ApplicationUserManager userManager,
            IAppGroupService appGroupService,
            IAppRoleService appRoleService,
            IAppUserService appUserService,
            IMapper mapper)
        {
            _userManager = userManager;
            _appGroupService = appGroupService;
            _appRoleService = appRoleService;
            _appUserService = appUserService;
            _mapper = mapper;
        }

        [Route("allPage")]
        [HttpGet]
        //[Authorize(Roles = nameof(PermissionProvider.ManageUser))]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null, bool? isSystemAccount = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _userManager.Users;
                if (!string.IsNullOrEmpty(filter))
                    model = model.Where(x => x.UserName.Contains(filter) ||
                    x.FullName.Contains(filter));
                model = model.OrderBy(x => x.UserName);
                var modelUser = model.ToPagedList(page + 1, pageSize);
                IEnumerable<AppUser> list = modelUser.ToList();
                var modelVm = _mapper.Map<IEnumerable<AppUser>, IEnumerable<AppUserVm>>(list);

                PaginationSet<AppUserVm> pagedSet = new PaginationSet<AppUserVm>()
                {
                    Page = page,
                    TotalCount = modelUser.TotalItemCount,
                    TotalPages = (int)Math.Ceiling((decimal)modelUser.TotalItemCount / pageSize),
                    Items = modelVm
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [Route("detail/{id}")]
        [HttpGet]
        //[Authorize(Roles = nameof(PermissionProvider.ManageUser))]
        public HttpResponseMessage Details(HttpRequestMessage request, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " không có giá trị.");
            }
            var user = _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Không có dữ liệu");
            }
            else
            {
                var applicationUserViewModel = _mapper.Map<AppUser, AppUserVm>(user.Result);
                var listGroup = _appGroupService.GetListGroupByUserId(applicationUserViewModel.Id);
                applicationUserViewModel.Groups = _mapper.Map<IEnumerable<AppGroup>, IEnumerable<AppGroupVm>>(listGroup);
                return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
            }

        }

        [HttpPost]
        [Route("create")]
        //[Authorize(Roles = nameof(PermissionProvider.ManageUser))]
        public async Task<HttpResponseMessage> Create(HttpRequestMessage request, AppUserVm appUserVm)
        {
            if (ModelState.IsValid)
            {
                var newAppUser = _mapper.Map<AppUser>(appUserVm);
                newAppUser.CreatedDate = GetDateTimeNowUTC;
                newAppUser.CreatedBy = User.Identity.GetUserId();
                try
                {
                    newAppUser.Id = Guid.NewGuid().ToString();
                    var result = await _userManager.CreateAsync(newAppUser, appUserVm.Password);
                    if (result.Succeeded)
                    {
                        var listAppUserGroup = new List<AppUserGroup>();
                        foreach (var group in appUserVm.Groups)
                        {
                            listAppUserGroup.Add(new AppUserGroup()
                            {
                                GroupId = group.Id,
                                UserId = newAppUser.Id
                            });
                            //add role to user
                            var listRole = _appRoleService.GetListRoleByGroupId(group.Id);
                            foreach (var role in listRole)
                            {
                                await _userManager.RemoveFromRoleAsync(newAppUser.Id, role.Name);
                                await _userManager.AddToRoleAsync(newAppUser.Id, role.Name);
                            }
                        }
                        _appGroupService.InsertUserToGroups(listAppUserGroup, newAppUser.Id);

                        return request.CreateResponse(HttpStatusCode.OK, appUserVm);

                    }
                    else
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
                catch (Exception ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [Route("update")]
        //[Authorize(Roles = nameof(PermissionProvider.ManageUser))]
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request, AppUserVm applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByIdAsync(applicationUserViewModel.Id);
                RemoveAllRoleOfUser(appUser);
                try
                {
                    _mapper.Map(applicationUserViewModel, appUser);
                    //appUser.UpdateUser(applicationUserViewModel);
                    appUser.UpdatedDate = GetDateTimeNowUTC;
                    appUser.UpdatedBy = User.Identity.GetUserId();
                    var result = await _userManager.UpdateAsync(appUser);
                    if (result.Succeeded)
                    {
                        var listAppUserGroup = new List<AppUserGroup>();
                        foreach (var group in applicationUserViewModel.Groups)
                        {
                            listAppUserGroup.Add(new AppUserGroup()
                            {
                                GroupId = group.Id,
                                UserId = applicationUserViewModel.Id
                            });
                            //add role to user
                            var listRole = _appRoleService.GetListRoleByGroupId(group.Id);
                            foreach (var role in listRole)
                            {
                                await _userManager.AddToRoleAsync(appUser.Id, role.Name);
                            }
                        }
                        _appGroupService.InsertUserToGroups(listAppUserGroup, applicationUserViewModel.Id);
                        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);

                    }
                    else
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Route("resetPassword")]
        [HttpPut]
        public async Task<HttpResponseMessage> ResetPassword(HttpRequestMessage request, AppUserVm userVm)
        {
            //var appUser = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            var remove = await _userManager.RemovePasswordAsync(userVm.Id);
            var passWord = DateTime.Now.ToString("ddMMyyyyss");
            var result = await _userManager.AddPasswordAsync(userVm.Id, passWord);

            if (result.Succeeded)
                return request.CreateResponse(HttpStatusCode.OK, passWord);
            else
                return request.CreateErrorResponse(HttpStatusCode.OK, "Không cập nhật được password");

        }

        [HttpDelete]
        [Route("delete")]
        //[Authorize(Roles = nameof(PermissionProvider.ManageUser))]
        public async Task<HttpResponseMessage> Delete(HttpRequestMessage request, string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);
            appUser.State = Status.Delete;
            appUser.UpdatedDate = GetDateTimeNowUTC;
            var result = _userManager.Update(appUser);
            if (result.Succeeded)
                return request.CreateResponse(HttpStatusCode.OK, id);
            else
                return request.CreateErrorResponse(HttpStatusCode.OK, string.Join(",", result.Errors));
        }

        [Route("changepassword")]
        [HttpPut]
        public async Task<HttpResponseMessage> ChangePassword(HttpRequestMessage request, PasswordModel PwdVM)
        {
            var appUser = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            var result = await _userManager.ChangePasswordAsync(appUser.Id, PwdVM.PasswordCurrent, PwdVM.PasswordNew);

            if (result.Succeeded)
                return request.CreateResponse(HttpStatusCode.OK, User.Identity.GetUserId());
            else
                return request.CreateErrorResponse(HttpStatusCode.OK, "Không cập nhật được password");

        }

        private void RemoveAllRoleOfUser(AppUser applicationUser)
        {
            var listRole = _userManager.GetRoles(applicationUser.Id);
            foreach (var role in listRole)
            {
                _userManager.RemoveFromRole(applicationUser.Id, role);
            }
        }
    }
}
