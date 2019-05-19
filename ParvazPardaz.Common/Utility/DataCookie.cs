using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Common.Utility
{
    public class DataCookie
    {
        /// <summary>
        /// شناسه ی آنچه به آن امتیاز داده می شود
        /// </summary>
        //public int OwnId { get; set; }
        public long OwnId { get; set; }

        /// <summary>
        /// تاریخ امتیازدهی
        /// </summary>
        public DateTime RateDate { get; set; }

        /// <summary>
        /// امتیازی که کاربر داده
        /// </summary>
        public decimal NewestRate { get; set; }
    }
}
