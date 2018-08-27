using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Blog;
using WBrand.Core.Domain.Entities.Catalog;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Core.Domain.Entities.System;

namespace WBrand.Data.EF
{
    public sealed partial class WBrandDbContext
    {
        /// <summary>
        /// Catalog
        /// </summary>
        public DbSet<Product> Products { set; get; }

        public DbSet<ProductAttribute> ProductAttributes { set; get; }

        public DbSet<ProductCategory> ProductCategories { set; get; }

        public DbSet<CatalogCategory> CatalogCategories { set; get; }

        public DbSet<CatalogAttribute> CatalogAttributes { set; get; }

        /// <summary>
        /// Blog
        /// </summary>
        public DbSet<BlogPost> BlogPosts { set; get; }

        public DbSet<BlogPostCategory> BlogPostCategories { set; get; }

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
