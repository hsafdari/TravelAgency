using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// جزییات برنامه سفر 
    /// </summary>
    public class TourProgramDetail : BaseEntity
    {

        #region Properties
        /// <summary>
        /// عنوان برنامه سفر
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// توضیح
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// مسیر عکسی از شهر
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

        #region Reference Navigation Properties
        public virtual TourProgram TourProgram { get; set; }
        public int TourProgramId { get; set; }

        public virtual Activity Activity { get; set; }
        public int ActivityId { get; set; }
        #endregion
    }
}
