using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridCompanyTransferViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// نام شرکت مسافربری
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }
        /// <summary>
        /// عنوان انگلیسی
        /// </summary>
        [Display(Name = "TitleEn", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string TitleEn { get; set; }
        /// <summary>
        /// کد یاتا
        /// </summary>
        [Display(Name = "IataCode", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string IataCode { get; set; }

        /// <summary>
        /// شماره تلفن شرکت
        /// </summary>
        [Display(Name = "Tel", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Tel { get; set; }

        /// <summary>
        /// آدرس 
        /// </summary>
        [Display(Name = "Address", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Address { get; set; }

        /// <summary>
        /// آپلود عکس 
        /// </summary>
        [Display(Name = "Image", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Image { get; set; }
    }
}
