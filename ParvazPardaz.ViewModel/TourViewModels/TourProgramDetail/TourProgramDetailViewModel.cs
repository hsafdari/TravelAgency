using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class TourProgramDetailViewModel
    {
        /// <summary>
        /// شناسه فعالیت تور
        /// </summary>
        [Display(Name = "Activity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int TourActivityId { get; set; }        
        /// <summary>
        /// عکسی از مکان فعالیت
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
        /// اندازه فایل
        /// </summary>
        public long ImageSize { get; set; }
        /// <summary>
        /// دراپ داونی از فعالیت ها
        /// </summary>
        public SelectList ActivitySelectList { get; set; }

        #region قبلی
        /// <summary>
        /// عنوان برنامه سفر
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string Title { get; set; }
        /// <summary>
        /// آپلود 
        /// </summary>
        [Display(Name = "File", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public HttpPostedFileBase File { get; set; } 
        #endregion

        /// <summary>
        /// توضیح
        /// </summary>
        //[UIHint("TinyMCE_Modern")]
        [AllowHtml]
        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        //public string Description { get; set; }
        public string DetailDescription { get; set; }
      
        /// <summary>
        /// آیدی برنامه سفر
        /// </summary>
        public int TourProgramId { get; set; }
        /// <summary>
        /// نشان میدهد در چه مدی است؟
        /// </summary>
        public CRUDMode CRUDMode { get; set; }
        /// <summary>
        /// آیدی تگ دیو هر بخش اضافه شده 
        /// </summary>
        public string SectionId { get; set; }
        /// <summary>
        /// لیستی از بخش های افزوده شده جهت نمایش در صفحه در حالت رفرش صفحه
        /// </summary>
        public List<ListViewTourProgramDetailViewModel> ListView { get; set; }
    }
}
