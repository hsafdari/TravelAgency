using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Hotel
{
    public class HotelGallery : BaseEntity
    {
        #region Properties
        /// <summary>
        /// عنوان تصویر *
        /// </summary>
        public string ImageTitle { get; set; }
        /// <summary>
        /// توضیحات تصویر *
        /// </summary>
        public string ImageDesc { get; set; }
        /// <summary>
        /// نام فایل عکس
        /// </summary>
        public string ImageFileName { get; set; }
        /// <summary>
        /// مسیر عکسی از هتل
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// پسوند عکس
        /// </summary>
        public string ImageExtension { get; set; }
        /// <summary>
        /// اندازه فایل
        /// </summary>
        public long ImageSize { get; set; }
        /// <summary>
        /// طول تصویر *
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// عرض تصویر *
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// عکس یرای اسلایدر اصلی بالای صفحه است ؟ *
        /// </summary>
        public bool IsPrimarySlider { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<Hotel> Hotels { get; set; }
        #endregion
    }
}
