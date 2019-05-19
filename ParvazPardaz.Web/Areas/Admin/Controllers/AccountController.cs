using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using ParvazPardaz.ViewModel.User;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Service.Contract.Users;
using Microsoft.Owin.Security;
using ParvazPardaz.Model.Entity.Users;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using ParvazPardaz.Common.Filters;
using ParvazPardaz.Service.Security;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.Model.Enum;
using Infrastructure;

namespace ParvazPardaz.Web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        #region	Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationSignInManager _signInManager;
        #endregion

        #region	Ctor
        public AccountController(IUnitOfWork unitOfWork, IApplicationUserManager userManager, IAuthenticationManager authenticationManager,
                                 IApplicationSignInManager signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _signInManager = signInManager;
        }
        #endregion

        #region Login,LogOff
        [AllowAnonymous]
        [HttpGet]
        public virtual ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //var response = Request["g-recaptcha-response"];
            //if (response == null || !ReCaptcha.IsValid(response))
            //{
            //    ModelState.AddModelError("DivRecaptchaRequired", ParvazPardaz.Resource.Validation.ValidationMessages.DivRecaptchaRequired);
            //    return View(model);
            //}

            if (_userManager.CheckIsUserBanned(model.UserName))
            {
                ModelState.AddModelError("", ParvazPardaz.Resource.Validation.ValidationMessages.InvalidAccount);
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loggedinUser = await _userManager.FindAsync(model.UserName, model.Password);
            if (loggedinUser != null && (loggedinUser.IsDeleted || loggedinUser.IsBanned))
            {
                ModelState.AddModelError("UserName", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.LockedOutAccount, _userManager.DefaultAccountLockoutTimeSpan));
                return View(model);
            }

            if (loggedinUser != null)
            {
                await _userManager.UpdateSecurityStampAsync(loggedinUser.Id);
            }

            SignInStatus result;
            if (loggedinUser != null && loggedinUser.Status == StatusUser.Active)
            {
                result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: true);
            }
            else if (loggedinUser != null && loggedinUser.Status == StatusUser.DeActive)
            {
                result = SignInStatus.LockedOut;
            }
            else
            {
                result = SignInStatus.Failure;
            }


            switch (result)
            {
                case SignInStatus.Success:
                    _signInManager.LogLogin(loggedinUser, Request.Browser);
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    ModelState.AddModelError("UserName", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.LockedOutAccount, _userManager.DefaultAccountLockoutTimeSpan));
                    return View(model);

                case SignInStatus.Failure:
                    _signInManager.LogUnSuccessLogin(model, Request.Browser);
                    ModelState.AddModelError("", ParvazPardaz.Resource.Validation.ValidationMessages.InvalidUsernamePassword);
                    return View(model);

                default:
                    ModelState.AddModelError("", ParvazPardaz.Resource.Validation.ValidationMessages.TryAgain);
                    return View(model);
            }
        }

        [HttpGet]
        [Mvc5AuthorizeAttribute()]
        public virtual ActionResult LogOff()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            System.Web.HttpContext.Current.Session["MenuItems"] = null;
            return Redirect("/");
            //return RedirectToAction("Login", new { @Area = "Admin" });
        }

        #endregion

        #region ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
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
                    PasswordVerificationResult result;

                    var loggedInUserId = System.Convert.ToInt32(User.Identity.GetUserId());
                    var loggedInUser = _userManager.FindUserById(loggedInUserId);
                    var systemAdministrator = _unitOfWork.Set<Role>().FirstOrDefault(x => x.Name.Equals("SystemAdministrator"));

                    //اگر کاربر مورد نظر "مدیر سیستمی" بود ، کاربر لاگین کرده هم باید "مدیر سیستمی" باشه تا بتونه ویرایشش کنه
                    if (user.Roles.Any(x => x.RoleId == systemAdministrator.Id) && loggedInUser.Roles.Any(x => x.RoleId == systemAdministrator.Id))
                    {
                        result = passwordHasher.VerifyHashedPassword(user.PasswordHash, changePasswordViewModel.OldPassword);
                    }
                    //اگر کاربر مورد نظر جهت ویرایش "مدیر سیستمی" نبود ، گیر نده
                    else if (!user.Roles.Any(x => x.RoleId == systemAdministrator.Id))
                    {
                        result = passwordHasher.VerifyHashedPassword(user.PasswordHash, changePasswordViewModel.OldPassword);
                    }
                    else
                    {
                        result = PasswordVerificationResult.Failed;
                    }

                    switch (result)
                    {
                        case PasswordVerificationResult.Failed:
                            ModelState.AddModelError("", ParvazPardaz.Resource.Validation.ValidationMessages.InvalidUsernamePassword);
                            return View(changePasswordViewModel);
                        case PasswordVerificationResult.Success:
                            user.PasswordHash = passwordHasher.HashPassword(changePasswordViewModel.Password);
                            await _unitOfWork.SaveAllChangesAsync();
                            return RedirectToAction("LogOff");
                        case PasswordVerificationResult.SuccessRehashNeeded:
                            user.PasswordHash = passwordHasher.HashPassword(changePasswordViewModel.OldPassword);
                            await _unitOfWork.SaveAllChangesAsync();
                            ModelState.AddModelError("", ParvazPardaz.Resource.Validation.ValidationMessages.TryAgain);
                            return View(changePasswordViewModel);
                        default:
                            break;
                    }
                }
                ModelState.AddModelError("", ParvazPardaz.Resource.Validation.ValidationMessages.InvalidUsernamePassword);
                return View(changePasswordViewModel);
            }
            return View(changePasswordViewModel);
        }
        #endregion

        #region MyProfile

        public async Task<ActionResult> MyProfile()
        {
            var userId = _userManager.GetCurrentUserId();
            var viewModel = await _userManager.GetProfileViewModel(userId);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> MyProfile(MyProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var myProfileViewModel = await _userManager.EditProfileAsync(viewModel);
                return RedirectToAction("Index", "Dashboard");
            }
            return View(viewModel);

        }
        #endregion

        #region Private
        [NonAction]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        #endregion
    }
}