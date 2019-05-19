using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ParvazPardaz.Model.Entity.SocialLog;
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.Service.Contract.Users;
using ParvazPardaz.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ParvazPardaz.Common.Extension;
using ParvazPardaz.DataAccess.Infrastructure;
using ParvazPardaz.Common.Utility;

namespace ParvazPardaz.Service.DataAccessService.Users
{
    public class ApplicationSignInManager : SignInManager<User, int>, IApplicationSignInManager
    {
        #region Fields
        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor

        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager, IUnitOfWork unitOfWork)
            : base(userManager, authenticationManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region LoginLog
        public void LogLogin(User model, HttpBrowserCapabilitiesBase request)
        {
            UserLog log = new UserLog();
            log.LogDateTime = DateTime.Now;
            log.UserId = model.Id;
            log.IPAddress = HttpContext.Current.Request.GetIp();
            log.IsLogined = true;
            log.Browser = UserInfo.UserRequestInfo(request);
            _unitOfWork.Set<UserLog>().Add(log);
            _unitOfWork.SaveAllChanges();
        }

        public void LogUnSuccessLogin(LoginViewModel model, HttpBrowserCapabilitiesBase request)
        {
            UserUnSuccessLog log = new UserUnSuccessLog();
            log.UserName = model.UserName;
            log.Password = model.Password;
            log.RequestTime = DateTime.Now;
            log.IPAddress = HttpContext.Current.Request.GetIp();
            log.Browser = UserInfo.UserRequestInfo(request);
            _unitOfWork.Set<UserUnSuccessLog>().Add(log);
            _unitOfWork.SaveAllChanges();
        }
        #endregion
    }
}
