using System;
using System.Collections.Generic;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Hotel;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Hotel
{
    public interface IHotelBoardService : IBaseService<HotelBoard>
    {
        #region Properties
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridHotelBoardViewModel> GetViewModelForGrid();
        #endregion

        #region Create
        /// <summary>
        /// افزودن
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<AddHotelBoardViewModel> CreateAsync(AddHotelBoardViewModel viewModel);
        #endregion

        #region Edit
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> EditAsync(EditHotelBoardViewModel viewModel);
        #endregion
        #region GetDDL
        SelectList GetDDL();
        #endregion
    }
}
