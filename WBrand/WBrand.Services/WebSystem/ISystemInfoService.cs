using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.System;

namespace WBrand.Services.WebSystem
{
    public interface ISystemInfoService: IBaseServices<SystemInfo>
    {
        SystemInfo GetByFirst();
    }
}
