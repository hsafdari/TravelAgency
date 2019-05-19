using ParvazPardaz.Model.Entity.Comment;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Comment
{
    public class CommentReviewConfig : EntityTypeConfiguration<CommentReview>
    {
        public CommentReviewConfig()
        {
            ToTable("CommentReviews", "Comment");

            Property(x => x.Rate).IsRequired();

            //Reference navigation 
            HasRequired(x => x.Comment).WithMany(x => x.CommentReviews).HasForeignKey(x => x.CommentId).WillCascadeOnDelete(false);
            HasRequired(x => x.Review).WithMany(x => x.CommentReviews).HasForeignKey(x => x.ReviewId).WillCascadeOnDelete(false);
            
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
