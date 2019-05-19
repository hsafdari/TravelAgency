using System;
using System.Collections.Generic;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Hotel
{
    public class HotelBoard : BaseEntity
    {
        #region Properties
        public string Title { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        /// <summary>
        /// مسیر عکسی از کشور
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// پسوند عکس
        /// </summary>
        public string ImageExtension { get; set; }
        /// <summary>
        /// نام فایل عکس
        /// </summary>
        public string ImageFileName { get; set; }
        /// <summary>
        /// اندازه فایل
        /// </summary>
        public long ImageSize { get; set; }
        #endregion

        #region Collection navigation properties
        public virtual ICollection<HotelPackageHotel> HotelPackageHotels { get; set; }
        #endregion
    }
}
