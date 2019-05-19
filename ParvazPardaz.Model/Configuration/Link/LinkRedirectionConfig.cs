using ParvazPardaz.Model.Entity.Link;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Link
{
    public class LinkRedirectionConfig : EntityTypeConfiguration<LinkRedirection>
    {
        public LinkRedirectionConfig()
        {
            ToTable("LinkRedirections", "Link");

            Property(x => x.OldLink).IsRequired();
            Property(x => x.NewLink).IsRequired();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
