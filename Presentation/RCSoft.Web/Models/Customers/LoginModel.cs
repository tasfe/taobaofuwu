using RCSoft.Web.Framework.Mvc;
using RCSoft.Web.Framework;
using System.ComponentModel.DataAnnotations;

namespace RCSoft.Web.Models.Customers
{
    public partial class LoginModel : BaseModel
    {
        [RCSoftResourceDisplayName("Account.Login.Fields.Email")]
        public string Email { get; set; }

        public bool UsernameEnabled { get; set; }

        [RCSoftResourceDisplayName("Account.Login.Fields.UserName")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [RCSoftResourceDisplayName("Account.Login.Fields.Password")]
        public string Password { get; set; }

        public bool DisplayCaptcha { get; set; }

        [RCSoftResourceDisplayName("Account.Login.Fields.RememberMe")]
        public bool RememberMe { get; set; }
    }
}