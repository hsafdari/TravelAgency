using ParvazPardaz.Model.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityType = ParvazPardaz.Model.Entity;

namespace ParvazPardaz.Model.Entity.Magazine
{
    public class TabMagazine : BaseEntity
    {
        public TabMagazine()
        {
            Groups = new HashSet<EntityType.Post.PostGroup>();
        }

        #region Properties
        public string Title { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int Priority { get; set; }
        #endregion

        #region Collection properties
        public virtual ICollection<EntityType.Post.PostGroup> Groups { get; set; }
        #endregion

        #region Reference properties
        public int CountryId { get; set; }
        public virtual Location Location { get; set; } 
        #endregion
    }
}
