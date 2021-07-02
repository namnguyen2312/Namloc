using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WBrand.Common.Constants;
using WBrand.Core.Domain.Entities.Identity;

namespace WBrand.Web.Provider
{
    public class CustomAccessTokenProvider : AuthenticationTokenProvider
    {
        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
            var expired = context.Ticket.Properties.ExpiresUtc < DateTime.UtcNow;
            if (expired)
            {
                //If current token is expired, set a custom response header
                context.Response.Headers.Add("X-AccessTokenExpired", new string[] { "1" });
            }

            base.Receive(context);
        }
    }
    public class AuthorizationServerProvider: OAuthAuthorizationServerProvider
    {
        public AuthorizationServerProvider()
        {
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            await Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            UserManager<AppUser> userManager = context.OwinContext.GetUserManager<UserManager<AppUser>>();
            AppUser user;
            try
            {
                user = await userManager.FindAsync(context.UserName, context.Password);
            }
            catch
            {
                // Could not retrieve the user due to error.
                context.SetError("server_error", "Lỗi trong quá trình xử lý.");
                context.Rejected();
                return;
            }
            if (user != null)
            {
                try
                {
                    var roles = userManager.GetRoles(user.Id);
                    ClaimsIdentity identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalBearer);
                    string email = string.IsNullOrEmpty(user.Email) ? "" : user.Email;
                    string expireTime = DateTimeOffset.UtcNow.AddMinutes(SystemConstants.ExpireTime).ToString("yyyy-MM-dd HH:mm:ss tt %K");
                    identity.AddClaim(new Claim("fullName", user.FullName ?? ""));
                    identity.AddClaim(new Claim("email", email));
                    identity.AddClaim(new Claim("userName", user.UserName));
                    //identity.AddClaim(new Claim("roles", JsonConvert.SerializeObject(roles)));
                    identity.AddClaim(new Claim("expireTime", expireTime));
                    var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        {"fullName", user.FullName ?? ""},
                        {"email", email},
                        {"userName", user.UserName},
                        //{"roles",JsonConvert.SerializeObject(roles) },
                        {"expireTime", expireTime }
                    });
                    var authenticationTicket = new AuthenticationTicket(identity, props);
                    context.Validated(authenticationTicket);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {
                context.SetError("invalid_grant", "Tài khoản hoặc mật khẩu không đúng.");
                context.Rejected();
            }
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}