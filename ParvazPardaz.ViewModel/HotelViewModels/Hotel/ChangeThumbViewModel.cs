using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class ChangeThumbViewModel
    {
        public ChangeThumbViewModel()
        {
            ThumbImageURLRadioButtons = new List<ImageRadioButtonsViewModel>();
        }

        /// <summary>
        /// شناسه ی کالای مزبور
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public int Id { get; set; }

        /// <summary>
        /// آدرس تصویر بندانگشتی پیش فرض
        /// </summary>
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ParvazPardaz.Resource.Validation.ValidationMessages))]
        public string DefaultThumbImage { get; set; }
        public int defaultImgId { get; set; }

        /// <summary>
        /// لیستی از آدرس تصاویر بندانگشتی کالا برای رادیو-باتن
        /// </summary>
        public List<ImageRadioButtonsViewModel> ThumbImageURLRadioButtons { get; set; }
        public List<int> ThumbImageURLRadioButtonIds { get; set; }
    }
}
