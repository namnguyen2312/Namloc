using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WBrand.Core.Domain.Entities.Blog;
using WBrand.Core.Domain.Entities.Catalog;
using WBrand.Core.Domain.Entities.SlideShow;
using WBrand.Core.Domain.Entities.System;

namespace WBrand.Web.Kernel.ViewModel
{
    public class HomeVm
    {
        public HomeVm()
        {
            
        }
        public IEnumerable<Banner> Banners { set; get; }

        public IEnumerable<SlideShow> SlideShows { set; get; }

        public IEnumerable<Video> Videos { set; get; }

        public IEnumerable<BlogPost> BlogPosts { set; get; }

        public IEnumerable<Product> Products { set; get; }
    }
}