using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridTaskViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// عنوان اقدامات سفارش
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Title { get; set; }

        /// <summary>
        /// از نوع تاریخ است؟
        /// </summary>
        [Display(Name = "IsDate", ResourceType = typeof(ParvazPardaz.Resource.Book.Book))]
        public bool IsDate { get; set; }
    }
}
