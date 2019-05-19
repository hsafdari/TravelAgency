using ParvazPardaz.Model;
using ParvazPardaz.Service.Contract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.ViewModel;

namespace ParvazPardaz.Service.Contract.Core
{
    public interface IDepartmentService : IBaseService<Department>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridDepartmentViewModel> GetViewModelForGrid(); 
        #endregion
    }
}
