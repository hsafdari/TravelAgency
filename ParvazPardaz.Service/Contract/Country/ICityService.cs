using ParvazPardaz.Model.Entity.Country;
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
    public interface ICityService : IBaseService<City>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridCityViewModel> GetViewModelForGrid();
        #endregion

        #region SearchEngine
        IEnumerable<SelectListItem> GetAllFromNationalDDL();
        IEnumerable<SelectListItem> GetAllDestinationNationalDDL();
        IEnumerable<SelectListItem> GetAllFromDomesticDDL();
        IEnumerable<SelectListItem> GetAllDestinationDomesticDDL();
        IEnumerable<SelectListItem> GetAvailableFromCitiesDDL();
        IEnumerable<SelectListItem> GetAvailableDestCitiesDDL();
        //get cityId for select
        IEnumerable<SelectListItem> GetAvailableFromCitiesDDL(int id);
        IEnumerable<SelectListItem> GetAvailableDestCitiesDDL(int id);

        #endregion
        #region GetAllCityOfSelectListItem
        IEnumerable<SelectListItem> GetAllCityOfSelectListItem();
        IEnumerable<SelectListItem> GetAllCityOfSelectListItem(int modelCityId);

        #endregion

        #region Create
        /// <summary>
        /// افزودن
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<AddCityViewModel> CreateAsync(AddCityViewModel viewModel);
        #endregion

        #region Edit
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> EditAsync(EditCityViewModel viewModel);
        #endregion

        #region ShowCityNameWithCountry
        /// <summary>
        /// نمایش شهر با نام کشور
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        string ShowCityNameWithCountry(int cityId);
        #endregion

        #region ShowCityNameWithCountryAndState
        /// <summary>
        /// نمایش شهر با استان و کشور
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        string ShowCityNameWithCountryAndState(int cityId);
        #endregion

        #region GetCityName
        string GetCityName(int cityId);
        string GetCityNameWithCountryEN(int cityId);
        #endregion

        #region CheckIsUniqueTitleInCity
        /// <summary>
        /// بررسی وجود عنوان در جدول شهر
        /// </summary>
        /// <param name="title">عنوان انگلیسی شهر</param>
        /// <returns>آیا این عنوان انگلیسی در جدول وجود ندارد؟</returns>
        bool CheckIsUniqueTitleInCity(string title);
        #endregion

        #region CheckIsUniqueENTitleInCity
        /// <summary>
        /// بررسی وجود عنوان در جدول شهر
        /// </summary>
        /// <param name="title">عنوان انگلیسی شهر</param>
        /// <returns>آیا این عنوان انگلیسی در جدول وجود ندارد؟</returns>
        bool CheckIsUniqueENTitleInCity(string entitle);
        #endregion
    }
}
