using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParvazPardaz.ViewModel
{
    public class ControlActionViewModel
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public  string Method { get; set; }
        public string Permission { get; set; }
        public string Title { get; set; }
        public bool Selected { get; set; }
    }
}