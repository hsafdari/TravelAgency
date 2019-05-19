
using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParvazPardaz.Model.Entity.Hotel
{
    /// <summary>
    /// رتبه هتل
    /// </summary>
    public class HotelRank : BaseEntity
    {
        #region Properties
        /// <summary>
        /// عنوان رتبه هتل
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// ایکون رتبه هتل
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// پسوند عکس لوگو
        /// </summary>
        public string ImageExtension { get; set; }
        /// <summary>
        /// اندازه فایل
        /// </summary>
        public long ImageSize { get; set; }
        /// <summary>
        /// نام فایل عکس لوگو
        /// </summary>
        public string ImageFileName { get; set; }
        /// <summary>
        /// ترتیب رتبه هتل
        /// </summary>
        public int OrderId { get; set; }
        
        #endregion


        #region Collection Navigation Properties
        public virtual ICollection<Hotel> Hotels { get; set; }
        #endregion
    }
}
