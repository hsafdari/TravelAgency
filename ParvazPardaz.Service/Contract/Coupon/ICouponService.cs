using System;
using System.Collections.Generic;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity;

namespace ParvazPardaz.Service.Contract
{
    public interface ICouponService : IBaseService<Coupon>
    {
        #region Properties
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridCouponViewModel> GetViewModelForGrid();
        #endregion
    }
}
