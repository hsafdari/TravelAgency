using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Model.Entity.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using System.Web;
using ParvazPardaz.Service.Contract.Users;
using ParvazPardaz.Service.Security;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.ViewModel;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ParvazPardaz.Service.Contract.Common;
using RefactorThis.GraphDiff;
using ParvazPardaz.Common.Extension;
using System.Web.Mvc;

namespace ParvazPardaz.Service.DataAccessService.Users
{
    public class ApplicationUserManager : UserManager<User, int>, IApplicationUserManager
    {

        #region Fields
        private readonly IIdentity _identity;
        private User _user;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<User> _users;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IMappingEngine _mappingEngine;
        private readonly HttpContextBase _httpContextBase;

        private const int A5Width = 420;
        private const int A5Height = 595;
        private const int A6Width = 298;
        private const int A6Height = 420;
        private const string RelativePath = "/Uploads/UserProfile/";
        #endregion

        #region Constructor

        public ApplicationUserManager(IUserStore<User, int> userStore,
            IApplicationRoleManager roleManager, IUnitOfWork unitOfWork, IIdentity identity,
            IDataProtectionProvider dataProtectionProvider, IMappingEngine mappingEngine, HttpContextBase httpContextBase)
            : base(userStore)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _unitOfWork = unitOfWork;
            _users = _unitOfWork.Set<User>();
            _roleManager = roleManager;
            _identity = identity;
            _mappingEngine = mappingEngine;
            _httpContextBase = httpContextBase;
        }

        #endregion

        #region GenerateUserIdentityAsync
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(User user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

            return userIdentity;
        }
        #endregion

        #region CheckIsUserNotActive
        public bool CheckIsUserNotActive(int id)
        {
            return _users.Any(a => a.Id == id && a.Status != StatusUser.Active);
        }

        public bool CheckIsUserNotActive(string userName)
        {
            return _users.Any(a => a.UserName == userName.ToLower() && a.Status != StatusUser.Active);
        }
        public bool CheckIsUserNotActiveByEmail(string email)
        {
            return _users.Any(a => a.Email == email && a.Status != StatusUser.Active);
        }
        #endregion

        #region HasPassword
        public async Task<bool> HasPassword(int userId)
        {
            var user = await FindByIdAsync(userId);
            return user != null && user.PasswordHash != null;
        }
        #endregion

        #region HasPhoneNumber
        public async Task<bool> HasPhoneNumber(int userId)
        {
            var user = await FindByIdAsync(userId);
            return user != null && user.PhoneNumber != null;
        }
        #endregion

        #region SeedDatabase
        public void SeedDatabase()
        {
            const string systemAdminUserName = "admin";
            const string systemAdminPassword = "123456";
            const string systemAdminEmail = "admin@gmail.com";
            const string systemAdminDisplayName = "System Administrator";

            var user = _users.FirstOrDefault(u => u.IsSystemAccount);
            if (user == null)
            {
                user = new User
                {
                    DisplayName = systemAdminDisplayName,
                    UserName = systemAdminUserName,
                    IsSystemAccount = true,
                    Email = systemAdminEmail,
                    Gender = Gender.Man,
                    CreatorDateTime = DateTime.Now
                };
                this.Create(user, systemAdminPassword);
                this.SetLockoutEnabled(user.Id, false);
            }

            var userRoles = _roleManager.FindUserRoles(user.Id);
            if (userRoles.Any(a => a == StandardRoles.SystemAdministrator)) return;
            this.AddToRole(user.Id, StandardRoles.SystemAdministrator);
        }

        #endregion

        #region GetAllUsersAsync
        public Task<List<User>> GetAllUsersAsync()
        {
            return Users.ToListAsync();
        }
        #endregion

        #region DeleteAll
        public async Task RemoveAll()
        {
            await Users.DeleteAsync();
        }
        #endregion

        #region GetUsersWithRoleIds
        public IList<User> GetUsersWithRoleIds(params int[] ids)
        {
            return Users.Where(applicationUser => ids.Contains(applicationUser.Id)).ToList();
        }
        #endregion

        #region Any
        public bool Any()
        {
            return _users.Any();
        }
        #endregion

        #region AddRange
        public void AddRange(IEnumerable<User> users)
        {
            _unitOfWork.AddThisRange(users);
        }
        #endregion

