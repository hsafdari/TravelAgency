using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Link
{
    public class LinkRedirection : BaseEntity
    {
        public string OldLink { get; set; }
        public string NewLink { get; set; }
    }
}
