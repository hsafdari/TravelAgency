using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    /// <summary>
    /// هر ردیف اتاق در ویوو
    /// </summary>
    public class RoomSectionViewModel
    {
        #region Constructor
        public RoomSectionViewModel()
        {

        }
        #endregion

        #region Properties
        /// <summary>
        /// نوع اتاق انتخاب شده
        /// </summary>
        public int SelectedRoomTypeId { get; set; }

        /// <summary>
        /// تعداد بزرگسال
        /// </summary>
        public int AdultCount { get; set; }

        /// <summary>
        /// تعداد کودک
        /// </summary>
        public int ChildCount { get; set; }

        /// <summary>
        /// تعداد نوزاد
        /// </summary>
        public int InfantCount { get; set; } 
        #endregion
    }
}
