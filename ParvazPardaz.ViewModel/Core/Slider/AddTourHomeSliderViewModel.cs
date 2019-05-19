﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

namespace ParvazPardaz.ViewModel
{
    public class AddTourHomeSliderViewModel : BaseViewModelId
    {
        /// <summary>
        /// عنوان اسلایدر
        /// </summary>
        //[MaxLength(50)]
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string ImageTitle { get; set; }

        /// <summary>
        /// تصویر
        /// </summary>
        [Display(Name = "ImageURL", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public HttpPostedFileBase File { get; set; }

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
        /// شناسه گروه اسلایدر
        /// </summary>
        [Display(Name = "SliderGroupID", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public int SliderGroupID { get; set; }
        /// <summary>
        /// سلکت-لیستی از گروه اسلایدر
        /// </summary>
        public IEnumerable<SelectListItem> SliderGroupDDL { get; set; }
        [Display(Name = "HeaderDays", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [MaxLength(20)]
        public string HeaderDays { get; set; }
        [Display(Name = "NavDescription", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [MaxLength(200)]
        public string NavDescription { get; set; }
        [Display(Name = "Price", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]

        public decimal? Price { get; set; }
        [Display(Name = "footerLine1", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [MaxLength(50)]
        public string footerLine1 { get; set; }
        [Display(Name = "footerLine2", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        [MaxLength(50)]
        public string footerLine2 { get; set; }
        [Display(Name = "Expirationdate", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public DateTime? Expirationdate { get; set; }

    }
}
