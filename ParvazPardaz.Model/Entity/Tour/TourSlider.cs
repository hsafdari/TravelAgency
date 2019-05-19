using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// اسلایدر تور
    /// </summary>
    public class TourSlider : BaseEntity
    {
        #region Properties

        /// <summary>
        /// مسیر عکسی از تور
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// پسوند عکس
        /// </summary>
        public string ImageExtension { get; set; }
        /// <summary>
        /// نام فایل عکس
        /// </summary>
        public string ImageFileName { get; set; }
        /// <summary>
        /// اندازه عکس
        /// </summary>
        public long ImageSize { get; set; }
        /// <summary>
        /// عکس یرای اسلایدر اصلی بالای صفحه است ؟
        /// </summary>
        public bool IsPrimarySlider { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<Tour> Tours { get; set; }
        #endregion
    }
}
