using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    /// <summary>
    /// مورد استفاده در دراپ داون انواع اتاق در صفحه اویلبل - نتایج جستجوی تور
    /// </summary>
    public class HotelRoomDDLViewModel
    {
        #region Constructor
        public HotelRoomDDLViewModel()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// عنوان اتاق
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// شناسه اتاق
        /// </summary>
        public string Value { get; set; }

        public string AdultPrice { get; set; }
        public string ChildPrice { get; set; }
        public string InfantPrice { get; set; }

        public string AdultOtherCurrencyPrice { get; set; }
        public string ChildOtherCurrencyPrice { get; set; }
        public string InfantOtherCurrencyPrice { get; set; }

        public int TotalAdultCapacity { get; set; }
        public int TotalChildCapacity { get; set; }
        public int TotalInfantCapacity { get; set; }
        
        /// <summary>
        /// شناسه ارز
        /// </summary>
        public string CurrencyId { get; set; }

        public int? Priority { get; set; }
        #endregion
    }
}
