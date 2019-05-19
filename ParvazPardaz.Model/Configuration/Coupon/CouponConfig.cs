using EntityType = ParvazPardaz.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Coupon
{
    public class CouponConfig : EntityTypeConfiguration<EntityType.Coupon>
    {
        public CouponConfig()
        {
            //فقط به خاطر آقای علیزاده در بوک گذاشته شد
            ToTable("Coupons", "Book");

            Property(x => x.Title).HasMaxLength(10).IsOptional();
            Property(x => x.Code).HasMaxLength(10).IsRequired();
            Property(x => x.DiscountPercent).IsRequired();
            Property(x => x.ExpireDate).IsRequired();
            HasOptional(x => x.ValidatedUser).WithMany().HasForeignKey(x => x.ValidatedUserId).WillCascadeOnDelete(false);

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            //Concurrency
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
