using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using ParvazPardaz.Model.Entity.Tour;


namespace ParvazPardaz.Model.Configuration.Tour
{
    public class FAQQuestionConfig : EntityTypeConfiguration<FAQQuestion>
    {
        public FAQQuestionConfig()
        {
            ToTable("FAQQuestions", "Tour");
            Property(x => x.Title).IsRequired();

            HasRequired(x => x.Tour).WithMany(x => x.FAQQuestions).HasForeignKey(x => x.TourId).WillCascadeOnDelete(false);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
