using ParvazPardaz.Model;
using ParvazPardaz.Model.Entity.Core;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ParvazPardaz.Service.Contract.Core
{
    public interface INewsLetterService : IBaseService<Newsletter>
    {
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridNewsLetterViewModel> GetViewModelForGrid();

        HttpCookie setCookie();
    }
}
