using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using WBrand.Common.Exceptions;
using WBrand.Common.Page;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Services.Facade.Identity;
using WBrand.Services.User;
using WBrand.Web.Kernel.ViewModel.User;

namespace WBrand.Web.Api.Controllers.User
{
    [RoutePrefix("api/appGroup")]
    public class AppGroupController : BaseApiController
    {
        private readonly IAppGroupService _appGroupService;
        private readonly IAppRoleService _appRoleService;
        private readonly ApplicationUserManager _userManager;
        private readonly IMapper _mapper;

        public AppGroupController(
            IAppGroupService appGroupService,
            IAppRoleService appRoleService,
            ApplicationUserManager userManager,
            IMapper mapper
            )
        {
            _appGroupService = appGroupService;
            _appRoleService = appRoleService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Route("allPage")]
        [HttpGet]
        //[Authorize(Roles = nameof(PermissionProvider.ManageGroup))]
        public HttpResponseMessage allPage(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _appGroupService.GetAll(page, pageSize, out totalRow, filter);
                IEnumerable<AppGroupVm> modelVm = _mapper.Map<IEnumerable<AppGroup>, IEnumerable<AppGroupVm>>(model);

                PaginationSet<AppGroupVm> pagedSet = new PaginationSet<AppGroupVm>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = modelVm
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [Route("All")]
        [HttpGet]
        //[Authorize(Roles = nameof(PermissionProvider.ManageGroup))]
        public HttpResponseMessage All(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _appGroupService.GetAll();
                IEnumerable<AppGroupVm> modelVm = _mapper.Map<IEnumerable<AppGroup>, IEnumerable<AppGroupVm>>(model);

                response = request.CreateResponse(HttpStatusCode.OK, modelVm);

                return response;
            });
        }

        [Route("detail/{id:int}")]
        [HttpGet]
        //[Authorize(Roles = nameof(PermissionProvider.ManageGroup))]
        public HttpResponseMessage Detail(HttpRequestMessage request, int id)
        {
            if (id == 0)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " is required.");
            }
            AppGroup appGroup = _appGroupService.GetDetail(id);
            var appGroupViewModel = _mapper.Map<AppGroup, AppGroupVm>(appGroup);
            if (appGroup == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "No group");
            }
            var listRole = _appRoleService.GetListRoleByGroupId(appGroupViewModel.Id);
            appGroupViewModel.Roles = _mapper.Map<IEnumerable<AppRole>, IEnumerable<AppRoleVm>>(listRole);
            return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel);
        }

        [Route("create")]
        [HttpPost]
        //[Authorize(Roles = nameof(PermissionProvider.ManageGroup))]
        public HttpResponseMessage Create(HttpRequestMessage request, AppGroupVm appGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppGroup = new AppGroup();
                newAppGroup.Name = appGroupViewModel.Name;
                try
                {
                    var appGroup = _appGroupService.Insert(newAppGroup);

                    var listRoleGroup = new List<AppRoleGroup>();
                    foreach (var role in appGroupViewModel.Roles)
                    {
                        listRoleGroup.Add(new AppRoleGroup()
                        {
                            GroupId = appGroup.Id,
                            RoleId = role.Id
                        });
                    }
                    _appRoleService.InsertRolesToGroup(listRoleGroup, appGroup.Id);

                    return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel);
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

        [HttpPut]
        [Route("update")]
        //[Authorize(Roles = nameof(PermissionProvider.ManageGroup))]
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request, AppGroupVm appGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var appGroup = _appGroupService.GetDetail(appGroupViewModel.Id);
                try
                {
                    appGroup.Id = appGroupViewModel.Id;
                    appGroup.Name = appGroupViewModel.Name;
                    _appGroupService.Update(appGroup);
                    //save group
                    var listRoleGroup = new List<AppRoleGroup>();
                    foreach (var role in appGroupViewModel.Roles)
                    {
                        listRoleGroup.Add(new AppRoleGroup()
                        {
                            GroupId = appGroup.Id,
                            RoleId = role.Id
                        });
                    }
                    _appRoleService.InsertRolesToGroup(listRoleGroup, appGroup.Id);

                    //add role to user
                    var listRole = _appRoleService.GetListRoleByGroupId(appGroup.Id);
                    var listUserInGroup = _appGroupService.GetListUserByGroupId(appGroup.Id);
                    foreach (var user in listUserInGroup)
                    {
                        var listRoleName = listRole.Select(x => x.Name).ToArray();
                        foreach (var roleName in listRoleName)
                        {
                            await _userManager.RemoveFromRoleAsync(user.Id, roleName);
                            await _userManager.AddToRoleAsync(user.Id, roleName);
                        }
                    }
                    return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel);
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

        [HttpDelete]
        [Route("delete")]
        //[Authorize(Roles = nameof(PermissionProvider.ManageGroup))]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var appGroup = _appGroupService.Delete(id);
            return request.CreateResponse(HttpStatusCode.OK, appGroup);
        }

        [Route("deletemulti")]
        [HttpDelete]
        //[Authorize(Roles = nameof(PermissionProvider.ManageGroup))]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedList)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listItem = new JavaScriptSerializer().Deserialize<List<int>>(checkedList);
                    foreach (var item in listItem)
                    {
                        _appGroupService.Delete(item);
                    }
                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }
    }
}
