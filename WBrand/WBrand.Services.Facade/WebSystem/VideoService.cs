using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.System;
using WBrand.Data;
using WBrand.Data.WebSystem;
using WBrand.Services.WebSystem;

namespace WBrand.Services.Facade.WebSystem
{
    public class VideoService : BaseServices<Video>, IVideoService
    {
        IVideoRepository _videoRepository;
        public VideoService(IVideoRepository videoRepository) : base(videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Video> GetAll(bool? isShow = null)
        {
            var query = _videoRepository.TableNoTracking;
            if (isShow != null)
                query = query.Where(x => x.IsShow == isShow);

            return query.OrderBy(x => x.Order).ToList();

        }
    }
}
