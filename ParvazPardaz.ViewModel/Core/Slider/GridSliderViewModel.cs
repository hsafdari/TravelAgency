using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridSliderViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// عنوان اسلایدر
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string ImageTitle { get; set; }

        /// <summary>
        /// تصویر
        /// </summary>
        [Display(Name = "ImageURL", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string ImageURL { get; set; }

        /// <summary>
        /// توضیحات تصویر
        /// </summary>
        [Display(Name = "ImageDescription", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [ScaffoldColumn(false)]
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
        [Display(Name = "NavigationUrl", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [ScaffoldColumn(false)]
        public string NavigationUrl { get; set; }

        /// <summary>
        /// عنوان گروه اسلایدر
        /// </summary>
        [Display(Name = "SliderGroupID", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public string SliderGroupTitle { get; set; }
    }
}
