using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WBrand.Common.Constants;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Services.Facade.Identity;
using WBrand.Web.Kernel.ViewModel;
using WBrand.Web.Kernel.ViewModel.User;

namespace WBrand.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        public AdminController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        [ActionName("system")]
        public ActionResult Index()
        {
            HttpCookie tokenCookie;
            if (Request.Cookies[SystemConstants.TokenApp] != null)
            {
                tokenCookie = Request.Cookies[SystemConstants.TokenApp];

                var token = JsonConvert.DeserializeObject<TokenVm>(tokenCookie.Value);
                if (token.access_token == null)
                {
                    tokenCookie.Expires = DateTime.Now.AddDays(-365);
                    Response.Cookies.Add(tokenCookie);
                    return RedirectToAction("login-cms");
                }
                DateTimeOffset time = DateTimeOffset.Parse(token.expireTime);
                if (time < DateTimeOffset.UtcNow)
                {
                    tokenCookie.Expires = DateTime.Now.AddDays(-365);
                    Response.Cookies.Add(tokenCookie);
                    return RedirectToAction("login-cms");
                }
            }
            else
            {
                return RedirectToAction("login-cms");
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Route("login-cms")]
        [ActionName("login-cms")]
        public async Task<ActionResult> Login(LoginVm model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    if (user.UserType == 0)
                    {
                        await this.GetToken(model);
                        Session["IsAuthorized"] = true;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Admin");
                    }
                }
                else
                {
                    ModelState.AddModelError("MsgError", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            return View("Login", model);
        }

        [ActionName("login-cms")]
        public async Task<ActionResult> Login()
        {
            HttpCookie tokenCookie;
            if (Request.Cookies[SystemConstants.TokenApp] != null)
            {
                tokenCookie = Request.Cookies[SystemConstants.TokenApp];
                tokenCookie.Expires = DateTime.Now.AddDays(-365);
                Response.Cookies.Add(tokenCookie);
            }
            await _userManager.SeedUser();
            return View("Login");
        }

        private async Task GetToken(LoginVm model)
        {
            String strPathAndQuery = System.Web.HttpContext.Current.Request.Url.PathAndQuery;
            String baseUrl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
            baseUrl = baseUrl + "/api/oauth/token";

            using (var client = new HttpClient())
            {
                var form = new Dictionary<string, string>
                    {
                           {"grant_type", "password"},
                           {"username", model.UserName},
                           {"password", model.Password},
                    };
                var tokenResponse = await client.PostAsync(baseUrl, new FormUrlEncodedContent(form));

                var token = await tokenResponse.Content.ReadAsAsync<TokenVm>();

                HttpCookie tokenCookie;
                if (Request.Cookies[SystemConstants.TokenApp] != null)
                    tokenCookie = Request.Cookies[SystemConstants.TokenApp];
                else
                    tokenCookie = new HttpCookie(SystemConstants.TokenApp);
                tokenCookie.Expires = DateTime.Now.AddDays(1);
                tokenCookie.Value = JsonConvert.SerializeObject(token);
                Response.Cookies.Add(tokenCookie);
            }
        }
    }
}