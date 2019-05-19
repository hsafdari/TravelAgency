using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.ViewModel
{
    public class GridTourViewModel : BaseViewModelOfEntity
    {
        /// <summary>
        /// عنوان تور
        /// </summary>
        [Display(Name = "Title", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Title { get; set; }

        /// <summary>
        /// آدرس انگلیسی تور با الگوی زیر ، مورد استفاده در جدول لینک ها
        /// Country-City-TourTitle
        /// </summary>
        [Display(Name = "LinkTableTitle", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string LinkTableTitle { get; set; }

        /// <summary>
        /// نشان میدهد که تور از سمت آژانس توصیه میشود ؟
        /// </summary>
        [Display(Name = "Recomended", ResourceType = typeof(ParvazPardaz.Resource.Tour.Tours))]
        public string Recomended { get; set; }

        #region Reference property
        [Display(Name = "URL", ResourceType = typeof(ParvazPardaz.Resource.CMS.CMS))]
        public Nullable<int> LinkTableId { get; set; }
        #endregion
    }
}
