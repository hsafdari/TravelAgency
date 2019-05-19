using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Tour
{
    public interface ICurrencyService : IBaseService<Currency>
    {
        #region  GetForGridView
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridCurrencyViewModel> GetViewModelForGrid();
        #endregion

        #region GetAllCurrenciesOfSelectListItem
        IEnumerable<SelectListItem> GetAllCurrenciesOfSelectListItem(); 
        IEnumerable<SelectListItem> GetAllCurrenciesOfSelectListItem(int selectedId);
        #endregion
    }
}
