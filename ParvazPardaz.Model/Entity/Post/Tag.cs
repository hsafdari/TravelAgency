using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityType = ParvazPardaz.Model.Entity.Hotel;
using EntityTypeMag = ParvazPardaz.Model.Entity.Magazine;

namespace ParvazPardaz.Model.Entity.Post
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }


        #region Collection Navigation Properties
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<EntityType.Hotel> Hotels { get; set; }
       
        #endregion
    }
}
