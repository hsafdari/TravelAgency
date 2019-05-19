using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Service.Contract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.ViewModel;

namespace ParvazPardaz.Service.Contract.Book
{
    public interface IOrderTypeService : IBaseService<OrderType>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridOrderTypeViewModel> GetViewModelForGrid(); 
        #endregion
    }
}
