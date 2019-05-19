using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class MoreDataViewModel
    {
        #region Constructor
        public MoreDataViewModel()
        {
            BlogTopBigSlider = new List<SlidersUIViewModel>();
            HotelList = new List<HotelDetailsViewModel>();
            MorePosts = new List<PostDetailViewModel>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// اسلایدر بزرگ بالای صفحه اصلی مجله گردشگری
        /// </summary>
        public IList<SlidersUIViewModel> BlogTopBigSlider { get; set; }

        /// <summary>
        /// هتل ها
        /// </summary>
        public IList<HotelDetailsViewModel> HotelList { get; set; }

        /// <summary>
        /// مطالب بیشتر
        /// </summary>
        public IList<PostDetailViewModel> MorePosts { get; set; }

        public string SearchTitle { get; set; }
        public ParvazPardaz.Model.Enum.OrderBy OrderByPost { get; set; }
        #endregion
    }
}
