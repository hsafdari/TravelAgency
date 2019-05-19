using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class GridCityViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// نام شهر
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }

        /// <summary>
        /// عنوان به زبان انگلیسی
        /// </summary>
        [Display(Name = "ENTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string ENTitle { get; set; }

        /// <summary>
        /// تصویری از شهر 
        /// </summary>
        [Display(Name = "Image", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Image { get; set; }
        /// <summary>
        /// نام استان
        /// </summary>
        [Display(Name = "State", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string State { get; set; }
        /// <summary>
        /// نام کشور
        /// </summary>
        [Display(Name = "Country", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Country { get; set; }
        [Display(Name = "IsDddlFrom", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool IsDddlFrom { get; set; }
        [Display(Name = "IsDddlDestination", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public bool IsDddlDestination { get; set; }
    }
}
