using ParvazPardaz.Model.Entity.Link;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Link
{
    public interface ILinkService : IBaseService<LinkTable>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridLinkViewModel> GetViewModelForGrid();
        #endregion
        #region Edit
        string UniqEdit(EditLinkViewModel viewModel);

        # endregion
    }
}
