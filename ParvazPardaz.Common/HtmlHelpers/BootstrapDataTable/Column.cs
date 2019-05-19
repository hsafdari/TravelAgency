using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Common.HtmlHelpers
{
    public class Column
    {
        public enum AlignType
        {
            Center,
            Right,
            Left
        }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Display { get; set; }
        public bool Visible { get; set; }
        public string Format { get; set; }
        public Type Type { get; set; }
        public bool Sortable { get; set; }
        public string OnColumnStyle { get; set; }
        public AlignType? Align { get; set; }
        public AlignType? HAlign { get; set; }
        public string OnFormatter { get; set; }
        public bool ShowHeader { get; set; }
        public bool NotSwitchable { get; set; }
        public bool SingleSelect { get; set; }
        public bool MultiSelect { get; set; }
        public string ActionFormater { get; set; }
        public string ActionEvents { get; set; }
    }
}
