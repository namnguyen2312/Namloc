using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WBrand.Web.Kernel.ViewModel
{
    public class TokenVm
    {
        public string access_token { set; get; }
        public string fullName { set; get; }
        public string email { set; get; }
        public string userName { set; get; }
        public string expireTime { set; get; }
    }
}