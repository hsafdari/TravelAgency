using System;
using System.Collections.Generic;
using ParvazPardaz.Model.Entity.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Rule
{
    public class TermsandCondition : BaseEntity
    {
        #region Properties
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public int Priority { get; set; }
        #endregion
    }
}
