﻿using ParvazPardaz.Model.Entity.Comment;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Comment
{
    public class ReviewConfig : EntityTypeConfiguration<Review>
    {
        public ReviewConfig()
        {
            ToTable("Reviews", "Comment");

            Property(x => x.Title).IsRequired();
            Property(x => x.ReviewType).IsRequired();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
