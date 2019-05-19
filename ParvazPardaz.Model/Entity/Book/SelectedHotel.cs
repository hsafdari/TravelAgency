using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Hotel;

namespace ParvazPardaz.Model.Entity.Book
{
    public class SelectedHotel : BaseEntity
    {
        #region Properties
        public DateTime CheckInDateTime { get; set; }
        public DateTime CheckOutDateTime { get; set; }
        public string HotelDescription { get; set; }
        #endregion

        #region reference navigation properties
        public int SelectedHotelPackageId { get; set; }
        public virtual SelectedHotelPackage SelectedHotelPackage { get; set; }

        public int HotelId { get; set; }
        public virtual EntityNS.Hotel Hotel { get; set; }
        #endregion
    }
}
