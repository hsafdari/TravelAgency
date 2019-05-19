using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Users;
using ParvazPardaz.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityComment = ParvazPardaz.Model.Entity.Comment.Comment;

namespace ParvazPardaz.Model.Entity.Post
{
    public class Post : BaseEntity
    {
        public string Name { get; set; }
        public string LinkTableTitle { get; set; }
        //public string Thumbnail { get; set; }
        public string PostSummery  { get; set; }
        public string PostContent { get; set; }
        public string MetaKeywords  { get; set; }
        public string MetaDescription  { get; set; }
        public int VisitCount { get; set; }
        public decimal PostRateAvg  { get; set; }
        public int PostRateCount  { get; set; }
        public DateTime PublishDatetime { get; set; }
        public Nullable<DateTime> ExpireDatetime  { get; set; }
        public int PostSort { get; set; }
        public ParvazPardaz.Model.Enum.AccessLevel AccessLevel { get; set; }
        public bool IsActiveComments { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> LikeCount { get; set; }

        /// <summary>
        /// مسیر عکس 
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// پسوند عکس
        /// </summary>
        public string ImageExtension { get; set; }

        /// <summary>
        /// نام فایل عکس
        /// </summary>
        public string ImageFileName { get; set; }

        /// <summary>
        /// اندازه فایل
        /// </summary>
        public long ImageSize { get; set; }

        #region Reference navigation properties
        /// <summary>
        /// نویسنده ای که قراره نشون داده بشه
        /// </summary>
        public Nullable<int> WriterId { get; set; }
        public virtual User Writer { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<PostGroup> PostGroups { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<PostImage> PostImages { get; set; }
        /// <summary>
        /// مجموعه دیدگاه های پست
        /// </summary>
        //public virtual ICollection<EntityComment> Comments { get; set; }
        #endregion
    }
}
