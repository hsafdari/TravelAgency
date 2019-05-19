using ParvazPardaz.Model.Entity.Tour;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Tour
{
    public class CompanyTransferVehicleTypeConfig : EntityTypeConfiguration<CompanyTransferVehicleType>
    {
        public CompanyTransferVehicleTypeConfig()
        {
            ToTable("CompanyTransferVehicleTypes", "Tour");
            Property(x => x.ModelName).IsRequired().HasMaxLength(100);

            HasRequired(x => x.CompanyTransfer).WithMany(x => x.CompanyTransferVehicleTypes).HasForeignKey(x => x.CompanyTransferId).WillCascadeOnDelete(true);
            HasRequired(x => x.VehicleType).WithMany(x => x.CompanyTransferVehicleTypes).HasForeignKey(x => x.VehicleTypeId).WillCascadeOnDelete(true);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
