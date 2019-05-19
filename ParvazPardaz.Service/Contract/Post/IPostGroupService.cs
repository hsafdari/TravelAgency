using ParvazPardaz.Model.Entity.Post;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Post
{
    public interface IPostGroupService : IBaseService<PostGroup>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridPostGroupViewModel> GetViewModelForGrid();
        #endregion

        #region GetAllTabGroupOfSelectListItem
        IEnumerable<SelectListItem> GetAllPostGroupOfSelectListItem();
        #endregion

        #region GetAllNullParentPostGroupOfSelectListItem
        IEnumerable<SelectListItem> GetAllNullParentPostGroupOfSelectListItem();
        #endregion

        #region GetAllnotNullParentPostGroupOfSelectListItem
        IEnumerable<SelectListItem> GetAllnotNullParentPostGroupOfSelectListItem();
        #endregion

        #region TourGroupDDL
        MultiSelectList GetTourGroupMSL();
        #endregion

        #region SeedDatabase
        /// <summary>
        /// مقدار دهی اولیه جدول گروه پست ها
        /// </summary>
        void SeedDatabase(); 
        #endregion
    }
}
