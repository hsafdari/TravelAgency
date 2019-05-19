using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class FAQConfig : EntityTypeConfiguration<FAQ>
    {
        public FAQConfig()
        {
            ToTable("FAQs", "Tour");
            Property(x => x.Question).IsRequired();
            Property(x => x.Answer).IsRequired();

            HasRequired(x => x.Tour).WithMany(x => x.FAQs).HasForeignKey(x => x.TourId).WillCascadeOnDelete(false);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
