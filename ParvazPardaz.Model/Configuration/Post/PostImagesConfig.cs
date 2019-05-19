using ParvazPardaz.Model.Entity.Post;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Post
{
    class TourSliderConfig : EntityTypeConfiguration<PostImage>
    {
        public TourSliderConfig()
        {
            ToTable("PostImage", "Post");
            Property(x => x.ImageTitle).IsOptional().HasMaxLength(300);
            Property(x => x.ImageDesc).IsOptional().HasMaxLength(500);
            Property(x => x.ImageExtension).IsOptional().HasMaxLength(5);
            Property(x => x.ImageFileName).IsOptional().HasMaxLength(250);
            Property(x => x.ImageUrl).IsOptional().HasMaxLength(300);
            Property(x => x.ImageSize).IsOptional();
            Property(x => x.Width).IsOptional();
            Property(x => x.Height).IsOptional();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
