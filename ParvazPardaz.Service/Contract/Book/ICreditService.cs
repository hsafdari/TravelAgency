using System;
using System.Collections.Generic;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Book;
using ParvazPardaz.Model.Entity.Book;

namespace ParvazPardaz.TravelAgency.UI.Services.Interface.Book
{
    public interface ICreditService : IBaseService<Credit>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        //IQueryable<GridCreditViewModel> GetViewModelForGrid();
        #endregion

        #region بعد پرداخت اعتباری
        /// <summary>
        /// لاگ بعد پرداخت
        /// </summary>
        /// <param name="newOrder"></param>
        void LogCreditAfterPayment(Order newOrder);
        #endregion

    }
}
