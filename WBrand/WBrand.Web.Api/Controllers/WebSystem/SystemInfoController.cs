using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WBrand.Core.Domain.Entities.System;
using WBrand.Services.WebSystem;

namespace WBrand.Web.Api.Controllers.WebSystem
{
    [RoutePrefix("api/systemInfo")]
    public class SystemInfoController : BaseApiController
    {
        ISystemInfoService _systemInfoService;
        public SystemInfoController(ISystemInfoService systemInfoService)
        {
            _systemInfoService = systemInfoService;
        }


        public SystemInfo Get()
        {
            return _systemInfoService.GetByFirst();
        }

        
        // PUT api/<controller>/5
        [HttpPut]
        [Route("Put")]
        public SystemInfo Put([FromBody]SystemInfo value)
        {
            //var test = value;

            return _systemInfoService.Update(value);
        }
    }
}
