using ParvazPardaz.Model.Entity.Common;
using ParvazPardaz.Model.Entity.Magazine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityType = ParvazPardaz.Model.Entity;

namespace ParvazPardaz.Model.Entity.Post
{
    public class PostGroup : BaseEntity
    {
        #region Properties
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string Title { get; set; }
        #endregion

        #region Collection Navigation Properties
        public Nullable<int> ParentId { get; set; }
        public virtual PostGroup PostGroupParent { get; set; }
        #endregion

        #region Collection Navigation Properties
        public virtual ICollection<PostGroup> PostGroupChildren { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<EntityType.Hotel.Hotel> Hotels { get; set; }
        public virtual ICollection<EntityType.Tour.Tour> Tours { get; set; }
        public virtual ICollection<TabMagazine> TabMagazines { get; set; }

        #endregion
    }
}
