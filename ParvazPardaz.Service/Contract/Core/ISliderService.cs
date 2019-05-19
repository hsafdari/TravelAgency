using ParvazPardaz.Model.Entity.Core;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Core
{
    public interface ISliderService : IBaseService<Slider>
    {
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridSliderViewModel> GetViewModelForGrid();

        #region Create
        /// <summary>
        /// افزودن
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<AddSliderViewModel> CreateAsync(AddSliderViewModel viewModel);
        #endregion

        #region Edit
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> EditAsync(EditSliderViewModel viewModel);
        #endregion
        IQueryable<GridTourHomeSliderViewModel> GetViewModelForGridHome();
        #region Create
        /// <summary>
        /// افزودن
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<AddTourHomeSliderViewModel> CreateAsyncHome(AddTourHomeSliderViewModel viewModel);
        #endregion
        #region Edit
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> EditAsyncHome(EditTourHomeSliderViewModel viewModel);
        #endregion
        IQueryable<GridTourHomeSliderViewModel> GetViewModelForGridTourLanding();
    }
}
