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
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Users;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Common.Controller;
using Microsoft.AspNet.Identity;
using ParvazPardaz.Service.Contract.Country;
using AutoMapper;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Enum;
using Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
      [Mvc5AuthorizeAttribute(StandardRoles.SystemAdministrator)]
    public class UserController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly ICityService _cityService;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;

        private const string RelativePath = "/Uploads/UserProfile/";
        private const int A5Width = 420;
        private const int A5Height = 595;
        #endregion

        #region	Ctor
        public UserController(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IApplicationRoleManager roleManager, ICityService cityService, IMappingEngine mappingEngine, HttpContextBase httpContextBase)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _cityService = cityService;
            _mappingEngine = mappingEngine;
            _httpContextBase = httpContextBase;
        }
        #endregion

        #region Index
        [CustomAuthorize(Permissionitem.List)]
        [Display(Name = "UserManagement", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]

        public ActionResult Index(string msg)
        {
            ViewBag.success = msg;
            return View();
        }

        public ActionResult GetUser([DataSourceRequest]DataSourceRequest request)
        {
            var query = _userManager.GetViewModelForGrid(UserProfileType.Personal);
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        public ActionResult Company(string msg)
        {
            ViewBag.success = msg;
            return View();
        }
        public ActionResult GetCompany([DataSourceRequest]DataSourceRequest request)
        {
            var query = _userManager.GetViewModelForGrid(UserProfileType.Company);
            var dataSourceResult = query.ToDataSourceResult(request);
            return Json(dataSourceResult);
        }
        #endregion

        #region Create
        [CustomAuthorize(Permissionitem.Create)]
        [Display(Name = "UserManagement", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]

        public ActionResult Create()
        {
            AddUserUserProfileViewModel newUser = new AddUserUserProfileViewModel();
            newUser.CityDDL = _cityService.GetAllCityOfSelectListItem();

            #region newUser._roles
            var loggedInUserId = System.Convert.ToInt32(User.Identity.GetUserId());
            var loggedInUser = _userManager.FindUserById(loggedInUserId);
            var systemAdministrator = _unitOfWork.Set<Role>().FirstOrDefault(x => x.Name.Equals("SystemAdministrator"));
            if (!loggedInUser.Roles.Any(x => x.RoleId == systemAdministrator.Id))
            {
                newUser._roles = _unitOfWork.Set<Role>().Where(r => r.IsBanned == false && !r.Name.Equals("SystemAdministrator")).ToList();
            }
            else
            {
                newUser._roles = _unitOfWork.Set<Role>().Where(r => r.IsBanned == false).ToList();
            } 
            #endregion

            return View(newUser);
        }


        [HttpPost]
        public async Task<ActionResult> Create(AddUserUserProfileViewModel viewModel)
        {
            #region viewModel._roles
            var loggedInUserId = System.Convert.ToInt32(User.Identity.GetUserId());
            var loggedInUser = _userManager.FindUserById(loggedInUserId);
            var systemAdministrator = _unitOfWork.Set<Role>().FirstOrDefault(x => x.Name.Equals("SystemAdministrator"));
            if (!loggedInUser.Roles.Any(x => x.RoleId == systemAdministrator.Id))
            {
                viewModel._roles = _unitOfWork.Set<Role>().Where(r => r.IsBanned == false && !r.Name.Equals("SystemAdministrator")).ToList();
            }
            else
            {
                viewModel._roles = _unitOfWork.Set<Role>().Where(r => r.IsBanned == false).ToList();
            }
            #endregion

            #region بررسی عدم وجود در سیستم
            var foundUserbyUserName = await _userManager.FindByNameAsync(viewModel.AddUser.UserName);
            if (foundUserbyUserName != null)
            {
                ModelState.AddModelError("UserName", ParvazPardaz.Resource.Validation.ValidationMessages.UserExist);
                viewModel.CityDDL = _cityService.GetAllCityOfSelectListItem();
                //viewModel._roles = _unitOfWork.Set<Role>().Where(r => r.IsBanned == false && r.Name != "SystemAdministrator").ToList();
                return View(viewModel);
            }
            #endregion

            if (ModelState.IsValid)
            {
                #region ایجاد کاربر
                var newUser = await _userManager.SignupAdminAsync(viewModel);
                #endregion

                if (newUser != null)
                {
                    var createdUser = _unitOfWork.Set<User>().FirstOrDefault(x => x.UserName.Equals(viewModel.AddUser.UserName));
                    if (createdUser != null)
                    {
                        #region Create profile
                        var newProfile = _mappingEngine.Map<UserProfile>(viewModel);

                        #region Saving Avatar Image
                        if (viewModel.File.HasFile())
                        {
                            string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                            string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
                            string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);

                            newProfile.AvatarUrl = relativePathWithFileNameOriginal;
                            newProfile.AvatarExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                            newProfile.AvatarFileName = viewModel.File.FileName;
                            newProfile.AvatarSize = viewModel.File.ContentLength;

                            if (!System.IO.Directory.Exists(_httpContextBase.Server.MapPath(RelativePath)))
                            {
                                var directory = System.IO.Directory.CreateDirectory(_httpContextBase.Server.MapPath(RelativePath));
                            }

                            //ذخیره فایل در سایز اصلی
                            byte[] buffer = new byte[viewModel.File.ContentLength];
                            await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                            System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameOriginal), buffer);
                            //ذخیره فایل در سایز کوچک
                            buffer = viewModel.File.InputStream.ResizeImageFile(A5Width, A5Height);
                            await viewModel.File.InputStream.ReadAsync(buffer, 0, viewModel.File.ContentLength);
                            System.IO.File.WriteAllBytes(_httpContextBase.Server.MapPath(relativePathWithFileNameThumb), buffer);
                        }
                        #endregion

                        #region Saving addresses
                        //if (viewModel.UserAddresses != null && viewModel.UserAddresses.Any())
                        //{
                        //    // افزودن آدرس های جدید   
                        //    foreach (var address in viewModel.UserAddresses)
                        //    {
                        //        var newAddress = _mappingEngine.Map<UserAddress>(address);
                        //        newProfile.UserAddresses.Add(newAddress);
                        //    }
                        //}
                        #endregion

                        newProfile.CreatorDateTime = DateTime.Now;
                        newProfile.CreatorUserId = System.Web.HttpContext.Current.Request.GetUserId();
                        newProfile.CreatorUserIpAddress = System.Web.HttpContext.Current.Request.GetIp();
                        createdUser.UserProfile = newProfile;

                        await _unitOfWork.SaveAllChangesAsync(false);
                        #endregion

                        return RedirectToAction("Index", new { msg = "create" });
                    }
                }
            }

            viewModel.CityDDL = _cityService.GetAllCityOfSelectListItem();
            //viewModel._roles = _unitOfWork.Set<Role>().Where(r => r.IsBanned == false && r.Name != "SystemAdministrator").ToList();

            return View(viewModel);
        }
        #endregion

        #region Edit
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "UserManagement", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public async Task<ActionResult> Edit(int id)
        {
            var loggedInUserId = System.Convert.ToInt32(User.Identity.GetUserId());
            var loggedInUser = _userManager.FindUserById(loggedInUserId);
            var systemAdministrator = _unitOfWork.Set<Role>().FirstOrDefault(x => x.Name.Equals("SystemAdministrator"));

            if (!loggedInUser.Roles.Any(x => x.RoleId == systemAdministrator.Id))
            {
                ViewBag.Roles = _unitOfWork.Set<Role>().Where(x => !x.Name.Equals("SystemAdministrator")).Select(r => new SelectListItem() { Selected = false, Text = r.Name, Value = r.Id.ToString() }).AsEnumerable();
            }
            else
            {
                ViewBag.Roles = _unitOfWork.Set<Role>().Select(r => new SelectListItem() { Selected = false, Text = r.Name, Value = r.Id.ToString() }).AsEnumerable();
            }

            EditUserViewModel editUserViewModel = await _userManager.GetUserViewModelAsync(id);
            //ViewBag.Roles = _roleManager.GetRolesOfSelectListItem();
            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditUserViewModel editUserViewModel)
        {
            var loggedInUserId = System.Convert.ToInt32(User.Identity.GetUserId());
            var loggedInUser = _userManager.FindUserById(loggedInUserId);
            var systemAdministrator = _unitOfWork.Set<Role>().FirstOrDefault(x => x.Name.Equals("SystemAdministrator"));
            var user = _userManager.FindUserById(editUserViewModel.Id);

            if (ModelState.IsValid)
            {
                //اگر کاربر مورد نظر "مدیر سیستمی" بود ، کاربر لاگین کرده هم باید "مدیر سیستمی" باشه تا بتونه ویرایشش کنه
                if (user.Roles.Any(x => x.RoleId == systemAdministrator.Id) && loggedInUser.Roles.Any(x => x.RoleId == systemAdministrator.Id))
                {
                    var update = await _userManager.EditUserAsync(editUserViewModel);
                }
                //اگر کاربر مورد نظر جهت ویرایش "مدیر سیستمی" نبود ، گیر نده
                else if (!user.Roles.Any(x => x.RoleId == systemAdministrator.Id))
                {
                    var update = await _userManager.EditUserAsync(editUserViewModel);                   
                }
                else
                {
                    return RedirectToAction("Index", new { msg = "notPermission" });
                }

                return RedirectToAction("Index", new { msg = "notPermission" });
            }


            if (loggedInUser.Roles.Any(x => x.RoleId != systemAdministrator.Id))
            {
                ViewBag.Roles = _unitOfWork.Set<Role>().Where(x => !x.Name.Equals("SystemAdministrator")).Select(r => new SelectListItem() { Selected = false, Text = r.Name, Value = r.Id.ToString() }).AsEnumerable();
            }
            else
            {
                ViewBag.Roles = _unitOfWork.Set<Role>().Select(r => new SelectListItem() { Selected = false, Text = r.Name, Value = r.Id.ToString() }).AsEnumerable();
            }
            //ViewBag.Roles = _roleManager.GetRolesOfSelectListItem();
            return View(editUserViewModel);
        }
        #endregion

        #region User Profile
        [Mvc5AuthorizeAttribute()]      
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "UserManagement", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]

        public async Task<ActionResult> MyProfile(int id)
        {
            //var userId = _userManager.GetCurrentUserId();
            var viewModel = await _userManager.GetProfileViewModel(id);
            //if (viewModel.FirstName == null && viewModel.LastName == null)
            //{
            //    var userinfo = _userManager.GetCurrentUser();
            //    viewModel.RecoveryEmail = userinfo.Email;
            //}
            viewModel.CityDDL = _cityService.GetAllCityOfSelectListItem();
            
            #region ViewBag.Roles
            var loggedInUserId = System.Convert.ToInt32(User.Identity.GetUserId());
            var loggedInUser = _userManager.FindUserById(loggedInUserId);
            var systemAdministrator = _unitOfWork.Set<Role>().FirstOrDefault(x => x.Name.Equals("SystemAdministrator"));

            if (!loggedInUser.Roles.Any(x => x.RoleId == systemAdministrator.Id))
            {
                ViewBag.Roles = _unitOfWork.Set<Role>().Where(x => !x.Name.Equals("SystemAdministrator")).Select(r => new SelectListItem() { Selected = false, Text = r.Name, Value = r.Id.ToString() }).AsEnumerable();
            }
            else
            {
                ViewBag.Roles = _unitOfWork.Set<Role>().Select(r => new SelectListItem() { Selected = false, Text = r.Name, Value = r.Id.ToString() }).AsEnumerable();
            } 
            #endregion

            //return View("~/Areas/Customer/Views/MyAccount/MyProfile.cshtml", viewModel);
            return View(viewModel);
        }

        [HttpPost]
        [Mvc5AuthorizeAttribute()]
        [ValidateAntiForgeryToken()]
        //[Route("MyProfile")]
        public async Task<ActionResult> MyProfile(MyProfileViewModel viewModel)
        {
            if (true)//ModelState.IsValid)
            {
                var myProfileViewModel = await _userManager.EditProfileAsync(viewModel);
                if (viewModel.ProfileType==UserProfileType.Personal)
                {
                    return RedirectToAction("Index", "User", new { Area = "Admin", msg = "update" });
                }
                else
                {
                    return RedirectToAction("Company", "User", new { Area = "Admin", msg = "update" });
                }
            }
            viewModel.CityDDL = _cityService.GetAllCityOfSelectListItem();

            return View(viewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        [CustomAuthorize(Permissionitem.Delete)]
        [Display(Name = "UserManagement", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]

        public async Task<JsonResult> Delete(int id)
        {
            var loggedInUserId = System.Convert.ToInt32(User.Identity.GetUserId());
            var loggedInUser = _userManager.FindUserById(loggedInUserId);
            var systemAdministrator = _unitOfWork.Set<Role>().FirstOrDefault(x => x.Name.Equals("SystemAdministrator"));
            var user = _userManager.FindUserById(id);

            User model = new User();

            //اگر کاربر مورد نظر "مدیر سیستمی" بود ، کاربر لاگین کرده هم باید "مدیر سیستمی" باشه تا بتونه ویرایشش کنه
            if (user.Roles.Any(x => x.RoleId == systemAdministrator.Id) && loggedInUser.Roles.Any(x => x.RoleId == systemAdministrator.Id))
            {
                model = await _userManager.DeleteLogicallyAsync(id);
            }
            //اگر کاربر مورد نظر جهت ویرایش "مدیر سیستمی" نبود ، گیر نده
            else if (!user.Roles.Any(x => x.RoleId == systemAdministrator.Id))
            {
                model = await _userManager.DeleteLogicallyAsync(id);
            }
            else
            {
                model.IsDeleted = false;
            }


            if (model.IsDeleted == true)
            {
                return Json(true, JsonRequestBehavior.DenyGet);
            }
            return Json(false, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region ChangePassword
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "ChangePassword", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]

        public async Task<ActionResult> ChangePassword(int id)
        {
            ChangePasswordViewModel model = new ChangePasswordViewModel();
            var user = await _userManager.FindByIdAsync(id);
            model.UserId = user.Id;
            model.OldPassword = "NotRequiredLogically"; 
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                int userId = changePasswordViewModel.UserId;
                var user = await _userManager.FindByIdAsync(userId);
                if (!string.IsNullOrEmpty(changePasswordViewModel.Password))
                {
                    Microsoft.AspNet.Identity.PasswordHasher passwordHasher = new PasswordHasher();
                    //PasswordVerificationResult result;

                    var loggedInUserId = System.Convert.ToInt32(User.Identity.GetUserId());
                    var loggedInUser = _userManager.FindUserById(loggedInUserId);
                    var systemAdministrator = _unitOfWork.Set<Role>().FirstOrDefault(x => x.Name.Equals("SystemAdministrator"));

                    //اگر کاربر مورد نظر "مدیر سیستمی" بود ، کاربر لاگین کرده هم باید "مدیر سیستمی" باشه تا بتونه ویرایشش کنه
                    if (user.Roles.Any(x => x.RoleId == systemAdministrator.Id) && loggedInUser.Roles.Any(x => x.RoleId == systemAdministrator.Id))
                    {
                        user.PasswordHash = passwordHasher.HashPassword(changePasswordViewModel.Password);
                    }
                    //اگر کاربر مورد نظر جهت ویرایش "مدیر سیستمی" نبود ، گیر نده
                    else if (!user.Roles.Any(x => x.RoleId == systemAdministrator.Id))
                    {
                        user.PasswordHash = passwordHasher.HashPassword(changePasswordViewModel.Password);
                    }
                    else
                    {
                        return RedirectToAction("Index", new { msg = "notPermission" });
                    }


                    await _unitOfWork.SaveAllChangesAsync();
                    return RedirectToAction("Index", new { msg = "update" });

                    //  var result = passwordHasher.VerifyHashedPassword(user.PasswordHash, changePasswordViewModel.OldPassword);
                    //switch (result)
                    //{
                    //    case PasswordVerificationResult.Failed:
                    //        ModelState.AddModelError("", ParvazPardaz.Resource.Validation.ValidationMessages.InvalidUsernamePassword);
                    //        return View(changePasswordViewModel);
                    //    case PasswordVerificationResult.Success:
                    //        user.PasswordHash = passwordHasher.HashPassword(changePasswordViewModel.Password);
                    //        await _unitOfWork.SaveAllChangesAsync();
                    //        return RedirectToAction("Index", new { msg = "update" });
                    //    case PasswordVerificationResult.SuccessRehashNeeded:
                    //        user.PasswordHash = passwordHasher.HashPassword(changePasswordViewModel.OldPassword);
                    //        await _unitOfWork.SaveAllChangesAsync();
                    //        ModelState.AddModelError("", ParvazPardaz.Resource.Validation.ValidationMessages.TryAgain);
                    //        return View(changePasswordViewModel);
                    //    default:
                    //        break;
                    //}
                }
                ModelState.AddModelError("", ParvazPardaz.Resource.Validation.ValidationMessages.InvalidUsernamePassword);
                return View(changePasswordViewModel);
            }
            return View(changePasswordViewModel);
        }
        #endregion

        #region ChangeUserStatus
        [CustomAuthorize(Permissionitem.Edit)]
        [Display(Name = "UserManagement", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]

        public JsonResult ChangeUserStatus(int StatusUser, int id)
        {
            try
            {
                var loggedInUserId = System.Convert.ToInt32(User.Identity.GetUserId());
                var loggedInUser = _userManager.FindUserById(loggedInUserId);
                var systemAdministrator = _unitOfWork.Set<Role>().FirstOrDefault(x => x.Name.Equals("SystemAdministrator"));
                var user = _userManager.FindUserById(id);

                //اگر کاربر مورد نظر "مدیر سیستمی" بود ، کاربر لاگین کرده هم باید "مدیر سیستمی" باشه تا بتونه ویرایشش کنه
                if (user.Roles.Any(x => x.RoleId == systemAdministrator.Id) && loggedInUser.Roles.Any(x => x.RoleId == systemAdministrator.Id))
                {
                    var users = _userManager.ChangeUserStatus(id, (Model.Enum.StatusUser)StatusUser);
                }
                //اگر کاربر مورد نظر جهت ویرایش "مدیر سیستمی" نبود ، گیر نده
                else if (!user.Roles.Any(x => x.RoleId == systemAdministrator.Id))
                {
                    var users = _userManager.ChangeUserStatus(id, (Model.Enum.StatusUser)StatusUser);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.DenyGet);
                }

                return Json(true, JsonRequestBehavior.DenyGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.DenyGet);
            }
        }
        #endregion
    }
}
