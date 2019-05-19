using System;
using System.Collections.Generic;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Book;

namespace ParvazPardaz.Model.Entity.Tour
{
    public class VehicleTypeClass : BaseEntity
    {
        #region Properties
        /// <summary>
        /// عنوان فارسی
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// عنوان انگلیسی
        /// </summary>
        public string TitleEn { get; set; }
        /// <summary>
        /// کد سه حرفی
        /// </summary>
        public string Code { get; set; }
        #endregion

        #region Reference navigation props
        /// <summary>
        /// نوع وسیله نقلیه
        /// </summary>
        public int VehicleTypeId { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        #endregion

        #region Collection navigation properties
        public virtual ICollection<TourScheduleCompanyTransfer> TourScheduleCompanyTransfers { get; set; }
        public virtual ICollection<SelectedFlight> SelectedFlights { get; set; }
        #endregion
    }
}
