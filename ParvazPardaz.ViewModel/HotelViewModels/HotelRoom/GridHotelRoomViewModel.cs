using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridHotelRoomViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// نام نوع اتاق
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }
        
        /// <summary>
        /// نوع اتاق های اصلی را مشخص میکند که به صورت پیش فرض نمایش داده میشود
        /// </summary>
        [Display(Name = "IsPrimary", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// ترتیب نمایش سمت کاربر
        /// </summary>
        [Display(Name = "Priority", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public Nullable<int> Priority { get; set; }

        /// <summary>
        /// بزرگسال؟
        /// </summary>
        [Display(Name = "HasAdult", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool HasAdult { get; set; }

        /// <summary>
        /// کودک؟
        /// </summary>
        [Display(Name = "HasChild", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool HasChild { get; set; }

        /// <summary>
        /// نوزاد؟
        /// </summary>
        [Display(Name = "HasInfant", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool HasInfant { get; set; }


        /// <summary>
        /// حداکثر ظرفیت بزرگسال
        /// </summary>
        [Display(Name = "AdultMaxCapacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int AdultMaxCapacity { get; set; }

        /// <summary>
        /// حداقل ظرفیت بزرگسال
        /// </summary>
        [Display(Name = "AdultMinCapacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int AdultMinCapacity { get; set; }

        /// <summary>
        /// حداکثر ظرفیت کودک
        /// </summary>
        [Display(Name = "ChildMaxCapacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int ChildMaxCapacity { get; set; }

        /// <summary>
        /// حداقل ظرفیت کودک
        /// </summary>
        [Display(Name = "ChildMinCapacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int ChildMinCapacity { get; set; }

        /// <summary>
        /// حداکثر ظرفیت نوزاد
        /// </summary>
        [Display(Name = "InfantMaxCapacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int InfantMaxCapacity { get; set; }

        /// <summary>
        /// حداقل ظرفیت نوزاد
        /// </summary>
        [Display(Name = "InfantMinCapacity", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public int InfantMinCapacity { get; set; }
    }
}