        #region Validations
        public bool CheckUserNameExist(string userName, int? id)
        {
            var users = _users.Select(a => new { Id = a.Id, a.UserName }).ToList();
            return id == null
                ? users.Any(a => string.Equals(a.UserName, userName, StringComparison.InvariantCultureIgnoreCase))
                : users.Any(a => string.Equals(a.UserName, userName, StringComparison.InvariantCultureIgnoreCase) && a.Id != id.Value);
        }

        public bool CheckPhoneNumberExist(string phoneNumber, int? id)
        {
            return id == null
               ? _users.Any(a => a.PhoneNumber == phoneNumber)
               : _users.Any(a => a.PhoneNumber == phoneNumber && a.Id != id.Value);
        }
        #endregion

        #region ChechIsBanneduser
        public bool CheckIsUserBanned(int id)
        {
            return _users.Any(a => a.Id == id && a.IsBanned);
        }

        public bool CheckIsUserBanned(string userName)
        {
            return _users.Any(a => a.UserName == userName.ToLower() && a.IsBanned);
        }
        public bool CheckIsUserBannedByEmail(string email)
        {
            return _users.Any(a => a.Email == email && a.IsBanned);
        }
        #endregion

        #region GetEmailStore
        public IUserEmailStore<User, int> GetEmailStore()
        {
            var cast = Store as IUserEmailStore<User, int>;
            if (cast == null)
            {
                throw new NotSupportedException("not support");
            }
            return cast;
        }

        #endregion

        #region IsEmailAvailableForConfirm
        public bool IsEmailAvailableForConfirm(string email)
        {
            return _users.Any(a => a.Email == email);
        }
        #endregion

        #region EditSecurityStamp
        public void EditSecurityStamp(int userId)
        {
            this.UpdateSecurityStamp(userId);
        }
        #endregion

        #region FindUserById
        public User FindUserById(int id)
        {
            return this.FindById(id);
        }
        #endregion

        #region CurrentUser
        public User GetCurrentUser()
        {
            return _user ?? (_user = this.FindById(GetCurrentUserId()));
        }

        public async Task<User> GetCurrentUserAsync()
        {
            return _user ?? (_user = await FindByIdAsync(GetCurrentUserId()));
        }

        public int GetCurrentUserId()
        {
            return int.Parse(HttpContext.Current.User.Identity.GetUserId());
        }

        //public bool IsAdministrator()
        //{
        //    return HttpContext.Current.User.IsInRole(StandardRoles.Administrators);
        //}
        //public bool IsOperator()
        //{
        //    return HttpContext.Current.User.IsInRole(StandardRoles.Operators);
        //}
        #endregion

        #region DeleteAsync

        public Task DeleteAsync(int id)
        {
            return _users.Where(a => a.Id == id).DeleteAsync();
        }
        #endregion

        #region IsSystemUser

        public Task<bool> IsSystemUser(int id)
        {
            return _users.AnyAsync(a => a.Id == id && a.IsSystemAccount);
        }
        #endregion

        #region Count
        public long Count()
        {
            return _users.LongCount();
        }
        #endregion

        #region GetIpAddress

        public string GetIpAddress()
        {
            if (HttpContext.Current == null)
                return null;
            return HttpContext.Current.Request.UserHostAddress;
        }
        #endregion

        #region GetForGrid
        public IQueryable<GridUserViewModel> GetViewModelForGrid(UserProfileType profileType)
        {
            return _users.Where(w => w.IsDeleted == false && w.IsSystemAccount == false && w.UserProfile.ProfileType == profileType).AsNoTracking().ProjectTo<GridUserViewModel>(_mappingEngine);
        }
        #endregion

        #region SignupAsync
        public async Task<IdentityResult> SignupAsync(SignupViewModel viewModel)
        {
            var user = _mappingEngine.Map<User>(viewModel);
            var sepratedFullName = viewModel.FullName.Split(' ');
            user.FirstName = sepratedFullName[0];
            user.LastName = string.Join(" ", sepratedFullName.Skip(1));
            user.Status = StatusUser.Active;
            //دادن نقش کاربران(مشتری) به کاربر در حال عضویت
            var customerRole = await _roleManager.FindByNameAsync(StandardRoles.Customer);
            UserRole newUserRole = new UserRole() { RoleId = customerRole.Id };
            user.Roles.Add(newUserRole);

            #region ایجاد پروفایل برای مشتری
            var newUserProfile = new UserProfile()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                MobileNumber = viewModel.PhoneNumber,
                ProfileType = UserProfileType.Personal
            };
            user.UserProfile = new UserProfile();
            user.UserProfile = newUserProfile;
            #endregion

