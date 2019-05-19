using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Tour;

namespace ParvazPardaz.Service.Contract.Tour
{
    public interface IRequiredDocumentService : IBaseService<EntityNS.RequiredDocument>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridRequiredDocumentViewModel> GetViewModelForGrid();
        #endregion
    }
}
