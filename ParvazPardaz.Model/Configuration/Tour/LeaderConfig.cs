using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class LeaderConfig : EntityTypeConfiguration<Leader>
    {
        public LeaderConfig()
        {
            ToTable("Leaders", "Tour");
            Property(x => x.Gender).IsRequired();
            Property(x => x.FirstName).IsRequired().HasMaxLength(25);
            Property(x => x.LastName).IsRequired().HasMaxLength(25);
            Property(x => x.FullName).IsOptional().HasMaxLength(50);
            Property(x => x.Mobile).IsRequired().HasMaxLength(15);
            Property(x => x.Tel).IsOptional().HasMaxLength(15);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
