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
    public interface ISelectedHotelPackageService : IBaseService<SelectedHotelPackage>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridSelectedHotelPackageViewModel> GetViewModelForGrid();
        IQueryable<GridSelectedHotelPackageViewModel> GetViewModelForGrid(DateTime? fromdate, DateTime? todate, string reporttype, int? cityid, int? hotelid);
        IQueryable<GridSelectedHotelPackageViewModel> GetViewModelForGrid(string fromdate, string todate, string reporttype, int? cityid, int? hotelid, string calendertype);
        #endregion
    }
}
