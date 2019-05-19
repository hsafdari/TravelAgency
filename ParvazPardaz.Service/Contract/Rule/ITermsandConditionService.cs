using System;
using System.Collections.Generic;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Rule;

namespace ParvazPardaz.Service.Contract.Rule
{
    public interface ITermsandConditionService : IBaseService<TermsandCondition>
    {
        #region Properties
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridTermsandConditionViewModel> GetViewModelForGrid();
        #endregion
    }
}
