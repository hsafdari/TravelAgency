using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class SelelectedRoomInPackageViewModel:BaseViewModelId
    {
         #region Constructor
        public SelelectedRoomInPackageViewModel()
        {
            StockCount = 0;
            Price = 0;
        }
        #endregion

        public string Title { get; set; }
        public int HotelRoomId { get; set; }
        public int SelectedHotelPackageId { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        /// <summary>
        /// موجودی 
        /// </summary>
        public int StockCount { get; set; }

        /// <summary>
        /// علامت واحد پولی
        /// </summary>
        public string CurrenySign { get; set; }
        /// <summary>
        /// مبلغ واحد ارزی در روز خرید
        /// </summary>
        public decimal? BaseCurrencyPrice { get; set; }
        /// <summary>
        /// مبلغ پایه محصول به واحد ارزی
        /// </summary>
        public decimal? TotalCurrencyPrice { get; set; }              
    }
}
