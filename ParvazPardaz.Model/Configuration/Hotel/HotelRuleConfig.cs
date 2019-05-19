using ParvazPardaz.Model.Entity.Hotel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Hotel
{
    public class HotelRuleConfig : EntityTypeConfiguration<HotelRule>
    {
        public HotelRuleConfig()
        {
            ToTable("HotelRules", "Hotel");

            Property(x => x.Title).IsRequired();
            Property(x => x.Rule).IsRequired();

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
