using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.SlideShow;
using WBrand.Core.Domain.Enum;

namespace WBrand.Services.WebSystem
{
    public interface ISlideShowService : IBaseServices<SlideShow>
    {
        IEnumerable<SlideShow> GetAll(SlideShowPosition position = 0,bool? isPublish= null);

        IEnumerable<dynamic> GetAllPosition();

        void DeleteById(int id);
    }
}
