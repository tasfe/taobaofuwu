using FluentValidation.Attributes;
using RCSoft.Web.Validators.Customers;
using RCSoft.Web.Framework.Mvc;
using System.Web.Mvc;
using RCSoft.Web.Framework;

namespace RCSoft.Web.Models.Customers
{
    [Validator(typeof(CustomerRoleValidator))]
    public partial class CustomerRoleModel : BaseEntityModel
    {
        [RCSoftResourceDisplayName("Customers.CustomerRole.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [RCSoftResourceDisplayName("Customers.CustomerRole.Fields.ParentRoleId")]
        [AllowHtml]
        public int ParentRoleId { get; set; }

        [RCSoftResourceDisplayName("Customers.CustomerRole.Fields.Active")]
        public bool Active { get; set; }

        [RCSoftResourceDisplayName("Customers.CustomerRole.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }
    }
}