using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Book
{
    public class RoomTypeHotelRoom : BaseEntity
    {
        #region Property
        public int MaximumCapacity { get; set; }
        #endregion

        #region Reference navigation Properties
        public int HotelRoomId { get; set; }
        public virtual HotelRoom HotelRoom { get; set; }

        public int RoomTypeId { get; set; }
        public virtual RoomType RoomType { get; set; }
        #endregion
    }
}
