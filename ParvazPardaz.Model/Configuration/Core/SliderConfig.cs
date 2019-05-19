using ParvazPardaz.Model.Entity.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration.Core
{
    public class SliderConfig : EntityTypeConfiguration<Slider>
    {
        public SliderConfig()
        {
            ToTable("Sliders", "Core");

            Property(x => x.ImageTitle).IsOptional();
            Property(x => x.ImageURL).HasMaxLength(250).IsRequired();
            Property(x => x.ImageDescription).HasMaxLength(200).IsOptional();
            Property(x => x.NavigationUrl).HasMaxLength(400).IsOptional();
            Property(x=>x.HeaderDays).HasMaxLength(20).IsOptional();
            Property(x=>x.NavDescription).HasMaxLength(200).IsOptional();
            Property(x=>x.footerLine1).HasMaxLength(50).IsOptional();
            Property(x=>x.footerLine2).HasMaxLength(50).IsOptional();
     
            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);

            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
