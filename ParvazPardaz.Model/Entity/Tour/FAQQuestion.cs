using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Tour
{

    public class FAQQuestion : BaseEntity
    {
        #region Properties
        /// <summary>
        /// عنوان سوال
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// توضیحات سوال
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// فعال یا غیر فعال بودن سوال
        /// </summary>
        public bool IsActive { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<FAQAnswer> FAQAnswers { get; set; }
        #endregion
        #region Reference Navigation Properties
        public virtual Tour Tour { get; set; }
        public int TourId { get; set; }
        #endregion
    }
}
