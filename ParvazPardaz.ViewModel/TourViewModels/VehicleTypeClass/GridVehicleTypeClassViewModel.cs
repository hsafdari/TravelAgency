using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class GridVehicleTypeClassViewModel : BaseViewModelOfEntity
    {
        #region Properties
        /// <summary>
        /// عنوان فارسی
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }
        /// <summary>
        /// عنوان انگلیسی
        /// </summary>
        [Display(Name = "TitleEn", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TitleEn { get; set; }
        /// <summary>
        /// کد سه حرفی
        /// </summary>
        [Display(Name = "ShortCode", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Code { get; set; }
        /// <summary>
        /// نوع وسیله نقلیه
        /// </summary>
        [Display(Name = "VehicleType", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string VehicleTypeTitle { get; set; }
        #endregion
    }
}
