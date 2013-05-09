using System;
using RCSoft.Web.Models.Products;
using FluentValidation;
using RCSoft.Services.Localization;

namespace RCSoft.Web.Validators.Products
{
    public class CategoryValidator:AbstractValidator<CategoryModel>
    {
        public CategoryValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotNull().WithMessage(localizationService.GetResource("Products.Catalog.Fileds.Name.Required"));
        }
    }
}