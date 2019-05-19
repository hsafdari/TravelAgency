﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class EditActivityViewModel:BaseViewModelId
    {
        [StringLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }
        [StringLength(250, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        [Display(Name = "Place", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Place { get; set; }
        [Display(Name = "File", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public HttpPostedFileBase File { get; set; }
        [Display(Name = "Description", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Description { get; set; }
        /// <summary>
        /// مسیر عکسی از لوگوی شرکت
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// پسوند عکس لوگو
        /// </summary>
        public string ImageExtension { get; set; }
        /// <summary>
        /// نام فایل عکس لوگو
        /// </summary>
        public string ImageFileName { get; set; }
        /// <summary>
        /// اندازه فایل
        /// </summary>
        public long ImageSize { get; set; }
    }
}
