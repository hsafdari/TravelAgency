using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Common.Extension;

namespace ParvazPardaz.Common.HtmlHelpers.Models
{
    public class EditModeFileUpload
    {
        public int Id { get; set; }
        private string type { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string Type { get { return type; } set { type = value.SetFileType(); } }
        public string Url { get; set; }


    }
}
