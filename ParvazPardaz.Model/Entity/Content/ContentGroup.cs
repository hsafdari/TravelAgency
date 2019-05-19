using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Content
{
    public class ContentGroup : BaseEntity
    {
        #region Constructor
        public ContentGroup()
        {

        }
        #endregion

        #region Properties
        public string Title { get; set; }
        public string TitleEN { get; set; }
        #endregion

        #region Collection navigation properties
        public virtual ICollection<Content> Contents { get; set; }
        #endregion
    }
}
