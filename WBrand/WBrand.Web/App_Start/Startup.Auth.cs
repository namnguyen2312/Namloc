using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using WBrand.Common.Constants;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Data.EF;
using WBrand.Services.Facade.Identity;
using WBrand.Web.Provider;

[assembly: OwinStartup(typeof(WBrand.Web.App_Start.Startup))]

namespace WBrand.Web.App_Start
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(WBrandDbContext.Create);

            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<UserManager<AppUser>>(CreateManager);


            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/oauth/token"),
                Provider = new AuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(SystemConstants.ExpireTime),
                AllowInsecureHttp = true
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                AccessTokenProvider = new CustomAccessTokenProvider()
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);



        }

        private static UserManager<AppUser> CreateManager(IdentityFactoryOptions<UserManager<AppUser>> options, IOwinContext context)
        {
            var userStore = new UserStore<AppUser>(context.Get<WBrandDbContext>());
            var owinManager = new UserManager<AppUser>(userStore);

            return owinManager;
        }
    }
}
