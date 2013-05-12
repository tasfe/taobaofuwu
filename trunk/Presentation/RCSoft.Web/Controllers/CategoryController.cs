using System;
using System.Linq;
using System.Collections.Generic;
using RCSoft.Services.Products;
using RCSoft.Services.Localization;
using RCSoft.Web.Models.Products;
using RCSoft.Core.Domain.Products;
using Telerik.Web.Mvc;
using System.Web.Mvc;
using Telerik.Web.Mvc.UI;
using RCSoft.Web.Framework.Controllers;

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

        #region Ajax
        public ActionResult AllCategories(string text, int selectedId)
        {
            var category = _categoryService.GetAllCategoriesByParentCategoryId(0);
            category.Insert(0, new Category { Name = "[根目录]", Id = 0 });
            var selectList = new List<SelectListItem>();
            foreach (var c in category)
            {
                selectList.Add(new SelectListItem()
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name,
                        Selected = c.Id == selectedId
                    });
            }

            return new JsonResult { Data = selectList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
                    categoryModel.PictureUrl = string.IsNullOrEmpty(c.PictureUrl) ? "~/Content/images/noDefaultImage.gif" : c.PictureUrl;
                    return categoryModel;
                }),

                Total = categories.TotalCount
            };
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new CategoryModel();
            model.IsDeleted = false;
            //父角色
            model.ParentCategories = new List<DropDownItem> { new DropDownItem { Text = "[根目录]", Value = "0" } };
            if (model.ParentCategoryId > 0)
            {
                var parentCategroy = _categoryService.GetCategoryById(model.ParentCategoryId);
                if (parentCategroy != null)
                    model.ParentCategories.Add(new DropDownItem { Text = parentCategroy.Name, Value = parentCategroy.Id.ToString() });
                else
                    model.ParentCategoryId = 0;
            }
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormNameAttribute("save-continue", "continueEditing")]
        public ActionResult Create(CategoryModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var category = model.ToEntity();
                category.CreateDate = DateTime.Now;
                category.UpdateDate = DateTime.Now;
                _categoryService.InsertCategory(category);
                SuccessNotification(_localizationService.GetResource("Customers.CustomerRoles.Added"));
                return continueEditing ? RedirectToAction("Edit", new { id = category.Id }) : RedirectToAction("List");
            }
            return View(model);
        }
        #endregion
    }
}