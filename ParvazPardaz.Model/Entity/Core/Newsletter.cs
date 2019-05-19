using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParvazPardaz.Model.Entity.Core
{
    public class Newsletter : BaseEntity
    {
        #region Properties
        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// موبایل
        /// </summary>
        public string Mobile { get; set; }
        #endregion
    }
}
