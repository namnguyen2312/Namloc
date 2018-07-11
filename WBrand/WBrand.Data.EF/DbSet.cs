﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Core.Domain.Entities.System;

namespace WBrand.Data.EF
{
    public sealed partial class WBrandDbContext
    {
        public DbSet<LogDbContext> LogDbContexts { set; get; }

        /// <summary>
        /// Identity table
        /// </summary>
        public DbSet<AppUserGroup> AppUserGroups { set; get; }
        public DbSet<AppRoleGroup> AppRoleGroups { set; get; }
        public DbSet<AppRole> AppRoles { set; get; }
        public DbSet<AppGroup> AppGroups { set; get; }
    }
}
