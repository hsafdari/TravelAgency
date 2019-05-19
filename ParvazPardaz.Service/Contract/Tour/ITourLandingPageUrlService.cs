using ParvazPardaz.Model.Entity.Tour;
using ParvazPardaz.Model.Enum;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.Service.Contract.Tour
{
    public interface ITourLandingPageUrlService : IBaseService<TourLandingPageUrl>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridTourLandingPageUrlViewModel> GetViewModelForGrid(); 
        #endregion

        #region FindUrlsByCityId
        IEnumerable<SelectListItem> FindUrlsByCityId(int? cityId, EnumLandingPageUrlType landingPageUrlType);
        #endregion

        #region FindUrlsByCityIdEditMode
        IEnumerable<SelectListItem> FindUrlsByCityIdEditMode(int? cityId, int? previousUrlId, EnumLandingPageUrlType landingPageUrlType);
        #endregion


        #region CheckURLInLinkTable
        bool CheckURLInLinkTable(string Url);
        #endregion
    }
}
