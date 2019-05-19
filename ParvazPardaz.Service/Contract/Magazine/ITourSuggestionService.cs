using System;
using System.Collections.Generic;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using EntityNS = ParvazPardaz.Model.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Magazine;

namespace ParvazPardaz.Service.Contract.Magazine
{
    public interface ITourSuggestionService : IBaseService<TourSuggestion>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridTourSuggestionViewModel> GetViewModelForGrid();
        #endregion

        #region Create
        /// <summary>
        /// افزودن
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<AddTourSuggestionViewModel> CreateAsync(AddTourSuggestionViewModel viewModel);
        #endregion

        #region Edit
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> EditAsync(EditTourSuggestionViewModel viewModel);
        #endregion
        IQueryable<TourSuggestionViewModel> GetSuggestions(int id);
    }
}
