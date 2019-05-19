using AutoMapper;
using Microsoft.Owin.Security;
using ParvazPardaz.Common.Controller;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.Service.Contract.Country;
using ParvazPardaz.Service.Contract.Users;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ParvazPardaz.Common.Extension;
using Microsoft.AspNet.Identity;

namespace ParvazPardaz.Web.Areas.Customer.Controllers
{
    public class MyAccountController : BaseController
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingEngine _mappingEngine;
        private readonly IApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationSignInManager _signInManager;
        //private readonly IOrderService _orderService;
        //private readonly IWishProductListService _wishlist;
        private readonly ICityService _cityService;
        #endregion

        #region	Ctor
        public MyAccountController(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IAuthenticationManager authenticationManager, IMappingEngine mappingEngine,
                                 IApplicationSignInManager signInManager, ICityService cityService)//, IOrderService orderservice, IWishProductListService wishlist)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _signInManager = signInManager;
            //_orderService = orderservice;
            _cityService = cityService;
            //_wishlist = wishlist;
            _mappingEngine = mappingEngine;
        }
        #endregion

        #region User Profile
        [Mvc5AuthorizeAttribute()]
        //[Route("MyProfile")]
        public async Task<ActionResult> MyProfile()
        {
            var userId = _userManager.GetCurrentUserId();
            var viewModel = await _userManager.GetProfileViewModel(userId);
            if (viewModel.FirstName == null && viewModel.LastName == null)
            {
                var userinfo = _userManager.GetCurrentUser();
                viewModel.RecoveryEmail = userinfo.Email;
            }
            viewModel._BirthDate = (viewModel.BirthDate != null ? viewModel.BirthDate.Value.ToString("yyyy/M/d") : DateTime.Now.ToString("yyyy/M/d"));
            viewModel.CityDDL = _cityService.GetAllCityOfSelectListItem();
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
                #region محاسبه تاریخ
                if (viewModel._BirthDate != null && viewModel._BirthDate.Trim() != "")
                {
                    if (viewModel.Calendertype == "persian")
                    {
                        var pc = new PersianCalendar();
                        string Date = viewModel._BirthDate.ToPersianNumber();
                        viewModel.BirthDate = System.Convert.ToDateTime(Date);
                        viewModel._BirthDate = viewModel.BirthDate.Value.ToString("yyyy/MM/dd");
                    }
                    else if (viewModel.Calendertype == "gregorian")
                    {
                        string Date = viewModel._BirthDate.ToPersianNumber();
                        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                        viewModel.BirthDate = System.Convert.ToDateTime(Date);
                        viewModel._BirthDate = viewModel.BirthDate.Value.ToString("yyyy/MM/dd");
                    }
                }
                #endregion

                var myProfileViewModel = await _userManager.EditUserProfileAsync(viewModel);
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index", "Dashboard", new { Area = "Customer" });
            }
            //viewModel.CityDDL = _cityService.GetAllCityOfSelectListItem();
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get UserAddress Partial
        /// <summary>
        /// مقداردهی پارشال-ویوو برای آدرس کاربر
        /// </summary>
        /// <param name="setProp"></param>
        /// <returns></returns>
        [HttpPost]
        public PartialViewResult GetUserAddressSection(MyProfileViewModel viewModel)
        {
            viewModel.CityDDL = _cityService.GetAllCityOfSelectListItem();
            return PartialView("_PrvUserAddress", viewModel);
        }
        #endregion

        #region IsUniqueEmail
        /// <summary>
        /// اگر شناسه=0 : افزودن کاربر ==> شرط: آیا کاربری این ایمیل را دارد؟
        /// اگر شناسه!=0 : ویرایش کاربر ==> شرط: آیا کاربری به غیر از شناسه مزبور این ایمیل را دارد؟
        /// </summary>
        /// <param name="user"></param>
        /// <returns>ایمیل منحصر به فرد است؟</returns>
        public ActionResult IsUniqueEmail(MyProfileViewModel user)
        {
            var userInDb = new User();
            if (user.Id > 0)
            {
                //در حالت ویرایش هستیم
                userInDb = _unitOfWork.Set<User>().FirstOrDefault(x => x.Id != user.Id && x.Email.Equals(user.RecoveryEmail));
            }
            else
            {
                //در حالت افزودن هستیم
                userInDb = _unitOfWork.Set<User>().FirstOrDefault(x => x.Email.Equals(user.RecoveryEmail));
            }

            if (userInDb != null)
            {
                //خیر، قبلا استفاده شده است
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            //بله، منحصر به فرد است
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ChangePassword
        public async Task<ActionResult> ChangePassword()
        {
            var userId = _userManager.GetCurrentUserId();
            ChangePasswordViewModel model = new ChangePasswordViewModel();
            var user = await _userManager.FindByIdAsync(userId);
            model.UserId = user.Id;
            model.OldPassword = "NotRequiredLogically";
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                int userId = _userManager.GetCurrentUserId();
                var user = await _userManager.FindByIdAsync(userId);
                if (!string.IsNullOrEmpty(changePasswordViewModel.Password))
                {
                    Microsoft.AspNet.Identity.PasswordHasher passwordHasher = new PasswordHasher();
                    var result = passwordHasher.VerifyHashedPassword(user.PasswordHash, changePasswordViewModel.OldPassword);
                    switch (result)
                    {
                        case PasswordVerificationResult.Failed:
                            return Json(new { status = false, message = ParvazPardaz.Resource.General.Generals.WrongOldPassword }, JsonRequestBehavior.AllowGet);

                        case PasswordVerificationResult.Success:
                            user.PasswordHash = passwordHasher.HashPassword(changePasswordViewModel.Password);
                            await _unitOfWork.SaveAllChangesAsync();
                            //_authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                            //return Json(new { status = true, redirectUrl = "/" }, JsonRequestBehavior.AllowGet);
                            return Json(new { status = true, redirectUrl = "/Customer/MyAccount/MyProfile#user-password" }, JsonRequestBehavior.AllowGet);


                        case PasswordVerificationResult.SuccessRehashNeeded:
                            user.PasswordHash = passwordHasher.HashPassword(changePasswordViewModel.OldPassword);
                            await _unitOfWork.SaveAllChangesAsync();
                            return Json(new { status = true, redirectUrl = "/" }, JsonRequestBehavior.AllowGet);

                        default:
                            break;
                    }
                }
            }
            return Json(new { status = false, message = ParvazPardaz.Resource.Validation.ValidationMessages.InvalidUsernamePassword }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}