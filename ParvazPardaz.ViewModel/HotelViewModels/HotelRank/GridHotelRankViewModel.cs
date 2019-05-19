using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridHotelRankViewModel:BaseViewModelOfEntity
    {
        /// <summary>
        /// عنوان رتبه هتل
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// ایکون رتبه هتل
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// ترتیب رتبه هتل
        /// </summary>
        public int OrderId { get; set; }
    }
}
