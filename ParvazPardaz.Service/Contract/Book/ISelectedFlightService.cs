using ParvazPardaz.Model.Entity.Book;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Book
{
    public interface ISelectedFlightService : IBaseService<SelectedFlight>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridSelectedFlightViewModel> GetViewModelForGrid(DateTime? fromdate, DateTime? todate, string reporttype);
        #endregion



        IQueryable<GridSelectedFlightViewModel> GetViewModelForGrid(DateTime? fromdate, DateTime? todate, string reporttype, int? companytranferId, string fromairport, string toairport);

        IQueryable<GridSelectedFlightViewModel> GetViewModelForGrid(string fromdate, string todate, string reporttype, int? companytranferId, string fromairport, string toairport, string calendertype);
    }
}
