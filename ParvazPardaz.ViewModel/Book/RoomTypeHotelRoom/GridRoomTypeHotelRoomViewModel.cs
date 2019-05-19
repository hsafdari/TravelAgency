using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridRoomTypeHotelRoomViewModel : BaseViewModelOfEntity
    {
        #region Property
        [Display(Name = "MaximumCapacity", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int MaximumCapacity { get; set; }

        [Display(Name = "HotelRoom", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int HotelRoomId { get; set; }

        [Display(Name = "RoomType", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int RoomTypeId { get; set; }
        #endregion
    }
}
