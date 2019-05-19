using System;
using System.Collections.Generic;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Users;

namespace ParvazPardaz.Service.Contract.Users
{
    public interface IUserGroupService : IBaseService<UserGroup>
    {
        #region Properties
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridUserGroupViewModel> GetViewModelForGrid();
        #endregion
    }
}
