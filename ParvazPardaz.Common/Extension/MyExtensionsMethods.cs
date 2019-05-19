using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;
using System.IO;


namespace ParvazPardaz.Common.Extension
{
    public static class MyExtensionsMethods
    {
        public static string ToUrlString(this string url)
        {

            return url.Replace(" ", "-").Replace(".", "").Replace("/", "-").Replace("?", "-").Replace("*", "-").Replace("+", "-").Replace("\\", "-");
        }
        //public static string ThumbImageTag(this string imgurl)
        //{
        //    var name = System.IO.Path.GetFileNameWithoutExtension(imgurl);
        //    var extention = System.IO.Path.GetExtension(imgurl);
        //    var url = Path.GetDirectoryName(imgurl).Replace("\\", "/") + "/" + name + "_Thumb" + extention;
        //    //var url = Path.GetFullPath(imgurl);
        //    var absolutePath = System.Web.HttpContext.Current.Server.MapPath("~/" + url);

        //    if (System.IO.File.Exists(absolutePath))
        //    {
        //        return "<img src=" + '"' + @url + '"' + "/>";
        //    }
        //    else
        //    {
        //        return "<img src=" + '"' + imgurl + '"' + " width='50' height='50'/>";
        //    }

        //}
        //public static string ThumbImage(this string imgurl)
        //{
        //    var name = System.IO.Path.GetFileNameWithoutExtension(imgurl);
        //    var extention = System.IO.Path.GetExtension(imgurl);
        //    var url = Path.GetDirectoryName(imgurl).Replace("\\", "/") + "/" + name + "_Thumb" + extention;
        //    //var url = Path.GetFullPath(imgurl);
        //    //var absolutePath = System.Web.HttpContext.Current.Server.MapPath("~/" + url);

        //    return url;
        //}
        //public static string VisaImage(this string imgurl)
        //{
        //    var name = System.IO.Path.GetFileNameWithoutExtension(imgurl);
        //    var extention = System.IO.Path.GetExtension(imgurl);
        //    var url = Path.GetDirectoryName(imgurl).Replace("\\", "/") + "/" + name + "_visa" + extention;
        //    //var url = Path.GetFullPath(imgurl);
        //    var absolutePath = System.Web.HttpContext.Current.Server.MapPath("~/" + url);

        //    if (System.IO.File.Exists(absolutePath))
        //    {
        //        return url;
        //    }
        //    else
        //    {
        //        return imgurl;
        //    }

        //}
        //public static string PickUpImage(this string imgurl)
        //{
        //    var name = System.IO.Path.GetFileNameWithoutExtension(imgurl);
        //    var extention = System.IO.Path.GetExtension(imgurl);
        //    var url = Path.GetDirectoryName(imgurl).Replace("\\", "/") + "/" + name + "_pickUp" + extention;
        //    //var url = Path.GetFullPath(imgurl);
        //    var absolutePath = System.Web.HttpContext.Current.Server.MapPath("~/" + url);

        //    if (System.IO.File.Exists(absolutePath))
        //    {
        //        return url;
        //    }
        //    else
        //    {
        //        return imgurl;
        //    }

        //}
        //public static string SefaratImage(this string imgurl)
        //{
        //    var name = System.IO.Path.GetFileNameWithoutExtension(imgurl);
        //    var extention = System.IO.Path.GetExtension(imgurl);
        //    var url = Path.GetDirectoryName(imgurl).Replace("\\", "/") + "/" + name + "_sefarat" + extention;
        //    //var url = Path.GetFullPath(imgurl);
        //    var absolutePath = System.Web.HttpContext.Current.Server.MapPath("~/" + url);

        //    if (System.IO.File.Exists(absolutePath))
        //    {
        //        return url;
        //    }
        //    else
        //    {
        //        return imgurl;
        //    }

        //}

        //public static string VacationImage(this string imgurl)
        //{
        //    var name = System.IO.Path.GetFileNameWithoutExtension(imgurl);
        //    var extention = System.IO.Path.GetExtension(imgurl);
        //    var url = Path.GetDirectoryName(imgurl).Replace("\\", "/") + "/" + name + "_Vac" + extention;
        //    //var url = Path.GetFullPath(imgurl);
        //    var absolutePath = System.Web.HttpContext.Current.Server.MapPath("~/" + url);

        //    if (System.IO.File.Exists(absolutePath))
        //    {
        //        return url;
        //    }
        //    else
        //    {
        //        return imgurl;
        //    }

        //}

        //public static string EmbassyImage(this string imgurl)
        //{
        //    var name = System.IO.Path.GetFileNameWithoutExtension(imgurl);
        //    var extention = System.IO.Path.GetExtension(imgurl);
        //    var url = Path.GetDirectoryName(imgurl).Replace("\\", "/") + "/" + name + "_embFlag" + extention;
        //    //var url = Path.GetFullPath(imgurl);
        //    var absolutePath = System.Web.HttpContext.Current.Server.MapPath("~/" + url);

        //    if (System.IO.File.Exists(absolutePath))
        //    {
        //        return url;
        //    }
        //    else
        //    {
        //        return imgurl;
        //    }

        //}

    }
}