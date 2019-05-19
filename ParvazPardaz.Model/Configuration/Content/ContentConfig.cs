using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityNS = ParvazPardaz.Model.Entity.Content;

namespace ParvazPardaz.Model.Configuration.Content
{
    public class ContentConfig : EntityTypeConfiguration<EntityNS.Content>
    {
        public ContentConfig()
        {
            ToTable("Contents", "Content");

            Property(x => x.Title).IsRequired();
            Property(x => x.NavigationUrl).IsOptional();
            Property(x => x.Description).IsRequired();
            Property(x => x.Context).IsRequired();
            Property(x => x.CreatorDateTime).IsOptional();
            Property(x => x.IsActive).IsRequired();
            Property(x => x.CommentIsActive).IsRequired();
            Property(x => x.ImageExtension).IsOptional().HasMaxLength(5);
            Property(x => x.ImageFileName).IsOptional().HasMaxLength(250);
            Property(x => x.ImageUrl).IsOptional().HasMaxLength(300);
            Property(x => x.ImageSize).IsOptional();
            Property(x => x.TourLandingPageUrlId).IsOptional();

            HasRequired(x => x.ContentGroup).WithMany(x => x.Contents).HasForeignKey(x => x.ContentGroupId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
