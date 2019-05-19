using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class ActivitiesUIViewModel
    {
        /// <summary>
        /// عنوان فعالیت
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// نام مکان بازدید یا فعالیت
        /// </summary>
        public string Place { get; set; }
        /// <summary>
        /// عکسی از مکان فعالیت
        /// </summary>
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }
    }
}
