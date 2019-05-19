using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ParvazPardaz.ViewModel
{
   public class HotelGalleryViewModel:BaseViewModelId
    {
        public int HotelID { get; set; }
        public HttpPostedFileBase File { get; set; }
        public List<ParvazPardaz.Common.HtmlHelpers.Models.EditModeFileUpload> EditModeFileUploads{ get; set; }
    }
}
