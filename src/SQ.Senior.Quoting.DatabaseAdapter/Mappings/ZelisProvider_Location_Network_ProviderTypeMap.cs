using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class ZelisProvider_Location_Network_ProviderTypeMap : IEntityTypeConfiguration<ZelisProvider_Location_Network_ProviderType> {
        public void Configure(EntityTypeBuilder<ZelisProvider_Location_Network_ProviderType> builder) {

            builder.ToTable("ZelisProvider_Location_Network_ProviderType", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("ZelisProvider_Location_Network_ProviderTypeID");
            builder.Property(c => c.ZelisProvider_Location_NetworkID).HasColumnName("ZelisProvider_Location_NetworkID");
            builder.Property(c => c.ZelisProviderTypeID).HasColumnName("ZelisProviderTypeID");
            builder.Property(c => c.DateCreated).HasColumnName("DateCreated");

            // Relationships
        }
    }
}