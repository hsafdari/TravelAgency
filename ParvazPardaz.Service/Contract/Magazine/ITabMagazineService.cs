using ParvazPardaz.Model.Entity.Magazine;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel.Magazine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.DataAccessService.Magazine
{
    public interface ITabMagazineService : IBaseService<TabMagazine>
    {
        #region GetViewModelForGrid
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridTabMagazineViewModel> GetViewModelForGrid(); 
        #endregion

        #region CreateTabMagAsync
        Task<bool> CreateTabMagAsync(AddTabMagazineViewModel addTabMagazineViewModel); 
        #endregion

        #region UpdateTabMagAsync
        Task<bool> UpdateTabMagAsync(EditTabMagazineViewModel editTabMagazineViewModel); 
        #endregion
    }
}
