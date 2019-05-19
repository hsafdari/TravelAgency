using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Hotel;

namespace ParvazPardaz.ViewModel
{
   public class TourDetailHotelUIViewModel
    {
        /// <summary>
        /// نام هتل
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// آدرس هتل
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// تلفن هتل
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// آدرس وبسایت هتل
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }
        public IEnumerable<HotelFacility> HotelFacilities { get; set; }
        public string HotelGalleryURL { get; set; }
        public bool IsDelete { get; set; }
        public string City { get; set; }
    }
}
