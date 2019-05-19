using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Helpers;

namespace ParvazPardaz.Common.Extension
{
    public static class ImageManage
    {
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0);
        }
        /// <summary>
        /// تغییر اندازه تصویر
        /// </summary>
        /// <returns></returns>
        public static byte[] ResizeImageFile(this byte[] imageFile, int width, int height)
        {
            return new WebImage(imageFile).Resize(width + 1, height + 1).Crop(1, 1).GetBytes();
        }

        public static byte[] ResizeImageFile(string path, int width, int height)
        {
            return new WebImage(path).Resize(width + 1, height + 1).Crop(1, 1).GetBytes();
        }

        public static byte[] ResizeImageFile(this  Stream stream, int width, int height)
        {
            return new WebImage(stream).Resize(width + 1, height + 1).Crop(1, 1).GetBytes();
        }
        public static byte[] ResizeImageFileExact(this  Stream stream, int width, int height)
        {
            return new WebImage(stream).Resize(width + 1, height + 1, false).Crop(1, 1).GetBytes();
        }

        public static byte[] AddWaterMark(string filePath, string text)
        {
            using (var img = System.Drawing.Image.FromFile(filePath))
            {
                using (var memStream = new MemoryStream())
                {
                    using (var bitmap = new Bitmap(img)) // to avoid GDI+ errors
                    {
                        bitmap.Save(memStream, ImageFormat.Png);
                        var content = memStream.ToArray();
                        var webImage = new WebImage(memStream);
                        webImage.AddTextWatermark(text, verticalAlign: "Top", horizontalAlign: "Left", fontColor: "Brown");
                        return webImage.GetBytes();
                    }
                }
            }
        }

        public static string ThumbImage(this string imgurl)
        {
            var name = System.IO.Path.GetFileNameWithoutExtension(imgurl);
            var extention = System.IO.Path.GetExtension(imgurl);
            var url = Path.GetDirectoryName(imgurl).Replace("\\", "/") + "/" + name + "_Thumb" + extention;
            //var url = Path.GetFullPath(imgurl);
            //var absolutePath = System.Web.HttpContext.Current.Server.MapPath("~/" + url);

            return url;
        }
    }
}