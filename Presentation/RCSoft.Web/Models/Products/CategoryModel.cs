using RCSoft.Web.Framework.Mvc;
using System.Collections.Generic;
using Telerik.Web.Mvc.UI;
using FluentValidation.Attributes;
using RCSoft.Web.Validators.Products;
using RCSoft.Web.Framework;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RCSoft.Web.Models.Products
{
    [Validator(typeof(CategoryValidator))]
    public partial class CategoryModel : BaseEntityModel
    {
        [RCSoftResourceDisplayName("Products.Catalog.Fileds.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [RCSoftResourceDisplayName("Products.Catalog.Fileds.ParentCategory")]
        [AllowHtml]
        public int ParentCategoryId { get; set; }

        [UIHint("Picture")]
        [RCSoftResourceDisplayName("Products.Catalog.Fileds.PictureUrl")]
        public string PictureUrl { get; set; }

        [RCSoftResourceDisplayName("Products.Catalog.Fileds.IsDeleted")]
        [AllowHtml]
        public bool IsDeleted { get; set; }

        [RCSoftResourceDisplayName("Products.Catalog.Fileds.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [RCSoftResourceDisplayName("Products.Catalog.Fileds.Description")]
        [AllowHtml]
        public string Description { get; set; }

        public IList<DropDownItem> ParentCategories { get; set; }

        public string ParentCategoryName { get; set; }
    }
}