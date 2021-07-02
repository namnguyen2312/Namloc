using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Identity;

namespace WBrand.Core.Domain.Models.User
{
    public class AppUserModel
    {
        public string Id { set; get; }
        public string UserName { set; get; }
        public string FullName { set; get; }
        public string PhoneNumber { set; get; }
        public string Address { set; get; }
        public DateTimeOffset BirthDay { set; get; }
        public int UserType { set; get; }
        public bool Gender { set; get; }
        public Status State { set; get; }
        public DateTimeOffset CreatedDate { set; get; }
        public DateTimeOffset UpdatedDate { set; get; }
        public string CreatedBy { set; get; }
        public string UpdatedBy { set; get; }
        public IEnumerable<AppGroupModel> Groups { set; get; }
    }
}
