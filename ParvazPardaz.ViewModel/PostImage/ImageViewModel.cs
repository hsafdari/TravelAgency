using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
   public class ImageViewModel:BaseViewModelId
    {
        public string ImageTitle { get; set; }
        public string ImageDesc { get; set; }
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

        public int Width { get; set; }
        public int Height { get; set; }
        /// <summary>
        /// عکس یرای اسلایدر اصلی بالای صفحه است ؟
        /// </summary>
        public bool IsPrimarySlider { get; set; }
    }
}
