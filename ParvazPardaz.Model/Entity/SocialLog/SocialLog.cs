using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.SocialLog
{
    public class SocialLog : BaseBigEntity
    {
        #region Properties
        public int TypeId { get; set; }
        public LinkType LinkType { get; set; }

        #endregion
    }
}
