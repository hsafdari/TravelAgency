using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel.TourViewModels.Request
{
    public class GridRequestViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// تعداد نوزاد
        /// </summary>
        [Display(Name = "InfantCount", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int InfantCount { get; set; }
        /// <summary>
        /// تعداد کودک
        /// </summary>
        [Display(Name = "ChildCount", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int ChildCount { get; set; }
        /// <summary>
        /// تعداد بزرگسال
        /// </summary>
        [Display(Name = "AdultCount", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public int AdultCount { get; set; }

        /// <summary>
        /// نوع اتاق
        /// </summary>
        [Display(Name = "RoomType", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string RoomType { get; set; }

        /// <summary>
        /// تاریج ایجاد
        /// </summary>
        [Display(Name = "CreatorDateTime", ResourceType = typeof(ParvazPardaz.Resource.General.Generals))]
        public DateTime? CreatorDateTime { get; set; }

        /// <summary>
        /// عنوان تور
        /// </summary>
        [Display(Name = "TourPackageTitle", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string TourPackageTitle { get; set; }

        /// <summary>
        /// عنوان پکیج هتل انتخابی
        /// </summary>
        [Display(Name = "HotelPackageTitle", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public string HotelPackageTitle { get; set; }


        [Display(Name = "UserName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string UserName { get; set; }
        [Display(Name = "Email", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string Email { get; set; }
        [Display(Name = "PhoneNumber", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string PhoneNumber { get; set; }
        [Display(Name = "FullName", ResourceType = typeof(ParvazPardaz.Resource.User.Users))]
        public string FullName { get; set; }

    }
}
