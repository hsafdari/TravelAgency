using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;

namespace ParvazPardaz.Common.Utility
{
    public class MakeThumbnail
    {
        private string imageUploadPath;
        public MakeThumbnail(string imagepath)
        {
            ImageUploadPath = imagepath;
        }
        public string ImageUploadPath
        {
            set { imageUploadPath = value; }
            get { return imageUploadPath; }
        }
        public string CreateThumbnail(string OriginalFileFullPath)
        {
            string filename = string.Empty;

            if (File.Exists(OriginalFileFullPath))
            {
                System.Drawing.Image img;

                using (img = Bitmap.FromFile(OriginalFileFullPath))
                {
                    Bitmap bmp = new Bitmap(img);
                    bmp = BitmapManipulator.ThumbnailBitmap(bmp, 50, 50);

                    string thumbfilename = Path.GetFileNameWithoutExtension(OriginalFileFullPath) + "_Thumb" + Path.GetExtension(OriginalFileFullPath);

                    string thumb_file_relative_path = ImageUploadPath + thumbfilename;

                    bmp.Save(HttpContext.Current.Server.MapPath(thumb_file_relative_path), System.Drawing.Imaging.ImageFormat.Jpeg);

                    filename = thumb_file_relative_path;
                }
                //bmp.Dispose();

            }
            return filename;
        }
        public string CreateThumbnail(string FileName, string Extension, Stream strObj)
        {
            string filename = string.Empty;
            System.Drawing.Image img;
            using (img = Bitmap.FromStream(strObj))
            {
                Bitmap bmp = new Bitmap(img);
                bmp = BitmapManipulator.ThumbnailBitmap(bmp, 50, 50);

                string thumbfilename = FileName + Extension;

                string thumb_file_relative_path = ImageUploadPath + thumbfilename;

                bmp.Save(HttpContext.Current.Server.MapPath(thumb_file_relative_path), System.Drawing.Imaging.ImageFormat.Jpeg);
                filename = thumb_file_relative_path;
            }
            return filename;
        }
        public string CreateThumbnail(string FileName, string Extension, Stream strObj, int MaxWidth, int MaxHeight, string postfix)
        {
            string filename = string.Empty;
            System.Drawing.Image img;
            using (img = Bitmap.FromStream(strObj))
            {
                Bitmap bmp = new Bitmap(img);
                bmp = BitmapManipulator.ThumbnailBitmap(bmp, MaxWidth, MaxHeight);
                string thumbfilename = FileName + postfix + Extension;

                string thumb_file_relative_path = ImageUploadPath + thumbfilename;

                bmp.Save(HttpContext.Current.Server.MapPath(thumb_file_relative_path), System.Drawing.Imaging.ImageFormat.Jpeg);
                filename = thumb_file_relative_path;
            }
            return filename;
        }
        public string CreateThumbnail(string FileName, string Extension, Stream strObj, int MaxWidth, int MaxHeight)
        {
            string filename = string.Empty;
            System.Drawing.Image img;
            using (img = Bitmap.FromStream(strObj))
            {
                Bitmap bmp = new Bitmap(img);
                bmp = BitmapManipulator.ThumbnailBitmap(bmp, MaxWidth, MaxHeight);
                string thumbfilename = FileName + Extension;

                string thumb_file_relative_path = ImageUploadPath + thumbfilename;

                bmp.Save(HttpContext.Current.Server.MapPath(thumb_file_relative_path), System.Drawing.Imaging.ImageFormat.Jpeg);
                filename = thumb_file_relative_path;
            }
            return filename;
        }
        public string GetThumbFilename(string OriginalFilename, string Extension)
        {
            string thumbfilename =
                    Path.GetFileNameWithoutExtension(OriginalFilename)
                    + Extension
                    + Path.GetExtension(OriginalFilename);

            return ImageUploadPath + thumbfilename;
        }
    }
}