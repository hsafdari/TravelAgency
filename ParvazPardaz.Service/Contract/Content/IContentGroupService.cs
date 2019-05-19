using ParvazPardaz.Model.Entity.Content;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Content
{
    public interface IContentGroupService : IBaseService<ContentGroup>
    {
        #region Grid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridContentGroupViewModel> GetViewModelForGrid();
        #endregion

        #region GetContentGroupDDL
        SelectList GetContentGroupDDL();
        SelectList GetContentGroupDDL(int selectedGroupId);
        #endregion
    }
}
