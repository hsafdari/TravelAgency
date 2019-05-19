using ParvazPardaz.Service.Contract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Magazine;
using ParvazPardaz.ViewModel;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Location
{
    public interface ILocationService : IBaseService<EntityNS.Location>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridLocationViewModel> GetViewModelForGrid();
        #endregion
        /// <summary>
        /// افزودن
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<AddLocationViewModel> CreateAsync(AddLocationViewModel viewModel);
        #region Edit
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> EditAsync(EditLocationViewModel viewModel);
        #endregion
        #region GetAllCountryOfSelectListItem
        IEnumerable<SelectListItem> GetAllLocationOfSelectListItem();
        #endregion

        #region CheckURLInLinkTable
        bool CheckURLInLinkTable(string Url);
        #endregion
    }
}
