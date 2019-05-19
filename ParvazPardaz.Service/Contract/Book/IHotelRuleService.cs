using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Book
{
    public interface IHotelRuleService : IBaseService<HotelRule>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridHotelRuleViewModel> GetViewModelForGrid();
        #endregion
    }
}
