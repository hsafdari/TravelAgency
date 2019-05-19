using System;
using System.Collections.Generic;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Tour;

namespace ParvazPardaz.Service.Contract.Tour
{
    public interface IVehicleTypeClassService : IBaseService<VehicleTypeClass>
    {
        #region Properties
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridVehicleTypeClassViewModel> GetViewModelForGrid();
        #endregion
    }
}
