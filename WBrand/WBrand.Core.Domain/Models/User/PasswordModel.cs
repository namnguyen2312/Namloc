using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Models.User
{
    public class PasswordModel
    {
        public string PasswordCurrent { set; get; }
        public string PasswordNew { set; get; }
    }
}
