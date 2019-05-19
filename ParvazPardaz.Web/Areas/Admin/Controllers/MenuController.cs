using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParvazPardaz.Model;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Model.Entity.Core;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Service.Contract.Core;
using Infrastructure;


namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
     [Mvc5AuthorizeAttribute(StandardRoles.PanelUser, StandardRoles.SystemAdministrator)]
    public class MenuController : Controller
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        #endregion

        #region Ctor
        public MenuController(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }
        #endregion
        //
        // GET: /Menu/
        public ActionResult Index(int id, string message)
        {
            ViewBag.success = message;
            ViewBag.GroupId = id;
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "_self", Text = ParvazPardaz.Resource.CMS.CMS.TargetSelf });
            list.Add(new SelectListItem() { Value = "_blank", Text = ParvazPardaz.Resource.CMS.CMS.TargetBlank });
            SelectList selectitems = new SelectList(list);
            ViewData["Target"] = new SelectList(list, "Value", "Text");          
            return View();
        }

        
        public ActionResult GetMenuItems(int? Id, int MenugroupId)
        {
            var treeListView = _unitOfWork.Set<Menu>().Where(mn => Id.HasValue ? mn.MenuParentId == Id : mn.MenuParentId == null && mn.MenuGroupId == MenugroupId && mn.IsDeleted == false);
            var TreeList = treeListView.Select(tree => new MenuTreeViewModel
            {
                Haschildren = tree.MenuChilds.Where(x=>x.IsDeleted==false).Any(),
                Id = tree.Id,
                NodeText = tree.MenuTitle
            }).ToList();
            return Json(TreeList, JsonRequestBehavior.AllowGet);
        }
        // POST: /menu/Create
        [HttpPost]
        public ActionResult Create(Menu menu, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    string filename = Guid.NewGuid().ToString().Replace("-", "");
                    if (Image != null && Image.ContentLength > 0)
                    {
                        string reletivePath = "/Uploads/Menu/";
                        string filesextension = System.IO.Path.GetExtension(Image.FileName);
                        menu.Image = reletivePath + filename + filesextension;
                        var mypath = System.IO.Path.Combine(Server.MapPath(reletivePath), filename + filesextension);

                        if (!System.IO.Directory.Exists(Server.MapPath(reletivePath)))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(reletivePath));
                        }

                        Image.SaveAs(mypath);
                    }
                }

                menu.CreatorDateTime = DateTime.Now;
                //menu.CreatorUserId = UserTransactions.GetUserLoginedId();
                //menu.CreatorUserIpAddress = UserTransactions.GetIpdAddress();
                _unitOfWork.Set<Menu>().Add(menu);
                _unitOfWork.SaveAllChanges();

                //مقداردهی فایل مرتبط با کش صفحه اول
                _cacheService.HomeCacheFileSetCurrentTime();

                return Json("Success", JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index", new { id = menu.MenuGroupId, message = "create" });
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index", new { id = menu.Id, message = "warning" });
            }

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Menu menuIems = _unitOfWork.Set<Menu>().FirstOrDefault(x => x.Id == id);
            if (menuIems != null)
            {
                if (menuIems.MenuChilds != null && menuIems.MenuChilds.Any())
                {
                    var childList = menuIems.MenuChilds.ToList();
                    foreach (var child in childList)
                    {
                        Delete(child.Id);
                        //menuIems.MenuChilds.Remove(child);
                        menuIems.MenuChilds.FirstOrDefault(x=>x.Id==child.Id).IsDeleted=true;
  
                    }
                }
                //_unitOfWork.Set<Menu>().Remove(menuIems);
                _unitOfWork.Set<Menu>().FirstOrDefault(x=>x.Id==menuIems.Id).IsDeleted=true;
                var isSaved = _unitOfWork.SaveAllChanges();

                //مقداردهی فایل مرتبط با کش صفحه اول
                _cacheService.HomeCacheFileSetCurrentTime();

                if (isSaved > 0)
                {
                    if (System.IO.File.Exists(Server.MapPath(menuIems.Image)))
                    {
                        System.IO.File.Delete(Server.MapPath(menuIems.Image));
                    }
                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                HttpNotFound();
            }
            Menu _menu = _unitOfWork.Set<Menu>().Find(id);

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "_self", Text = ParvazPardaz.Resource.CMS.CMS.TargetSelf });
            list.Add(new SelectListItem() { Value = "_blank", Text = ParvazPardaz.Resource.CMS.CMS.TargetBlank });
            SelectList selectitems = new SelectList(list);
            ViewData["Target"] = new SelectList(list, "Value", "Text", _menu.Target);

            if (_menu.MenuParentId == null)
            {
                ViewBag.Display = "block";
            }
            else
            {
                ViewBag.Display = "none";
                //_menu.MenuType = 0;
            }

            //List<SelectListItem> menuTypeList = new List<SelectListItem>(){
            //    new SelectListItem(){Text=@ParvazPardaz.Resource.CMS.CMS.Type_0, Value="Type_0"},
            //    new SelectListItem(){Text=@ParvazPardaz.Resource.CMS.CMS.Type_1, Value="Type_1"},
            //    new SelectListItem(){Text=@ParvazPardaz.Resource.CMS.CMS.Type_2, Value="Type_2"},
            //    new SelectListItem(){Text=@ParvazPardaz.Resource.CMS.CMS.Type_3, Value="Type_3"}
            //};
            ////string selectedValue = _menu.MenuType.ToString();
            //ViewData["MenuType"] = new SelectList(menuTypeList, "Value", "Text", _menu.MenuType);

            return View(_menu);
        }
        [HttpPost]
        public ActionResult Edit(Menu menu, HttpPostedFileBase Image)
        {
            var _menu = _unitOfWork.Set<Menu>().Find(menu.Id);
            if (_menu != null)
            {
                if (Image != null)
                {
                    string filename = Guid.NewGuid().ToString().Replace("-", "");
                    if (Image != null && Image.ContentLength > 0)
                    {
                        string reletivePath = "/Uploads/Menu/";
                        string filesextension = System.IO.Path.GetExtension(Image.FileName);
                        _menu.Image = reletivePath + filename + filesextension;
                        var mypath = System.IO.Path.Combine(Server.MapPath(reletivePath), filename + filesextension);

                        if (!System.IO.Directory.Exists(Server.MapPath(reletivePath)))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath(reletivePath));
                        }

                        Image.SaveAs(mypath);
                    }
                }
                _menu.ShortDescription = menu.ShortDescription;
                _menu.MenuType = menu.MenuType;
                _menu.ModifierDateTime = DateTime.Now;
                _menu.MenuIsActive = menu.MenuIsActive;
                _menu.MenuTitle = menu.MenuTitle;
                _menu.MenuUrl = menu.MenuUrl;
                _menu.OrderId = menu.OrderId;
                _menu.Target = menu.Target;
                //_menu.ModifierUserId = UserTransactions.GetUserLoginedId();
                //_menu.ModifierUserIpAddress = UserTransactions.GetIpdAddress();
                _unitOfWork.SaveAllChanges();

                //مقداردهی فایل مرتبط با کش صفحه اول
                _cacheService.HomeCacheFileSetCurrentTime();

                return RedirectToAction("Index", new { @id = _menu.MenuGroupId, message = "update" });
            }
            //List<SelectListItem> menuTypeList = new List<SelectListItem>(){
            //    new SelectListItem(){Text="", Value="0"},
            //    new SelectListItem(){Text=@ParvazPardaz.Resource.CMS.CMS.Type_1, Value="1"},
            //    new SelectListItem(){Text=@ParvazPardaz.Resource.CMS.CMS.Type_2, Value="2"},
            //    new SelectListItem(){Text=@ParvazPardaz.Resource.CMS.CMS.Type_3, Value="3"}
            //};
            //ViewData["MenuType"] = new SelectList(menuTypeList, "Value", "Text", _menu.MenuType);
            return View();
        }

    }
}
