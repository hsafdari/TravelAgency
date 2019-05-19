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
    
    public class FAQAnswer : BaseEntity
    {
        #region Properties
        /// <summary>
        /// جواب سوال
        /// </summary>
        public string Answer { get; set; }
        #endregion


        #region Reference Navigation Properties
        public virtual FAQQuestion FAQQuestion { get; set; }
        public int FAQQuestionId { get; set; }
        #endregion
    }
}
