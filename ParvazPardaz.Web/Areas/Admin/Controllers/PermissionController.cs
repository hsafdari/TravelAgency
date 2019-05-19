using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using ParvazPardaz.ViewModel;
using Infrastructure;
using ParvazPardaz.Model;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.AccessLevel;
using System.Reflection;
using ParvazPardaz.Model.Entity.AccessLevel;
using ParvazPardaz.Model.Entity.Users;
using Microsoft.AspNet.Identity;
using ParvazPardaz.Service.Contract.Users;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using DataTables.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    [Mvc5AuthorizeAttribute(StandardRoles.SystemAdministrator)]
    public class PermissionController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionService _permissionService;
        private IApplicationUserManager _userManager { get; set; }
        #endregion

        #region	Ctor
        public PermissionController(IUnitOfWork unitOfWork, IPermissionService permissionService, IApplicationUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _permissionService = permissionService;
            _userManager = userManager;
        }
        #endregion

        #region Index
        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;

            #region newUser._roles
            var loggedInUserId = System.Convert.ToInt32(User.Identity.GetUserId());
            var loggedInUser = _userManager.FindUserById(loggedInUserId);
            var systemAdministrator = _unitOfWork.Set<Role>().FirstOrDefault(x => x.Name.Equals("SystemAdministrator"));
            if (!loggedInUser.Roles.Any(x => x.RoleId == systemAdministrator.Id))
            {
                ViewBag.Roles = _unitOfWork.Set<Role>().Where(r => r.IsBanned == false && !r.Name.Equals("SystemAdministrator"))
                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            }
            else
            {
                ViewBag.Roles = _unitOfWork.Set<Role>().Where(r => r.IsBanned == false)
                     .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            }
            #endregion
            return View();
        }
        public ActionResult GetPermission([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, int RoleId)
        {
            string _namespace = "ParvazPardaz.Web.Areas.Admin.Controllers";
            var data = GetPageUrlDDL(_namespace, RoleId);
            return Json(new DataTablesResponse(requestModel.Draw, data, 0, data.Count), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create

        public ActionResult Create(string msg)
        {
            ViewBag.success = msg;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddPermissionViewModel addPermissionViewModel)
        {
            if (ModelState.IsValid)
            {
                var newPermission = await _permissionService.CreateAsync<AddPermissionViewModel>(addPermissionViewModel);
                //return RedirectToAction("Create", new { msg = "create" });
                return Json(true);
            }
            //return View(addPermissionViewModel);
            return Json(false);
        }
        #endregion

        #region Edit

        public async Task<ActionResult> Edit(int id)
        {
            EditPermissionViewModel editPermissionViewModel = await _permissionService.GetViewModelAsync<EditPermissionViewModel>(x => x.ID == id);
            return View(editPermissionViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditPermissionViewModel editPermissionViewModel)
        {
            if (ModelState.IsValid)
            {
                var update = await _permissionService.UpdateAsync<EditPermissionViewModel>(editPermissionViewModel, t => t.Id == editPermissionViewModel.ID);
                //return RedirectToAction("Index", new { msg = "update" });
                return Json(true);
            }
            //return View(editPermissionViewModel);
            return Json(false);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var deletedId = await _permissionService.DeleteAsync(x => x.Id == id);//.DeleteLogicallyAsync(x => x.ID == id);            
            if (deletedId > 0)//if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

        public List<ControlActionViewModel> GetPageUrlDDL(string _namespace)
        {

            var q = (from t in Assembly.GetExecutingAssembly().GetTypes()
                     where t.IsClass && t.Namespace == _namespace
                     where t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                     select t).ToList();

            List<ControlActionViewModel> ActionControllers = new List<ControlActionViewModel>();

            foreach (var item in q)
            {
                string currentController = item.Name.Remove(item.Name.Length - 10);

                var methods = item.GetMethods();

                foreach (var m in methods)
                {
                    var attribs = m.GetCustomAttributes();
                    foreach (var att in attribs)
                    {
                        AuthorizeAttribute attrib = att as AuthorizeAttribute;

                        if (attrib != null)
                        {
                            ControlActionViewModel actionitem = new ControlActionViewModel();
                            var custo = attrib.GetType().GetNestedType("CustomAuthorizeAttribute");
                            actionitem.Controller = currentController;
                            var value = (Permissionitem[])attrib.GetType().GetProperty("_permissions").GetValue(attrib);
                            actionitem.Permission = value.Select(x => x.ToString()).FirstOrDefault();
                            actionitem.Method = "/Admin/" + currentController + "/" + m.Name;

                            string auth = attrib.ToString();
                            ActionControllers.Add(actionitem);
                        }
                    }
                }
            }

            var controllerLists = _unitOfWork.Set<ControllersList>().ToList();
            //var items = (from c in ActionControllers
            //             where !controllerLists.Any(x => x.PageUrl == c.Method)
            //             select c).Distinct().ToList();
            //ActionControllers = items;
            foreach (var item in ActionControllers)
            {
                if (controllerLists.Any(x => x.PageUrl == item.Method))
                {
                    item.Selected = true;
                }
                else
                    item.Selected = false;
            }
            return ActionControllers;
        }
        private List<ControlActionViewModel> GetPageUrlDDL(string _namespace, int RoleId)
        {
            var q = (from t in Assembly.GetExecutingAssembly().GetTypes()
                     where t.IsClass && t.Namespace == _namespace
                     where t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                     select t).ToList();

            List<ControlActionViewModel> ActionControllers = new List<ControlActionViewModel>();

            foreach (var item in q)
            {
                string currentController = item.Name.Remove(item.Name.Length - 10);

                var methods = item.GetMethods();

                foreach (var m in methods)
                {
                    var attribs = m.GetCustomAttributes();
                    ControlActionViewModel actionitem = new ControlActionViewModel();
                    foreach (var att in attribs)
                    {
                        CustomAuthorizeAttribute attrib = att as CustomAuthorizeAttribute;
                        DisplayAttribute disattrib = att as DisplayAttribute;

                        if (attrib != null)
                        {
                            actionitem.Controller = currentController;
                            var value = (Permissionitem[])attrib.GetType().GetProperty("_permissions").GetValue(attrib);
                            actionitem.Permission = value.Select(x => x.ToString()).FirstOrDefault();
                            actionitem.Method = "/Admin/" + currentController + "/" + m.Name;

                            string auth = attrib.ToString();


                        }
                        if (disattrib != null)
                        {
                            actionitem.Title = disattrib.GetName();
                        }
                    }
                    if (actionitem.Controller != null)
                    {
                        ActionControllers.Add(actionitem);
                    }
                }
            }

            var controllerLists = _unitOfWork.Set<ControllersList>().ToList();
            int i = 1;
            foreach (var item in ActionControllers)
            {
                item.Id = i++;
                if (controllerLists.Any(x => x.PageUrl == item.Method && x.RolePermissionControllers.Any(y => y.RoleId == RoleId)))
                {
                    item.Selected = true;
                }
                else
                    item.Selected = false;
            }
            return ActionControllers;
        }
        public ActionResult Change(int RoleId, string Url, bool access, string Permission)
        {
            _permissionService.SetAccess(RoleId, Url, access, Permission);
            return Json("true", JsonRequestBehavior.AllowGet);
        }

    }
}
