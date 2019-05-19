using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Book
{
    public interface IOrderInformationService : IBaseService<OrderInformation>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridOrderInformationViewModel> GetViewModelForGrid();
        #endregion
    }
}
