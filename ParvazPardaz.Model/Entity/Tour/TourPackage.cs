using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Hotel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    public class TourPackage : BaseEntity
    {
        #region Properties
        public string Title { get; set; }
        public string DateTitle { get; set; }
        public string Code { get; set; }
        public Nullable<int> OwnerId { get; set; }
        public string FromPrice { get; set; }
        public string OfferPrice { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        #endregion

        #region Reference Navigation Properties
        public virtual Tour Tour { get; set; }
        public int TourId { get; set; }
        public virtual TourPackageDay TourPackageDay { get; set; }
        public Nullable<int> TourPackgeDayId { get; set; }
        #endregion

        #region Collection Navigation Property
        public virtual ICollection<TourSchedule> TourSchedules { get; set; }
        public virtual ICollection<HotelPackage> HotelPackages { get; set; }
        #endregion
    }
}
