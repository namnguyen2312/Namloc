using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WBrand.Common.Extensions;
using WBrand.Web.Controllers.Extend;

namespace WBrand.Web.Controllers
{
    public class ImageController : Controller
    {
        [OutputCache(Duration = 3600, VaryByParam = "filename, h, w")]
        public ActionResult Thumbnail(string filename, int h, int w)
        {
            var uri = new Uri(Request.Url.AbsoluteUri);
            var originUrl = uri.GetLeftPart(UriPartial.Authority);

            var partialName = $"~{new Uri(originUrl + filename).PathAndQuery}";
            using (Image image = Image.FromFile(Server.MapPath(partialName)))
            {
                return new ImageResult(image.ScaleFix(h, w));
            }
        }
    }
}