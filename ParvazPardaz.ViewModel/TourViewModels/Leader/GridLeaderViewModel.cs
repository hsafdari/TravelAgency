using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridLeaderViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// نام 
        /// </summary>
        [Display(Name = "LastName", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        [Display(Name = "LastName", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string LastName { get; set; }
        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        [Display(Name = "FullName", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string FullName { get; set; }
        /// <summary>
        /// جنسیت
        /// </summary>
        [Display(Name = "Gender", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public Gender Sex { get; set; }
        /// <summary>
        /// شماره تلفن
        /// </summary>
        [Display(Name = "Tel", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Tel { get; set; }
        /// <summary>
        /// شماره موبایل 
        /// </summary>
        [Display(Name = "Mobile", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Mobile { get; set; }
    }
}
