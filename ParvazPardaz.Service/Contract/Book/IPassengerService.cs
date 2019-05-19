using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Book
{
    public interface IPassengerService : IBaseService<Passenger>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridPassengerViewModel> GetViewModelForGrid();
        #endregion

        #region GetViewModelForGrid
        IQueryable<GridPassengerViewModel> GetViewModelForGrid(DateTime? fromdate, DateTime? todate, string reporttype);
        IQueryable<GridPassengerViewModel> GetViewModelForGrid(string fromdate, string todate, string reporttype, string calendertype);
        #endregion

        #region Age range DDL for logged in user
        /// <summary>
        /// تمامی مسافران مرتبط با رزروهای کاربر لاگین شده
        /// </summary>
        /// <returns></returns>
        SelectList GetPreviousPassengerDDL(int loggedInUserId, AgeRange ageRange);
        #endregion
    }
}
