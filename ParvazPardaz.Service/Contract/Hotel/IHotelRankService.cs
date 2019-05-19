using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Hotel
{
    public interface IHotelRankService : IBaseService<HotelRank>
    {
        #region  GetForGridView
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridHotelRankViewModel> GetViewModelForGrid();
        #endregion

        #region Create
        /// <summary>
        /// افزودن
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<AddHotelRankViewModel> CreateAsync(AddHotelRankViewModel viewModel);
        #endregion

        #region Edit
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> EditAsync(EditHotelRankViewModel viewModel);
        #endregion

        #region GetAllHotelRanksOfSelectListItem
        IEnumerable<SelectListItem> GetAllHotelRanksOfSelectListItem(); 
        #endregion
    }
}
