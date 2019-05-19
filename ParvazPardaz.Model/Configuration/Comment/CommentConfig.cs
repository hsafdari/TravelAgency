using EntityType = ParvazPardaz.Model.Entity.Comment;
//using ParvazPardaz.Model.Entity.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Comment
{
    public class CommentConfig : EntityTypeConfiguration<EntityType.Comment>
    {
        public CommentConfig()
        {
            ToTable("Comments", "Comment");

            Property(x => x.Email).IsRequired();
            Property(x => x.CommentText).IsRequired();
            Property(x => x.Like).IsOptional();
            Property(x => x.DisLike).IsOptional();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Reference navigation
            HasOptional(x => x.CommentParent).WithMany(x => x.CommentChildren).HasForeignKey(x => x.ParentId).WillCascadeOnDelete(false);

            //HasRequired(x => x.Product).WithMany(x => x.Comments).HasForeignKey(x => x.OwnId).WillCascadeOnDelete(false);
            //HasRequired(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.OwnId).WillCascadeOnDelete(false);
            //HasRequired(x => x.Hotel).WithMany(x => x.Comments).HasForeignKey(x => x.OwnId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
