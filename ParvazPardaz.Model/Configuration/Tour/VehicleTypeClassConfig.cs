using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class VehicleTypeClassConfig : EntityTypeConfiguration<VehicleTypeClass>
    {
        public VehicleTypeClassConfig()
        {
            ToTable("VehicleTypeClasses", "Tour");
            Property(x => x.Title).IsRequired();
            Property(x => x.TitleEn).IsRequired();
            Property(x => x.Code).IsRequired().HasMaxLength(3);

            HasRequired(x => x.VehicleType).WithMany(x => x.VehicleTypeClasses).HasForeignKey(x => x.VehicleTypeId).WillCascadeOnDelete(false);
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
