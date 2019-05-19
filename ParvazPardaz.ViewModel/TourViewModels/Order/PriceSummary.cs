using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class PriceSummary
    {
        //public string TourTitle { get; set; }
        //public string DepartureDateTime { get; set; }
        //public string ArrivalDateTime { get; set; }
        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// تعداد
        /// </summary>
        public string Quantity { get; set; }
        /// <summary>
        /// قیمت واحد
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// قیمت واحد در تعداد
        /// </summary>
        public decimal Total { get; set; }
        /// <summary>
        /// واحد پولی
        /// </summary>
        public string Currency { get; set; }
        //public decimal TotalDecimal { get; set; }
    }
}

