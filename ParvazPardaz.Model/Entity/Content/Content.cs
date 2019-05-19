using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Entity.Content
{
    public class Content : BaseEntity
    {
        #region Constructor
        public Content()
        {

        }
        #endregion

        #region properties
        public string Title { get; set; }
        public string NavigationUrl { get; set; }
        public string Description { get; set; }
        public string Context { get; set; }
        public Nullable<DateTime> ContentDateTime { get; set; }
        public bool IsActive { get; set; }
        public bool CommentIsActive { get; set; }
        public string ImageUrl { get; set; }
        public string ImageExtension { get; set; }
        public string ImageFileName { get; set; }
        public long ImageSize { get; set; }
        #endregion

        #region Reference navigation properties
        public int ContentGroupId { get; set; }
        public virtual ContentGroup ContentGroup { get; set; }

        /// <summary>
        /// شناسه آدرس لندینگ پیج تور
        /// </summary>
        public Nullable<int> TourLandingPageUrlId { get; set; }
        #endregion
    }
}
