using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class MenuTreeViewModel
    {
        public int Id { get; set; }
        public bool Haschildren { get; set; }
        public string NodeText { get; set; }
        public bool Checked { get; set; }
        public string Title { get; set; }
    }
}
