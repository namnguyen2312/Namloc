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
    public class VideoController : BaseApiController
    {
        IVideoService _videoService;
        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Video> GetAll()
        {
            return _videoService.GetAll();
        }


        public Video Get(int id)
        {
            return _videoService.GetById(id);
        }

        // POST api/<controller>
        public Video Add([FromBody]Video value)
        {
            return _videoService.Insert(value);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Put")]
        public Video Put([FromBody]Video value)
        {
            //var test = value;

            return _videoService.Update(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(int id)
        {
            _videoService.DeleteById(id);
        }
    }
}
