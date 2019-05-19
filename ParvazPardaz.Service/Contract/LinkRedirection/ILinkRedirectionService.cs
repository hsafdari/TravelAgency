using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Link;

namespace ParvazPardaz.Service.Contract.LinkRedirection
{
    public interface ILinkRedirectionService : IBaseService<EntityNS.LinkRedirection>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridLinkRedirectionViewModel> GetViewModelForGrid();
        #endregion
    }
}
