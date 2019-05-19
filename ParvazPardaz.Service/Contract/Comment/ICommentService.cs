using EntityType = ParvazPardaz.Model.Entity.Comment;
//using ParvazPardaz.Model.Entity.Product;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Product
{
    public interface ICommentService : IBaseService<EntityType.Comment>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridCommentViewModel> GetViewModelForGrid();
        #endregion
    }
}
