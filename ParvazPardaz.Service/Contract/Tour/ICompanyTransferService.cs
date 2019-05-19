using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Tour
{
    public interface ICompanyTransferService : IBaseService<CompanyTransfer>
    {
        #region  GetForGridView
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridCompanyTransferViewModel> GetViewModelForGrid();
        #endregion

        #region GetAllCompanyTransferOfSelectListItem
        IEnumerable<SelectListItem> GetAllCompanyTransferOfSelectListItem(); 
        #endregion

        #region Create
        /// <summary>
        /// افزودن
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<AddCompanyTransferViewModel> CreateAsync(AddCompanyTransferViewModel viewModel);
        #endregion

        #region Edit
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> EditAsync(EditCompanyTransferViewModel viewModel);
        #endregion
    }
}
