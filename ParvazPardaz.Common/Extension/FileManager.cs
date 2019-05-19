using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace ParvazPardaz.Common.Extension
{
    public class FileManager
    {
        #region Ctor
        public FileManager()
        {
        }

        public FileManager(string FileName, byte[] Data)
            : this(FileName)
        {
            this.Data = Data;
        }

        public FileManager(string FileName)
        {
            this.Title = FileName;
            this.Name = Path.GetFileNameWithoutExtension(this.Title);
            this.Extension = Path.GetExtension(this.Title).Substring(1).ToLower();
        }

        public FileManager(string FileName, int Key)
            : this(FileName)
        {
            this.Key = Key;
        }

        public FileManager(string FileName, Stream input, int length)
        {
            this.Title = FileName;
            this.Name = Path.GetFileNameWithoutExtension(this.Title);
            this.Extension = Path.GetExtension(this.Title).Substring(1).ToLower();
            Data = new byte[length];
            input.Read(Data, 0, length);
        }
        #endregion

        #region Properties
        public int Key { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Data { get; set; }
        #endregion

        #region Methods
        public static string GetTempSectionPath(string Section)
        {
            var TempPath = HttpContext.Current.Server.MapPath("..\\"); ;
            var FullPath = Combine(TempPath, Section);
            Directory.CreateDirectory(FullPath);
            return FullPath;
        }

        public static string GetTempSectionFilePath(string Section, string FileName)
        {
            return Combine(GetTempSectionPath(Section), FileName);
        }

        public static string ToTempPath(string Section, string FileName, string Name)
        {
            var StorePath = GetTempSectionPath(Section);
            var FileManager = new FileManager(FileName);
            return Combine(StorePath, FileManager.ToFileName(Name));
        }

        public static string Combine(params string[] Paths)
        {
            return Path.Combine(Paths);
        }

        public string ToFileName(string name)
        {
            return String.Format("{0}.{1}", name, Extension);
        }

        static readonly string[] SizeSuffixesENG = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public static string SizeSuffix(Int64 value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixesENG[mag]);
        }
        #endregion

    }
}