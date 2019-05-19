using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Entity = ParvazPardaz.Model.Entity.Country;

namespace ParvazPardaz.Service.Contract.Country
{
    public interface IStateService : IBaseService<Entity.State>
    {
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridStateViewModel> GetViewModelForGrid();
        /// <summary>
        /// واکشی استان ها برای کشوری خاص
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        IEnumerable<SelectListItem> FindStatesByCountryId(int? countryId);
        /// <summary>
        /// واکشی شهرها برای کشوری خاص
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        IEnumerable<SelectListItem> FindCitiesByCountryId(int? countryId);
        #region Create
        /// <summary>
        /// افزودن
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<AddStateViewModel> CreateAsync(AddStateViewModel viewModel);
        #endregion

        #region Edit
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> EditAsync(EditStateViewModel viewModel);
        #endregion

        #region CheckIsUniqueTitleInState
        /// <summary>
        /// بررسی وجود عنوان در جدول 
        /// </summary>
        /// <param name="title">عنوان</param>
        /// <returns>آیا این عنوان در جدول وجود ندارد؟</returns>
        bool CheckIsUniqueTitleInState(string title);
        #endregion
    }
}
