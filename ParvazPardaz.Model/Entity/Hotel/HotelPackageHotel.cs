using System;
using System.Collections.Generic;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Hotel;

namespace ParvazPardaz.Model.Entity.Hotel
{
    public class HotelPackageHotel : BaseEntity
    {
        #region Reference navigation properties
        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }

        public int HotelPackageId { get; set; }
        public virtual HotelPackage HotelPackage { get; set; }

        public Nullable<int> HotelBoardId { get; set; }
        public virtual HotelBoard HotelBoard { get; set; }
        #endregion
    }
}
