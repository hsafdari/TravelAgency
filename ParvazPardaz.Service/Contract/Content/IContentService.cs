using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EntityNS = ParvazPardaz.Model.Entity.Content;

namespace ParvazPardaz.Service.Contract.Content
{
    public interface IContentService : IBaseService<EntityNS.Content>
    {
        #region Grid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridContentViewModel> GetViewModelForGrid(); 
        #endregion

        #region Create
        /// <summary>
        /// افزودن
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<AddContentViewModel> CreateAsync(AddContentViewModel viewModel);
        #endregion

        #region Edit
        /// <summary>
        /// ویرایش
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        Task<int> EditAsync(EditContentViewModel viewModel);
        #endregion
    }
}
