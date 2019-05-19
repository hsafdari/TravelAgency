using ParvazPardaz.Model.Entity.Hotel;
using ParvazPardaz.Service.Contract.Common;
using ParvazPardaz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Service.Contract.Hotel
{
    public interface IHotelGalleryService : IBaseService<HotelGallery>
    {
        ///// <summary>
        ///// جهت لود دیتا برای گرید ویو
        ///// </summary>
        ///// <returns></returns>
        //IQueryable<GridHotelGalleryViewModel> GetViewModelForGrid();        

        HotelGallery UpoloadGallery(ImageSliderViewModel viewModel);

        bool RemoveGallery(int id);
    }
}
