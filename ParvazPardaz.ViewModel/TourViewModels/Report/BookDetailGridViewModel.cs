using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class BookDetailGridViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// توضیح که شامل نام خدمات اضافه و هتل و اتاق های هتل و پروازها و غیره میباشد
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// قیمت
        /// </summary>
        public decimal? Price { get; set; }
        public decimal? TotalPrice { get; set; }
        public int Quantity { get; set; }
    }
}
