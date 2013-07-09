using System.Linq;
using System.Web.Mvc;
using RCSoft.Services.Customers;
using Telerik.Web.Mvc;
using RCSoft.Web.Models.Customers;
using System.Collections.Generic;
using Telerik.Web.Mvc.UI;
using RCSoft.Core.Domain.Customers;
using RCSoft.Web.Framework.Controllers;
using RCSoft.Services.Localization;
using System;

namespace RCSoft.Web.Controllers
{
    [UserAuthorize]
    public class CustomerRoleController : BaseController
    {
        #region 字段
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        #endregion

        #region 构造函数
        public CustomerRoleController(ICustomerService customerService,ILocalizationService localizationService)
        {
            this._customerService = customerService;
            this._localizationService = localizationService;
        }
        #endregion

        #region AJAX
        public ActionResult AllCustomerRoles(string text, int selectedId)
        {
            var roles = _customerService.GetAllCustomerRoles(true);
            roles.Insert(0, new CustomerRole { Name = "[根目录]", Id = 0 });
            var selectList = new List<SelectListItem>();
            foreach (var r in roles)
            {
                selectList.Add(new SelectListItem()
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name,
                        Selected = r.Id == selectedId
                    });
            }

            return new JsonResult { Data = selectList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region 角色
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new CustomerRoleListModel();
            var customerRoles = _customerService.GetAllCustomerRoles(null, 0, 10, true);
            model.CustomerRoles = new GridModel<CustomerRoleModel>
            {
                Data = customerRoles.Select(x => {
                    var customerRoleModel = x.ToModel();
                    customerRoleModel.ParentRoleName = x.ParentRoleId == 0 ? "" : _customerService.GetCustomerRoleById(x.ParentRoleId).Name;
                    return customerRoleModel;
                }),
                
                Total = customerRoles.TotalCount
            };
            return View(model);
        }

        [HttpPost, GridAction(EnableCustomBinding = true)]
        public ActionResult List(GridCommand command, CustomerRoleListModel model)
        {
            var roles = _customerService.GetAllCustomerRoles(model.SearchRoleName, command.Page - 1, command.PageSize, true);
            var gridModel = new GridModel<CustomerRoleModel>
            {
                Data = roles.Select(x =>
                {
                    var roleModel = x.ToModel();
                    roleModel.ParentRoleName = x.ParentRoleId == 0 ? "" : _customerService.GetCustomerRoleById(x.ParentRoleId).Name;
                    return roleModel;
                }),
                Total = roles.TotalCount
            };
            return new JsonResult
            {
                Data = gridModel
            };
        }

        public ActionResult Create()
        {
            var model = new CustomerRoleModel();
            model.Active = true;
            //父角色
            model.ParentRoles = new List<DropDownItem> { new DropDownItem { Text = "[根目录]", Value = "0" } };
            if (model.ParentRoleId > 0)
            {
                var parentRole = _customerService.GetCustomerRoleById(model.ParentRoleId);
                if (parentRole != null)
                    model.ParentRoles.Add(new DropDownItem { Text = parentRole.Name, Value = parentRole.Id.ToString() });
                else
                    model.ParentRoleId = 0;
            }
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormNameAttribute("save-continue", "continueEditing")]
        public ActionResult Create(CustomerRoleModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var customerRole = model.ToEntity();
                _customerService.InsertCustomerRole(customerRole);

                SuccessNotification(_localizationService.GetResource("Customers.CustomerRoles.Added"));
                return continueEditing ? RedirectToAction("Edit", new { id = customerRole.Id }) : RedirectToAction("List");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var cutomerRole = _customerService.GetCustomerRoleById(id);
            if (cutomerRole == null)
                return RedirectToAction("List");

            var model = cutomerRole.ToModel();
            model.ParentRoles = new List<DropDownItem> { new DropDownItem { Text = "[根目录]", Value = "0" } };
            if (model.ParentRoleId > 0)
            {
                var parentCategory = _customerService.GetCustomerRoleById(model.ParentRoleId);
                if (parentCategory != null)
                    model.ParentRoles.Add(new DropDownItem { Text = parentCategory.Name, Value = parentCategory.Id.ToString() });
                else
                    model.ParentRoleId = 0;
            }
            return View(model);
        }
        [HttpPost, ParameterBasedOnFormNameAttribute("save-continue", "continueEditing")]
        public ActionResult Edit(CustomerRoleModel model, bool continueEditing)
        {
            var customerRole = _customerService.GetCustomerRoleById(model.Id);
            if (customerRole == null)
                return RedirectToAction("List");
            try
            {
                if (ModelState.IsValid)
                {
                    customerRole = model.ToEntity(customerRole);
                    _customerService.UpdateCustomerRole(customerRole);
                    return continueEditing ? RedirectToAction("Edit", customerRole.Id) : RedirectToAction("List");
                }
                return View(model);
            }
            catch (Exception exc)
            {
                ErrorNotifacation(exc);
                return RedirectToAction("Edit", new { id = customerRole.Id });
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var customerRole = _customerService.GetCustomerRoleById(id);
            if (customerRole == null)
                return RedirectToAction("List");
            try
            {
                _customerService.DeleteCustomerRole(customerRole);
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc.Message);
                return RedirectToAction("Edit", new { id = customerRole.Id });
            }
        }
        #endregion
    }
}