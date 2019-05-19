using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParvazPardaz.Model.Entity.Rule;

namespace ParvazPardaz.Model.Configuration.Rule
{
    class TermsandConditionConfig : EntityTypeConfiguration<TermsandCondition>
    {
        public TermsandConditionConfig()
        {
            ToTable("TermsandConditions", "Rule");
            Property(x => x.Title).HasMaxLength(200).IsRequired();
            Property(x => x.Content).IsRequired();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
