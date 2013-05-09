using System;
using System.Linq;
using System.Collections.Generic;
using RCSoft.Services.Products;
using RCSoft.Services.Localization;
using RCSoft.Web.Models.Products;
using Telerik.Web.Mvc;
using System.Web.Mvc;

namespace RCSoft.Web.Controllers
{
    public class CategoryController : BaseController
    {
        #region 字段
        private readonly ICategoryService _categoryService;
        private readonly ILocalizationService _localizationService;
        #endregion

        #region 构造函数
        public CategoryController(ICategoryService categoryService, ILocalizationService localizationService)
        {
            this._categoryService = categoryService;
            this._localizationService = localizationService;
        }
        #endregion

        #region 方法
        public ActionResult List()
        {
            var model = new CategoryListModel();
            var categories = _categoryService.GetAllCategories(null, 0, 10, true);
            model.Categories = new GridModel<CategoryModel>
            {
                Data = categories.Select(c =>
                {
                    var categoryModel = c.ToModel();
                    categoryModel.ParentCategoryName = c.ParentCategoryId == 0 ? "" : _categoryService.GetCategoryById(c.ParentCategoryId).Name;
                    return categoryModel;
                }),

                Total = categories.TotalCount
            };
            return View(model);
        }
        #endregion
    }
}