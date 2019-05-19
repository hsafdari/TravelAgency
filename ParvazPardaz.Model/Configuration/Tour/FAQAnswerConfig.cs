using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using ParvazPardaz.Model.Entity.Tour;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class FAQAnswerConfig : EntityTypeConfiguration<FAQAnswer>
    {
        public FAQAnswerConfig()
        {
            ToTable("FAQAnswers", "Tour");
            Property(x=>x.Answer).IsRequired();

            HasRequired(x => x.FAQQuestion).WithMany(x => x.FAQAnswers).HasForeignKey(x => x.FAQQuestionId).WillCascadeOnDelete(false);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
