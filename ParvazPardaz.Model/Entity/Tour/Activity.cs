using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// فعالیت های تور 
    /// </summary>
    public class Activity : BaseEntity
    {
        #region Properties
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
        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<TourProgramActivity> TourProgramActivities { get; set; }
        public virtual ICollection<TourProgramDetail> TourProgramDetails { get; set; }
        #endregion
    }
}
