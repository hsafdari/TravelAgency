using ParvazPardaz.Model.Entity.Tour;
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
    public interface IAdditionalServiceService : IBaseService<AdditionalService>
    {
        /// <summary>
        /// جهت لود دیتا برای گرید ویو
        /// </summary>
        /// <returns></returns>
        IQueryable<GridAdditionalServiceViewModel> GetViewModelForGrid();
        /// <summary>
        /// گرفتن سلکت-لیستی از خدمات اضافی تور
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllAdditionalServiceOfSelectListItem();
    }
}
