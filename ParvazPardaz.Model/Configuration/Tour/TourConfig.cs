using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Model.Configuration
{
    public class TourConfig : EntityTypeConfiguration<Entity.Tour.Tour>
    {
        public TourConfig()
        {
            ToTable("Tours", "Tour");

            Property(x => x.Title).HasMaxLength(100).IsRequired();
            Property(x => x.LinkTableTitle).IsOptional().HasMaxLength(1000);//.IsRequired();
            Property(x => x.Description).IsMaxLength().IsOptional();
            Property(x => x.ShortDescription).IsRequired();
            Property(x => x.TourLandingPageUrlId).IsOptional();
            Property(x => x.Priority).IsOptional();

            HasMany(x => x.TourTypes).WithMany(x => x.Tours).Map(x =>
            {
                x.MapLeftKey("TourId");
                x.MapRightKey("TourTypeId");
                x.ToTable("TourTourTypes", "Tour");
            });

            HasMany(x => x.TourLevels).WithMany(x => x.Tours).Map(x =>
            {
                x.MapLeftKey("TourId");
                x.MapRightKey("TourLevelId");
                x.ToTable("TourTourLevels", "Tour");
            });

            HasMany(x => x.TourCategories).WithMany(x => x.Tours).Map(x =>
            {
                x.MapLeftKey("TourId");
                x.MapRightKey("TourCategoryId");
                x.ToTable("TourTourCategories", "Tour");
            });

            HasMany(x => x.TourSliders).WithMany(x => x.Tours).Map(x =>
            {
                x.MapLeftKey("TourId");
                x.MapRightKey("TourSliderId");
                x.ToTable("TourTourSliders", "Tour");
            });

            HasMany(x => x.PostGroups).WithMany(x => x.Tours).Map(z =>
            {
                z.MapLeftKey("TourId");
                z.MapRightKey("TourGroupId");
                z.ToTable("TourGroupTours", "Tour");
            });

            HasOptional(x => x.CreatorUser).WithMany().HasForeignKey(x => x.CreatorUserId).WillCascadeOnDelete(false);
            HasOptional(x => x.ModifierUser).WithMany().HasForeignKey(x => x.ModifierUserId).WillCascadeOnDelete(false);
            Property(x => x.RowVersion).IsRowVersion();
        }
    }
}
