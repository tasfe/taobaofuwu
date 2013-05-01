using System;
using RCSoft.Web.Models.Customers;
using FluentValidation;
using RCSoft.Services.Localization;

namespace RCSoft.Web.Validators.Customers
{
    public class CustomerRoleValidator : AbstractValidator<CustomerRoleModel>
    {
        public CustomerRoleValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotNull().WithMessage(localizationService.GetResource("Customers.CustomerRole.Fields.Name.Required"));
        }
    }
}
