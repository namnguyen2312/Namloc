using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Data.EF;

namespace WBrand.Services.Facade.Identity
{
    public class ApplicationUserStore : UserStore<AppUser>
    {
        public ApplicationUserStore(WBrandDbContext context)
               : base(context)
        {

        }
    }
}
