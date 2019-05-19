using System;
using System.Linq;
using System.Text;
using ParvazPardaz.ViewModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using EntityType = ParvazPardaz.Model;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.Model.Entity.AccessLevel;
using ParvazPardaz.Model.Entity.Core;

namespace ParvazPardaz.Service.Contract.AccessLevel
{
    public interface IPermissionService : IBaseService<Permission>
    {
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridPermissionViewModel> GetViewModelForGrid();

        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridPermissionViewModel> GetViewModelForGrid(GridPermissionViewModel searchPermissionViewModel);

        /// <summary>
        /// بررسی دسترسی داشتن
        /// </summary>
        /// <param name="permission"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        bool CanAccess(string permission, string url);
        bool SetAccess(int RoleId, string Url, bool Access, string Permission);
        /// <summary>
        /// Return Menu Permissions
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        List<Menu> MenuPermissions(int userId);
    }
}
