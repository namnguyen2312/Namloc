using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Data.EF;

namespace WBrand.Services.Facade.Identity
{
    public class ApplicationUserManager : UserManager<AppUser>
    {
        IUserStore<AppUser> _store;
        public ApplicationUserManager(IUserStore<AppUser> store)
              : base(store)
        {
            _store = store;
        }
        public async Task SeedUser()
        {
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(new WBrandDbContext()));

            if (manager.Users.Count() > 0)
                return;
            var user = new AppUser()
            {
                UserName = "admin",
                Email = "namloc@namlocalu.com",
                EmailConfirmed = true,
                State = Status.Active,
                CreatedDate = DateTimeOffset.UtcNow
                
            };
            
            await manager.CreateAsync(user,"123456");
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<AppUser>(context.Get<WBrandDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);

            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("Ok"));
            }
            
            return manager;
        }
    }
}
