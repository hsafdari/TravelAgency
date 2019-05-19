using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.ViewModel;
using ParvazPardaz.Model.Entity.Core;
using ParvazPardaz.Service.Contract.Common;

namespace ParvazPardaz.Service.Contract.Core
{
    public interface IMenuGroupService : IBaseService<MenuGroup>
    {
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridMenuGroupViewModel> GetViewModelForGrid();
    }
}
