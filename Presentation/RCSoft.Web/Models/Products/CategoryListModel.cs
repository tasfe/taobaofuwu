using RCSoft.Web.Framework;
using System.Web.Mvc;
using Telerik.Web.Mvc;
using RCSoft.Web.Models.Products;

namespace RCSoft.Web.Models.Products
{
    public class CategoryListModel
    {
        public GridModel<CategoryModel> Categories { get; set; }
    }
}