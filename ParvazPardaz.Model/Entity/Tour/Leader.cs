using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParvazPardaz.Model.Entity.Tour
{
    /// <summary>
    /// افراد راهنمای سفر
    /// </summary>
    public class Leader : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// جنسیت
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// شماره تلفن
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// شماره موبایل 
        /// </summary>
        public string Mobile { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<TourProgram> TourPrograms { get; set; }
        #endregion
    }
}
