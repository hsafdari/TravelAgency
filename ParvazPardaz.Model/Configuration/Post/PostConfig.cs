using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Post
{
    public class PostConfig : EntityTypeConfiguration<ParvazPardaz.Model.Entity.Post.Post>
    {
        public PostConfig()
        {
            ToTable("Posts", "Post");
            Property(x => x.Name).IsRequired();
            Property(x => x.LinkTableTitle).IsRequired();
            //Property(x => x.Thumbnail).IsRequired();
            Property(x => x.PostSummery).IsOptional().HasMaxLength(500);
            Property(x => x.PostContent).IsRequired();
            Property(x => x.MetaKeywords).IsOptional().HasMaxLength(500);
            Property(x => x.MetaDescription).IsOptional().HasMaxLength(500);
            Property(x => x.VisitCount).IsRequired();
            Property(x => x.PostRateAvg).IsOptional().HasPrecision(18, 2);
            Property(x => x.PostRateCount).IsOptional();
            Property(x => x.PublishDatetime).IsRequired();
            Property(x => x.ExpireDatetime).IsOptional();
            Property(x => x.PostSort).IsRequired();
            Property(x => x.AccessLevel).IsRequired();
            Property(x => x.IsActiveComments).IsRequired();
            Property(x => x.IsActive).IsRequired();
            Property(x => x.LikeCount).IsOptional();

            HasMany(x => x.PostGroups).WithMany(x => x.Posts).Map(z =>
            {
                z.MapLeftKey("PostId");
                z.MapRightKey("PostGroupId");
                z.ToTable("PostsGroups", "Post");
            });

            HasMany(x => x.Tags).WithMany(x => x.Posts).Map(z =>
            {
                z.MapLeftKey("PostId");
                z.MapRightKey("TagId");
                z.ToTable("PostsTags", "Post");
            });

            HasOptional(x => x.Writer).WithMany().HasForeignKey(x => x.WriterId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
