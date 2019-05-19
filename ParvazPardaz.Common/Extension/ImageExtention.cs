using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Common.Extension
{
    public static class ImageExtention
    {
        public static string tourImage(this string imgurl)
        {
            var name = System.IO.Path.GetFileNameWithoutExtension(imgurl);
            var extention = System.IO.Path.GetExtension(imgurl);
            var url = Path.GetDirectoryName(imgurl).Replace("\\", "/") + "/" + name + "_Thumb" + extention;
            //var url = Path.GetFullPath(imgurl);
            var absolutePath = System.Web.HttpContext.Current.Server.MapPath("~/" + url);

            if (System.IO.File.Exists(absolutePath))
            {
                return url;
            }
            else
            {
                return imgurl;
            }

        }

        public static string GetThumbUrl(this string imgUrl)
        {
            if (imgUrl != null && imgUrl.Trim() != "")
            {
                var splittedUrl = imgUrl.Split('.');
                if (splittedUrl.Length > 0)
                    return splittedUrl[0] + "_thumb." + splittedUrl[1];
                else
                    return imgUrl;
            }
            else return imgUrl;
        }

        public static string GetThumbProductUrl(this string imgUrl)
        {
            if (imgUrl != null && imgUrl.Trim() != "")
            {
                var reultSplit = string.Empty;
                var splited = imgUrl.Split('/');
                if (splited.Length > 0)
                {
                    reultSplit = splited[0] + "/" + splited[1] + "/Thumbs/" + splited[2];
                }

                var splittedUrl = reultSplit.Split('.');
                if (splittedUrl.Length > 0)
                    return splittedUrl[0] + "_Thumb." + splittedUrl[1];
                else
                    return imgUrl;
            }
            else return imgUrl;
        }


        public static string GetTabMagUrl(this string imgUrl, string sizeString)
        {
            if (imgUrl != null && imgUrl.Trim() != "")
            {
                var splittedUrl = imgUrl.Split('.');
                if (splittedUrl.Length > 0)
                    return splittedUrl[0] + "-" + sizeString + "." + splittedUrl[1];
                else
                    //return imgUrl;
                    return "http://placehold.it/" + sizeString;
            }
            else
                //return imgUrl;
                return "http://placehold.it/" + sizeString;
        }

        public static string GetTabMagDefaultUrl(this string imgUrl, string sizeString)
        {
            if (imgUrl != null && imgUrl.Trim() != "")
            {
                return imgUrl;
            }
            else
            {
                return "http://placehold.it/" + sizeString;
            }

        }


        public static string GetNoImgIfNotExist(this string imgUrl, string imgSize)
        {
            if (imgUrl != null && imgUrl.Trim() != "")
            {
                if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(imgUrl.Trim())))
                {
                    return imgUrl.Trim();
                }
                else
                {
                    return "http://placehold.it/" + imgSize;
                }
            }
            else return imgUrl.Trim();
        }


    }

}
