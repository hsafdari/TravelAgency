using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Magazine
{
    /// <summary>
    /// مکان های مورد استفاده در تب های مجله گردشگری
    /// باکس اسلایدر صفحه اول
    /// </summary>
    public class Location : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام مکان به فارسی
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// نام مکان به زبان انگلیسی
        /// </summary>
        public string ENTitle { get; set; }

        /// <summary>
        /// آدرس
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// متن سئو
        /// </summary>
        public string SeoText { get; set; }
        public string ImageURL { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<TabMagazine> TabMagazines { get; set; }
        public virtual ICollection<TourSuggestion> TourSuggestions { get; set; }
        #endregion
    }
}
