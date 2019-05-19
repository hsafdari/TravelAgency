using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Book;

namespace ParvazPardaz.Service.Contract.Book
{
    public interface ITaskService : IBaseService<EntityNS.Task>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridTaskViewModel> GetViewModelForGrid();
        #endregion
    }
}
