using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ParvazPardaz.ViewModel
{
    public class SlidersUITourHomeViewModel
    {
        public SlidersUITourHomeViewModel()
        {

        }
        /// <summary>
        /// عنوان اسلایدر
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string ImageTitle { get; set; }

        public string ImageURL { get; set; }

        /// <summary>
        /// توضیحات تصویر
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string ImageDescription { get; set; }

        /// <summary>
        /// فعال/غیرفعال
        /// </summary>
        [Display(Name = "IsActive", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public bool ImageIsActive { get; set; }

        /// <summary>
        /// اولویت نمایش اسلایدر
        /// </summary>
        [Display(Name = "Priority", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public int Priority { get; set; }

        /// <summary>
        /// آدرسی که اسلایدر به آن اشاره خواهد کرد
        /// </summary>
        [MaxLength(400)]
        [Display(Name = "NavigationUrl", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string NavigationUrl { get; set; }

        /// <summary>
        /// عنوان گروه اسلایدر
        /// </summary>
        [Display(Name = "SliderGroupTitle", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string SliderGroupTitle { get; set; }
        [Display(Name = "HeaderDays", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string HeaderDays { get; set; }
        [Display(Name = "NavDescription", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string NavDescription { get; set; }
        [Display(Name = "Price", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public decimal? Price { get; set; }
        [Display(Name = "footerLine1", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string footerLine1 { get; set; }
        [Display(Name = "footerLine2", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string footerLine2 { get; set; }

        public int dayId { get; set; }
        public int cityId { get; set; }
    }
}
