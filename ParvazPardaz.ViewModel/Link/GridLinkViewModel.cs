using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridLinkViewModel : BigBaseViewModelOfEntity
    {
        public int typeId { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Target { get; set; }
        public string Rel { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
        public LinkType linkType { get; set; }
        public string CustomMetaTags { get; set; }
    }
}