            return await CreateAsync(user, viewModel.Password);
        }
        #endregion

        #region CreateUserAsync
        public async Task<IdentityResult> CreateUserAsync(AddUserViewModel viewModel)
        {
            var user = _mappingEngine.Map<User>(viewModel);
            var listSelectedRoles = _unitOfWork.Set<Role>().Where(r => viewModel.SelectedRoles.Any(sr => sr == r.Id)).ToList();
            listSelectedRoles.ForEach(selRoles =>
            {
                user.Roles.Add(new UserRole() { RoleId = selRoles.Id, UserId = user.Id });
            });
            return await CreateAsync(user, viewModel.Password);
        }
        #endregion

        #region SignupAdminAsync
        public async Task<IdentityResult> SignupAdminAsync(AddUserUserProfileViewModel viewModel)
        {
            #region ایجاد کاربر
            SignupViewModel newSignup = new SignupViewModel()
            {
                //Email = viewModel.AddUser.Email,
                UserName = viewModel.AddUser.UserName,
                Password = viewModel.AddUser.Password,
                //ConfirmPassword = viewModel.AddUser.Password,
            };

            var user = _mappingEngine.Map<User>(newSignup);

            //دادن نقش
            foreach (var rId in viewModel._selectedRoles)
            {
                var selectedRole = await _roleManager.FindByIdAsync(rId);
                if (selectedRole != null)
                {
                    UserRole newUserRole = new UserRole() { RoleId = selectedRole.Id };
                    user.Roles.Add(newUserRole);
                }
            }

            return await CreateAsync(user, viewModel.AddUser.Password);
            #endregion
        }
        #endregion

        #region EditUserAsync
        public async Task<EditUserViewModel> EditUserAsync(EditUserViewModel viewModel)
        {
            var user = _users.Find(viewModel.Id);
            _mappingEngine.Map(viewModel, user);
            if (viewModel.SelectedRoles.Any())
            {
                _unitOfWork.MarkAsDetached(user);
                var roles = _unitOfWork.Set<Role>().Where(r => viewModel.SelectedRoles.Any(sr => sr == r.Id)).ToList();
                roles.ForEach(role =>
                {
                    user.Roles.Add(new UserRole() { RoleId = role.Id, UserId = user.Id });
                });
                _unitOfWork.Update(user, a => a.AssociatedCollection(u => u.Roles));
            }
            {
                user.Roles.Clear();
            }

            if (!await IsInRoleAsync(user.Id, StandardRoles.SystemAdministrator))
                this.UpdateSecurityStamp(user.Id);
            else await _unitOfWork.SaveAllChangesAsync();

            return await GetUserViewModelAsync(viewModel.Id);
        }
        #endregion

        #region GetUserViewModelAsync
        public virtual async Task<EditUserViewModel> GetUserViewModelAsync(int id)
        {
            return await _unitOfWork.Set<User>().AsNoTracking().ProjectTo<EditUserViewModel>(_mappingEngine).FirstOrDefaultAsync(u => u.Id == id);
        }
        #endregion

        #region DeleteLogicallyAsync
        public async Task<User> DeleteLogicallyAsync(int id)
        {
            var model = await _unitOfWork.Set<User>().FirstOrDefaultAsync(u => u.Id == id);
            model.IsDeleted = true;
            model.DeletedDateTime = DateTime.Now;
            await _unitOfWork.SaveAllChangesAsync();
            return model;
        }
        #endregion

        #region GetViewModel
        public async Task<MyProfileViewModel> GetProfileViewModel(int id)
        {
            return await _users.AsNoTracking().ProjectTo<MyProfileViewModel>(_mappingEngine).FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region EditProfileAsync
        public async Task<MyProfileViewModel> EditProfileAsync(MyProfileViewModel viewModel)
        {
            //var user = _users.Find(viewModel.Id);
            var user = _users.FirstOrDefault(x => x.Id == viewModel.Id);
            //var userProfile = _userProfiles.Find(viewModel.Id);

            // اگر قبلا پروفایلی داشته ، ویرایش
            if (user.UserProfile != null && viewModel != null)
            {
                #region ویرایش پروفایل
                var model = _mappingEngine.DynamicMap<UserProfile>(viewModel);

                #region Update Avatar Image
                if (viewModel.File.HasFile())
                {
                    if (model.AvatarUrl != null)
                    {
                        //حذف تصویر بزرگ قبلی
                        if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.AvatarUrl)))
                        {
                            System.IO.File.Delete(_httpContextBase.Server.MapPath(model.AvatarUrl));
                        }
                        //حذف تصویر کوچک قبلی
                        var splittedUrl = model.AvatarUrl.Split('.');
                        var imgThumbUrl = splittedUrl[0] + "_Thumb." + splittedUrl[1];
                        if (System.IO.File.Exists(_httpContextBase.Server.MapPath(imgThumbUrl)))
                        {
                            System.IO.File.Delete(_httpContextBase.Server.MapPath(imgThumbUrl));
                        }
                    }


                    string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                    string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
                    string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);

                    model.AvatarUrl = relativePathWithFileNameOriginal;
                    model.AvatarExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                    model.AvatarFileName = viewModel.File.FileName;
                    model.AvatarSize = viewModel.File.ContentLength;

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
                //حذف آدرس های قبلی                    
                if (user.UserProfile.UserAddresses != null && user.UserProfile.UserAddresses.Any())
                {
                    foreach (var oldAddress in user.UserProfile.UserAddresses.ToList())
                    {
                        _unitOfWork.Set<UserAddress>().Remove(oldAddress);
                    }
                }

                if (viewModel.UserAddresses != null && viewModel.UserAddresses.Any())
                {
                    model.UserAddresses = new List<UserAddress>();

                    // افزودن آدرس های جدید   
                    foreach (var address in viewModel.UserAddresses)
                    {
                        var newAddress = _mappingEngine.Map<UserAddress>(address);
                        //newAddress.UserProfileId = user.UserProfile.Id;
                        model.UserAddresses.Add(newAddress);
                    }
                }
                #endregion

                model.CreatorDateTime = user.UserProfile.CreatorDateTime;
                model.CreatorUserId = user.UserProfile.CreatorUserId;
                model.CreatorUserIpAddress = user.UserProfile.CreatorUserIpAddress;
                model.ModifierDateTime = DateTime.Now;
                model.ModifierUserId = HttpContext.Current.Request.GetUserId();
                model.ModifierUserIpAddress = HttpContext.Current.Request.GetIp();
                user.UserProfile = model;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.FullName = model.FirstName + " " + model.LastName;
                
                #region اگه کسی دیگه این ایمیل رو نداشت ، در نظر بگیر
                var hasOtherThisEmail = _unitOfWork.Set<User>().Any(x => x.Id != user.Id && x.Email.ToLower().Equals(model.RecoveryEmail));
                if (!hasOtherThisEmail)
                {
                    user.Email = model.RecoveryEmail;
                }
                #endregion

                #region اگه کسی دیگه این نام کاربری رو نداشت ، در نظر بگیر
                var hasOtherThisUseName = _unitOfWork.Set<User>().Any(x => x.Id != user.Id && x.UserName.ToLower().Equals(viewModel.UserName));
                if (!hasOtherThisUseName)
                {
                    user.UserName = viewModel.UserName;
                }
                #endregion

                #region editing roles
                if (viewModel._selectedRoles != null && viewModel._selectedRoles.Any())
                {
                    user.Roles.Clear();
                    foreach (var item in viewModel._selectedRoles)
                    {
                        user.Roles.Add(new UserRole()
                        {
                            UserId = viewModel.Id,
                            RoleId = item
                        });
                    }
                }
                #endregion

                if (user.UserProfile.MobileNumber != null && user.UserProfile.MobileNumber.Trim() != "")
                {
                    user.PhoneNumber = user.UserProfile.MobileNumber;
                }

                //await _unitOfWork.SaveAllChangesAsync();
                await _unitOfWork.SaveAllChangesAsync(false);
                #endregion
            }
            else // اگر پروفایلی وجود ندارد ، ایجاد
            {
                #region ایجاد پروفایل
                var model = _mappingEngine.Map<UserProfile>(viewModel);

                #region Saving Avatar Image
                if (viewModel.File.HasFile())
                {
                    string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                    string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
                    string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);

                    model.AvatarUrl = relativePathWithFileNameOriginal;
                    model.AvatarExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                    model.AvatarFileName = viewModel.File.FileName;
                    model.AvatarSize = viewModel.File.ContentLength;
                    user.FirstName = viewModel.FirstName;
                    user.LastName = viewModel.LastName;
                    user.FullName = viewModel.FirstName + " " + viewModel.LastName;

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

                #region Saving addresses - COMMENTED, SAVE BY MAPPING WITH AUTOMAPPER
                //if (viewModel.UserAddresses != null && viewModel.UserAddresses.Any())
                //{
                //    // افزودن آدرس های جدید   
                //    foreach (var address in viewModel.UserAddresses)
                //    {
                //        var newAddress = _mappingEngine.Map<UserAddress>(address);
                //        model.UserAddresses.Add(newAddress);
                //    }
                //}
                #endregion

                user.UserProfile = model;
                user.UserProfile.RemainingCreditValue = 0;

                if (user.UserProfile.MobileNumber != null && user.UserProfile.MobileNumber.Trim() != "")
                {
                    user.PhoneNumber = user.UserProfile.MobileNumber;
                }

                //await _unitOfWork.SaveAllChangesAsync();
                _unitOfWork.SaveAllChanges();
                #endregion
            }

            return await GetProfileViewModel(viewModel.Id);
        }
        #endregion

        #region EditProfileAsync
        public async Task<MyProfileViewModel> EditUserProfileAsync(MyProfileViewModel viewModel)
        {
            //var user = _users.Find(viewModel.Id);
            var user = _users.FirstOrDefault(x => x.Id == viewModel.Id);
            //var userProfile = _userProfiles.Find(viewModel.Id);

            // اگر قبلا پروفایلی داشته ، ویرایش
            if (user.UserProfile != null && viewModel != null)
            {
                #region ویرایش پروفایل
                var model = _mappingEngine.DynamicMap<UserProfile>(viewModel);

                #region Update Avatar Image
                if (viewModel.File.HasFile())
                {
                    if (model.AvatarUrl != null)
                    {
                        //حذف تصویر بزرگ قبلی
                        if (System.IO.File.Exists(_httpContextBase.Server.MapPath(model.AvatarUrl)))
                        {
                            System.IO.File.Delete(_httpContextBase.Server.MapPath(model.AvatarUrl));
                        }
                        //حذف تصویر کوچک قبلی
                        var splittedUrl = model.AvatarUrl.Split('.');
                        var imgThumbUrl = splittedUrl[0] + "_Thumb." + splittedUrl[1];
                        if (System.IO.File.Exists(_httpContextBase.Server.MapPath(imgThumbUrl)))
                        {
                            System.IO.File.Delete(_httpContextBase.Server.MapPath(imgThumbUrl));
                        }
                    }


                    string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                    string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
                    string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);

                    model.AvatarUrl = relativePathWithFileNameOriginal;
                    model.AvatarExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                    model.AvatarFileName = viewModel.File.FileName;
                    model.AvatarSize = viewModel.File.ContentLength;

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
                //حذف آدرس های قبلی                    
                if (user.UserProfile.UserAddresses != null && user.UserProfile.UserAddresses.Any())
                {
                    foreach (var oldAddress in user.UserProfile.UserAddresses.ToList())
                    {
                        _unitOfWork.Set<UserAddress>().Remove(oldAddress);
                    }
                }

                if (viewModel.UserAddresses != null && viewModel.UserAddresses.Any())
                {
                    model.UserAddresses = new List<UserAddress>();

                    // افزودن آدرس های جدید   
                    foreach (var address in viewModel.UserAddresses)
                    {
                        var newAddress = _mappingEngine.Map<UserAddress>(address);
                        //newAddress.UserProfileId = user.UserProfile.Id;
                        model.UserAddresses.Add(newAddress);
                    }
                }
                #endregion

                model.CreatorDateTime = user.UserProfile.CreatorDateTime;
                model.CreatorUserId = user.UserProfile.CreatorUserId;
                model.CreatorUserIpAddress = user.UserProfile.CreatorUserIpAddress;
                model.ModifierDateTime = DateTime.Now;
                model.ModifierUserId = HttpContext.Current.Request.GetUserId();
                model.ModifierUserIpAddress = HttpContext.Current.Request.GetIp();
                user.UserProfile = model;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.FullName = model.FirstName + " " + model.LastName;

                #region اگه کسی دیگه این ایمیل رو نداشت ، در نظر بگیر
                var hasOtherThisEmail = _unitOfWork.Set<User>().Any(x => x.Id != user.Id && x.Email.ToLower().Equals(model.RecoveryEmail));
                if (!hasOtherThisEmail)
                {
                    user.Email = model.RecoveryEmail;
                    user.UserName = model.RecoveryEmail;
                }
                #endregion

                #region editing roles
                if (viewModel._selectedRoles != null && viewModel._selectedRoles.Any())
                {
                    user.Roles.Clear();
                    foreach (var item in viewModel._selectedRoles)
                    {
                        user.Roles.Add(new UserRole()
                        {
                            UserId = viewModel.Id,
                            RoleId = item
                        });
                    }
                }
                #endregion

                if (user.UserProfile.MobileNumber != null && user.UserProfile.MobileNumber.Trim() != "")
                {
                    user.PhoneNumber = user.UserProfile.MobileNumber;
                }

                //await _unitOfWork.SaveAllChangesAsync();
                await _unitOfWork.SaveAllChangesAsync(false);
                #endregion
            }
            else // اگر پروفایلی وجود ندارد ، ایجاد
            {
                #region ایجاد پروفایل
                var model = _mappingEngine.Map<UserProfile>(viewModel);

                #region Saving Avatar Image
                if (viewModel.File.HasFile())
                {
                    string relativePathWithFileName = RelativePath + System.Guid.NewGuid();
                    string relativePathWithFileNameOriginal = relativePathWithFileName + System.IO.Path.GetExtension(viewModel.File.FileName);
                    string relativePathWithFileNameThumb = relativePathWithFileName + "_Thumb" + System.IO.Path.GetExtension(viewModel.File.FileName);

                    model.AvatarUrl = relativePathWithFileNameOriginal;
                    model.AvatarExtension = System.IO.Path.GetExtension(viewModel.File.FileName);
                    model.AvatarFileName = viewModel.File.FileName;
                    model.AvatarSize = viewModel.File.ContentLength;
                    user.FirstName = viewModel.FirstName;
                    user.LastName = viewModel.LastName;
                    user.FullName = viewModel.FirstName + " " + viewModel.LastName;

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

                #region Saving addresses - COMMENTED, SAVE BY MAPPING WITH AUTOMAPPER
                //if (viewModel.UserAddresses != null && viewModel.UserAddresses.Any())
                //{
                //    // افزودن آدرس های جدید   
                //    foreach (var address in viewModel.UserAddresses)
                //    {
                //        var newAddress = _mappingEngine.Map<UserAddress>(address);
                //        model.UserAddresses.Add(newAddress);
                //    }
                //}
                #endregion

                user.UserProfile = model;
                user.UserProfile.RemainingCreditValue = 0;

                if (user.UserProfile.MobileNumber != null && user.UserProfile.MobileNumber.Trim() != "")
                {
                    user.PhoneNumber = user.UserProfile.MobileNumber;
                }

                //await _unitOfWork.SaveAllChangesAsync();
                _unitOfWork.SaveAllChanges();
                #endregion
            }

            return await GetProfileViewModel(viewModel.Id);
        }
        #endregion

        #region ChangeUserStatus
        public int ChangeUserStatus(int Id, ParvazPardaz.Model.Enum.StatusUser StatusUser)
        {
            var model = _users.Find(Id);
            model.Status = StatusUser;
            return _unitOfWork.SaveAllChanges();
        }
        #endregion

        #region GetWriterDDL
        public SelectList GetBlogWriterDDL()
        {
            //var writerRole = _roleManager.FindRoleByName("Writer");
            var blogWriterRole = _unitOfWork.Set<Role>().FirstOrDefault(x => x.Name.Equals("BlogWriter"));
            var userList = _unitOfWork.Set<User>().Where(x => !x.IsDeleted && !x.IsBanned && !x.IsSystemAccount).ToList();
            if (blogWriterRole != null)
            {
                return new SelectList(userList.Where(x => blogWriterRole.Users.Any(a => a.UserId == x.Id)).Select(s => new { Title = (s.UserProfile != null ? s.UserProfile.DisplayName : (s.FirstName != null ? s.FirstName + " " + s.LastName : "--بی نام--")), Value = s.Id.ToString() }).ToList(), "Value", "Title");
            }
            return new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Title");
        }
        #endregion
    }
}



