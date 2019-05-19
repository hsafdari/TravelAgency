using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Tour
{
    public interface ITourService : IBaseService<Model.Entity.Tour.Tour>
    {
        #region GetViewModelForGrid
        IQueryable<GridTourViewModel> GetViewModelForGrid();
        #endregion

        #region CreateTour
        ParvazPardaz.Model.Entity.Tour.Tour CreateTour(AddTourViewModel addTourViewModel);
        #endregion

        #region UpdateTour
        ParvazPardaz.Model.Entity.Tour.Tour UpdateTour(EditTourViewModel viewModel); 
        #endregion

        #region CheckURLInLinkTable
        /// <summary>
        /// بررسی وجود آدرس تور در جدول لینک-تیبل
        /// </summary>
        /// <param name="title">عنوان تور</param>
        /// <returns>آیا با این عنوان، آدرس تور در جدول لینک-تیبل وجود ندارد؟</returns>
        bool CheckURLInLinkTable(string LinkTableTitle); 
        #endregion

        #region RelatedTours
        IList<PostListDetailViewModel> RelatedTours(int cityId);
        #endregion

        #region SearchTours
        List<TabMagPost> SearchTours(string q);
        #endregion

        #region GetTourDDL
        SelectList GetTourDDL();
        #endregion
    }
}
