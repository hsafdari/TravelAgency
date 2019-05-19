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
    public interface IAgencySettingService : IBaseService<AgencySetting>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridAgencySettingViewModel> GetViewModelForGrid();
        #endregion

        #region GetAllAgencySettingOfSelectListItem
        //IEnumerable<SelectListItem> GetAllAgencySettingOfSelectListItem();
        #endregion

        #region Create
        /// <summary>
        /// افزودن
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<AddAgencySettingViewModel> CreateAsync(AddAgencySettingViewModel viewModel);
        #endregion

        #region Edit
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> EditAsync(EditAgencySettingViewModel viewModel);
        #endregion
    }
}
