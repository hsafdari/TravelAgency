using ParvazPardaz.Service.Contract.Core;
using System;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParvazPardaz.Service.DataAccessService.Core
{
    public class CacheService : ICacheService
    {
        public void HomeCacheFileSetCurrentTime()
        {
            //var currentTimeString = DateTime.Now.ToString();
            //var filePath = System.Web.HttpContext.Current.Server.MapPath("/App_Data/HomeDependentCachingFile.txt");
            //if (!System.IO.File.Exists(filePath))
            //{
            //    System.IO.File.Create(filePath);
            //}

            //System.IO.StreamWriter file = new System.IO.StreamWriter(filePath);
            //file.WriteLine(currentTimeString);
            //file.Close();
        }
    }
}
