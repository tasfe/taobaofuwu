using RCSoft.Web.Framework.Mvc;
using RCSoft.Web.Framework;
using System.Web.Mvc;
using Telerik.Web.Mvc;

namespace RCSoft.Web.Models.Customers
{
    public partial class CustomerRoleListModel : BaseModel
    {
        [RCSoftResourceDisplayName("Customers.CustomerRole.SearchRoleName")]
        [AllowHtml]
        public string SearchRoleName { get; set; }

        public GridModel<CustomerRoleModel> CustomerRoles { get; set; }
    }
}