using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
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
using System.Data.Entity;
using Infrastructure;
using ParvazPardaz.ViewModel.User;
using System.Security.Claims;
using Postal;
using ParvazPardaz.EmailSendig;
using System.Data.Entity.Core.Objects;

namespace ParvazPardaz.Web.Controllers
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

        #region GetAuthorizePartial
        //[AllowAnonymous]
        //[HttpGet]
        public ActionResult GetAuthorizePartial()
        {
            return PartialView("~/Views/Shared/_PrvAutorize.cshtml");
        }
        #endregion

        #region IsLoggedIn
        [Route("IsLoggedIn")]
        public ActionResult IsLoggedIn()
        {
            return Json(User.Identity.IsAuthenticated, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Login, LogOff
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
        public virtual async Task<ActionResult> Login(LoginCustomerViewModel model, string returnUrl = "")
        {
            if (_userManager.CheckIsUserNotActiveByEmail(model.Email))
            {
                return Json(new { status = "NotValid", message = ParvazPardaz.Resource.Validation.ValidationMessages.NotValid }, JsonRequestBehavior.AllowGet);
            }

            if (!ModelState.IsValid)
            {
                return Json(new { status = "TryAgain", message = ParvazPardaz.Resource.Validation.ValidationMessages.TryAgain }, JsonRequestBehavior.AllowGet);
            }

            var userName = model.Email;
            var loggedinUser = await _userManager.FindAsync(userName, model.Password);
            if (loggedinUser != null)
            {
                await _userManager.UpdateSecurityStampAsync(loggedinUser.Id);
            }

            var result = await _signInManager.PasswordSignInAsync(userName, model.Password, model.RememberMe, shouldLockout: true);

            switch (result)
            {
                case SignInStatus.Success:
                    Session["FullName"] = loggedinUser.FullName;
                    return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);

                case SignInStatus.LockedOut:
                    return Json(new { status = "LockedOut", message = ParvazPardaz.Resource.Validation.ValidationMessages.LockedOutAccount }, JsonRequestBehavior.AllowGet);

                case SignInStatus.Failure:
                    return Json(new { status = "Failure", message = ParvazPardaz.Resource.Validation.ValidationMessages.InvalidUsernamePassword }, JsonRequestBehavior.AllowGet);

                default:
                    return Json(new { status = "TryAgain", message = ParvazPardaz.Resource.Validation.ValidationMessages.TryAgain }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public virtual ActionResult LogOff()
        {
            Session.Remove("FullName");
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect("/");   //Redirect(Request.UrlReferrer.AbsolutePath);
        }
        #endregion

        #region Signup
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Signup(SignupViewModel signupViewModel)
        {
            signupViewModel.Email = signupViewModel.Email.ToLower();
            var foundUserbyEmail = await _userManager.FindByEmailAsync(signupViewModel.Email);

            if (foundUserbyEmail != null)
            {
                return Json(new { status = "EmailExist", message = ParvazPardaz.Resource.Validation.ValidationMessages.EmailExist }, JsonRequestBehavior.AllowGet);
            }

            if (ModelState.IsValid)
            {
                var newUser = await _userManager.SignupAsync(signupViewModel);
                if (newUser.Succeeded)
                {
                    var loggedinUser = await _userManager.FindAsync(signupViewModel.UserName, signupViewModel.Password);
                    if (loggedinUser != null)
                    {
                        await _userManager.UpdateSecurityStampAsync(loggedinUser.Id);
                    }

                    var result = await _signInManager.PasswordSignInAsync(signupViewModel.UserName, signupViewModel.Password, false, shouldLockout: true);

                    switch (result)
                    {
                        case SignInStatus.Success:
                            {
                                Session["FullName"] = loggedinUser.FullName;
                                dynamic emailSend = new Postal.Email("Welcome.Html");
                                emailSend.Subject = "به کی سفر خوش آمدید";
                                emailSend.Name = signupViewModel.FullName;
                                emailSend.To = signupViewModel.Email;
                                emailSend.Send();

                                return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);
                            }

                        case SignInStatus.LockedOut:
                            return Json(new { status = "LockedOut", message = ParvazPardaz.Resource.Validation.ValidationMessages.LockedOutAccount }, JsonRequestBehavior.AllowGet);

                        case SignInStatus.Failure:
                            return Json(new { status = "Failure", message = ParvazPardaz.Resource.Validation.ValidationMessages.InvalidUsernamePassword }, JsonRequestBehavior.AllowGet);

                        default:
                            return Json(new { status = "TryAgain", message = ParvazPardaz.Resource.Validation.ValidationMessages.TryAgain }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { status = "SignupFailure" }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { status = "TryAgain", message = ParvazPardaz.Resource.Validation.ValidationMessages.TryAgain }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ChangePassword
        [Mvc5AuthorizeAttribute()]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Mvc5AuthorizeAttribute()]
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
                            ModelState.AddModelError("OldPassword", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.NotValid, ParvazPardaz.Resource.User.Users.Password));
                            return View(changePasswordViewModel);

                        case PasswordVerificationResult.Success:
                            user.PasswordHash = passwordHasher.HashPassword(changePasswordViewModel.Password);
                            await _unitOfWork.SaveAllChangesAsync();
                            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                            return RedirectToAction("Index", "Home");

                        case PasswordVerificationResult.SuccessRehashNeeded:
                            user.PasswordHash = passwordHasher.HashPassword(changePasswordViewModel.OldPassword);
                            await _unitOfWork.SaveAllChangesAsync();
                            ModelState.AddModelError("OldPassword", ParvazPardaz.Resource.Validation.ValidationMessages.TryAgain);
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

        #region ForgetPassword
        //public ActionResult ForgetPassword()
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordViewModel viewModel)
        {
            //فرستادن ایمیل
            if (ModelState.IsValid)
            {
                //var response = Request["g-recaptcha-response"];
                //if (response == "" && !ReCaptcha.IsValid(response))
                //{
                //    return Json(new { status = "InvalidCaptcha", isRedirect = false }, JsonRequestBehavior.AllowGet);
                //}

                var foundUser = await _userManager.FindByEmailAsync(viewModel.Email);
                if (foundUser != null)
                {
                    var code = Guid.NewGuid().ToString().Replace("-", "");
                    foundUser.RecoveryPasswordCode = code;
                    foundUser.RecoveryPasswordCreatedDateTime = DateTime.Now.Date;
                    foundUser.RecoveryPasswordExpireDate = System.DateTime.Now.AddDays(20).Date;
                    foundUser.RecoveryPasswordStatus = true;

                    var result = await _userManager.UpdateAsync(foundUser);

                    if (result.Succeeded)
                    {
                        var appsettingKey = System.Configuration.ConfigurationManager.AppSettings["WebsiteUrl"];
                        var websiteUrl = appsettingKey != null ? appsettingKey.ToString() : "";

                        try
                        {
                            dynamic emailSend = new Postal.Email("ResetPassword.Html");
                            emailSend.Subject = "بازیابی کلمه عبور";
                            emailSend.Name = websiteUrl + "/account/resetpassword/" + code;
                            emailSend.To = viewModel.Email;
                            await emailSend.SendAsync();

                            return Json(new
                            {
                                validationMsg = ParvazPardaz.Resource.User.Users.SendForgoPassword,
                                isRedirect = true

                            }, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception exc)
                        {
                            //elmah log error
                            Elmah.ErrorSignal.FromCurrentContext().Raise(exc);

                            return Json(new
                            {
                                validationMsg = ParvazPardaz.Resource.Validation.ValidationMessages.TryAgain,
                                isRedirect = false

                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            validationMsg = ParvazPardaz.Resource.Validation.ValidationMessages.TryAgain,
                            isRedirect = false

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        validationMsg = ParvazPardaz.Resource.Validation.ValidationMessages.IncorrectEmail,
                        isRedirect = false

                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new
                {
                    validationMsg = ParvazPardaz.Resource.Validation.ValidationMessages.TryAgain,
                    isRedirect = false

                }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        #region Resetpassword
        public async Task<ActionResult> ResetPassword(string id)
        {
            var foundUser = await _unitOfWork.Set<User>().FirstOrDefaultAsync(u => u.RecoveryPasswordCode.Equals(id.Trim()) && !u.IsDeleted && DbFunctions.TruncateTime(u.RecoveryPasswordExpireDate.Value) >= DbFunctions.TruncateTime(DateTime.Now) && (u.RecoveryPasswordStatus ?? true));
            ResetPasswordConfirmViewModel resetPasswordConfirm = null;

            if (foundUser != null)
            {
                resetPasswordConfirm = new ResetPasswordConfirmViewModel() { RecoveryPasswordCode = id, IsActive = true, Username = foundUser.UserName };
            }
            else
            {
                resetPasswordConfirm = new ResetPasswordConfirmViewModel() { IsActive = false };
            }
            return View(resetPasswordConfirm);
        }


        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPasswordConfirmViewModel viewModel)
        {
            var foundUser = await _unitOfWork.Set<User>().FirstOrDefaultAsync(u => u.RecoveryPasswordCode.Equals(viewModel.RecoveryPasswordCode.Trim()) && u.IsDeleted == false);
            if (foundUser == null || foundUser.IsBanned)
            {
                ModelState.AddModelError("Username", string.Format(ParvazPardaz.Resource.Validation.ValidationMessages.NotValid, ParvazPardaz.Resource.User.Users.Account));
                return View(viewModel);
            }
            if (!foundUser.RecoveryPasswordCode.Equals(viewModel.RecoveryPasswordCode.Trim()) || foundUser.RecoveryPasswordStatus == false || foundUser.RecoveryPasswordExpireDate <= System.DateTime.Now || foundUser.LockoutEnabled)
            {
                ModelState.AddModelError("Username", ParvazPardaz.Resource.Validation.ValidationMessages.NotValidResetPasswordLink);
                return View(viewModel);
            }

            Microsoft.AspNet.Identity.PasswordHasher passwordHasher = new PasswordHasher();
            foundUser.PasswordHash = passwordHasher.HashPassword(viewModel.Password);
            foundUser.RecoveryPasswordStatus = false;
            await _unitOfWork.SaveAllChangesAsync();

            return RedirectToAction("Index", "Home", new { message = "PasswordReseted" });
        }
        #endregion

        #region IsUniqueCustomerEmail
        /// <summary>
        /// اگر شناسه=0 : افزودن کاربر ==> شرط: آیا کاربری این ایمیل را دارد؟
        /// اگر شناسه!=0 : ویرایش کاربر ==> شرط: آیا کاربری به غیر از شناسه مزبور این ایمیل را دارد؟
        /// </summary>
        /// <param name="user"></param>
        /// <returns>ایمیل منحصر به فرد است؟</returns>
        //[Mvc5AuthorizeAttribute()]
        public ActionResult IsUniqueCustomerEmail(SignupViewModel user)
        {
            var userInDb = new User();
            if (user.Id > 0)
            {
                //در حالت ویرایش هستیم
                userInDb = _unitOfWork.Set<User>().FirstOrDefault(x => x.Id != user.Id && x.Email.Equals(user.Email));
            }
            else
            {
                //در حالت افزودن هستیم
                userInDb = _unitOfWork.Set<User>().FirstOrDefault(x => x.Email.Equals(user.Email));
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

        #region GetClientAuthSection
        public ActionResult GetClientAuthSection()
        {
            return PartialView("_PrvMenuAuthSection");
        }
        #endregion
    }
}