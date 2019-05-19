using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    /// <summary>
    /// اتاق ها و اطلاعات قیمت و تعداد افرادی به تفکیک رده سنی
    /// مورد استفاده در صفحه تایید اطلاعات
    /// </summary>
    public class RoomTypePriceInfoViewModel
    {
        #region Constructor
        public RoomTypePriceInfoViewModel()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// عناون اتاق
        /// </summary>
        public string RoomTypeTitle { get; set; }
        
        /// <summary>
        /// رده سنی
        /// </summary>
        public string AgeRangeTitle { get; set; }

        /// <summary>
        /// تعداد این رده سنی
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// قیمت با توجه به تعداد این رده سنی
        /// </summary>
        public decimal Price { get; set; }
        #endregion
    }
}
