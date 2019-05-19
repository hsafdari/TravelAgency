using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridOrderTypeViewModel : BaseViewModelOfEntity
    {
        #region Properties
        /// <summary>
        /// عنوان نوع سفارش
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.EShop.EShopResource))]
        public string Title { get; set; }
        #endregion
    }
}
