using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Book
{
    public class OrderTask : BaseEntity
    {
        #region Properties
        public string TaskValue { get; set; }        
        #endregion

        #region Reference navigation properties
        public long OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
        #endregion
    }
}
