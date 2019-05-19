using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel.NotFoundLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Link;

namespace ParvazPardaz.Service.Contract.NotFoundLink
{
    public interface INotFoundLinkService : IBaseService<EntityNS.NotFoundLink>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridNotFoundLinkViewModel> GetViewModelForGrid();
        #endregion
    }
}
