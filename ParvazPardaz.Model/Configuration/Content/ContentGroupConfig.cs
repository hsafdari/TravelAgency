using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Content;

namespace ParvazPardaz.Model.Configuration.Content
{
    public class ContentGroupConfig : EntityTypeConfiguration<EntityNS.ContentGroup>
    {
        public ContentGroupConfig()
        {
            ToTable("ContentGroups", "Content");

            Property(x => x.Title).IsRequired();
            Property(x => x.TitleEN).IsRequired();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
