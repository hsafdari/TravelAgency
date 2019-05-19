using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridCurrencyViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// عنوان واحد پولی
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }

        /// <summary>
        /// عنوان واحد پولی
        /// </summary>
        [Display(Name = "CurrenySign", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string CurrenySign { get; set; }

        /// <summary>
        /// قیمت بر اساس واحد ريال
        /// </summary>
        [Display(Name = "BaseRialPrice", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public decimal BaseRialPrice { get; set; }
    }
}
