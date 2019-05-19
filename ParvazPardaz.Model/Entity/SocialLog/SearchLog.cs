using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.SocialLog
{
    public class SearchLog : BaseBigEntity
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public string Browser { get; set; }
    }
}
