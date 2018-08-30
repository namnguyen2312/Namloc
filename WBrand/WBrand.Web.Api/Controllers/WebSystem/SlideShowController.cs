using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WBrand.Core.Domain.Entities.SlideShow;
using WBrand.Core.Domain.Enum;
using WBrand.Services.WebSystem;

namespace WBrand.Web.Api.Controllers.WebSystem
{
    public class SlideShowController : BaseApiController
    {
        ISlideShowService _slideShowService;
        public SlideShowController(ISlideShowService slideShowService)
        {
            _slideShowService = slideShowService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<SlideShow> GetAll(SlideShowPosition position)
        {
            return _slideShowService.GetAll(position);
        }


        public SlideShow Get(int id)
        {
            return _slideShowService.GetById(id);
        }

        // POST api/<controller>
        public SlideShow Add([FromBody]SlideShow value)
        {
            return _slideShowService.Insert(value);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Put")]
        public SlideShow Put([FromBody]SlideShow value)
        {
            //var test = value;

            return _slideShowService.Update(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(int id)
        {
            _slideShowService.DeleteById(id);
        }
    }
}
