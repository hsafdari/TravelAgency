using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ParvazPardaz.ViewModel
{
    public class ListViewTourProgramDetailViewModel : BaseViewModelId
    {
        /// <summary>
        /// عنوان برنامه سفر
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// آیدی برنامه سفر
        /// </summary>
        public int TourProgramId { get; set; }
        /// <summary>
        /// نام شهر
        /// </summary>
        public string CityTitle { get; set; }
        /// <summary>
        /// آیدی تگ دیو هر بخش اضافه شده 
        /// </summary>
        public string SectionId { get; set; }
     
    }
}
