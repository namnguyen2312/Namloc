using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WBrand.Common.Page;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Services.User;
using WBrand.Web.Kernel.ViewModel.User;

namespace WBrand.Web.Api.Controllers.User
{
    [RoutePrefix("api/appRole")]
    public class AppRoleController : BaseApiController
    {
        IAppRoleService _appRoleService;
        private readonly IMapper _mapper;
        public AppRoleController(
             IAppRoleService appRoleService,
             IMapper mapper)
        {
            _appRoleService = appRoleService;
            _mapper = mapper;
        }
        [Route("allPage")]
        [HttpGet]
        [Authorize(Roles = nameof(Permission.ManagePermission))]
        public HttpResponseMessage AllPage(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                _appRoleService.InsertRoleFromSystem();
                int totalRow = 0;
                var model = _appRoleService.GetAll(page, pageSize, out totalRow, filter);
                IEnumerable<AppRoleVm> modelVm = _mapper.Map<IEnumerable<AppRole>, IEnumerable<AppRoleVm>>(model);

                PaginationSet<AppRoleVm> pagedSet = new PaginationSet<AppRoleVm>()
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

        [Route("all")]
        [HttpGet]
        [Authorize(Roles = nameof(Permission.ManagePermission))]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _appRoleService.GetAll();
                IEnumerable<AppRoleVm> modelVm = _mapper.Map<IEnumerable<AppRole>, IEnumerable<AppRoleVm>>(model);

                response = request.CreateResponse(HttpStatusCode.OK, modelVm);

                return response;
            });
        }
    }
}
