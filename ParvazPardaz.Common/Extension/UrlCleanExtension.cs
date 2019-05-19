using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Common.Extension
{
    public static class UrlCleanExtension
    {
        public static string UrlTourCanonical(string Url)
        {
            string _myUrl = Url.Replace("/tour/", "/");
            if (Url.Contains("?"))
            {
                int index= _myUrl.IndexOf("?");
                _myUrl = _myUrl.Remove(index);            
            }
            if (_myUrl.Substring(_myUrl.Length-1,1)=="/")
            {
                _myUrl = _myUrl.Remove(_myUrl.Length - 1, 1);
            }
            return _myUrl;
        }
    }
}
