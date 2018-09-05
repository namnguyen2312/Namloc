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
    [RoutePrefix("api/banner")]
    public class BannerController : BaseApiController
    {
        IBannerService _bannerService;

        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Banner> GetAll()
        {
            return _bannerService.GetAll();
        }

       
        public Banner Get(int id)
        {
            return _bannerService.GetById(id);
        }

        // POST api/<controller>
        public Banner Add([FromBody]Banner value)
        {
            return _bannerService.Insert(value);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Put")]
        public Banner Put([FromBody]Banner value)
        {
            //var test = value;

            return _bannerService.Update(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(int id)
        {
            _bannerService.DeleteById(id);
        }
    }
}