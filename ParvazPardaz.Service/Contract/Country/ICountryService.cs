using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Country
{
    public interface ICountryService : IBaseService<ParvazPardaz.Model.Entity.Country.Country>
    {
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridCountryViewModel> GetViewModelForGrid();

        #region Create
        /// <summary>
        /// افزودن
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<AddCountryViewModel> CreateAsync(AddCountryViewModel viewModel);
        #endregion

        #region Edit
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> EditAsync(EditCountryViewModel viewModel);
        #endregion

        #region GetAllCountryOfSelectListItem
        IEnumerable<SelectListItem> GetAllCountryOfSelectListItem();
        #endregion

        #region CheckIsUniqueTitleInCountry
        /// <summary>
        /// بررسی وجود عنوان در جدول 
        /// </summary>
        /// <param name="title">عنوان </param>
        /// <returns>آیا این عنوان در جدول وجود ندارد؟</returns>
        bool CheckIsUniqueTitleInCountry(string title);
        #endregion
    }
}
