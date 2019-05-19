using ParvazPardaz.Model.Entity.Post;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Post
{
    public class PostGroupConfig : EntityTypeConfiguration<PostGroup>
    {
        public PostGroupConfig()
        {
            ToTable("PostGroups", "Post");
            Property(x => x.Name).IsRequired();
            Property(x => x.IsActive).IsRequired();
            Property(x => x.Title).IsOptional();

            HasOptional(x => x.PostGroupParent).WithMany(x => x.PostGroupChildren).HasForeignKey(x => x.ParentId).WillCascadeOnDelete(false);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
