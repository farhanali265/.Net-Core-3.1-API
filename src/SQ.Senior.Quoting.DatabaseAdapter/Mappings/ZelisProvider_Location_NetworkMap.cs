using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class ZelisProvider_Location_NetworkMap : IEntityTypeConfiguration<ZelisProvider_Location_Network> {
        public void Configure(EntityTypeBuilder<ZelisProvider_Location_Network> builder) {

            builder.ToTable("ZelisProvider_Location_Network", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("ZelisProvider_Location_NetworkID");
            builder.Property(c => c.ZelisProvider_LocationID).HasColumnName("ZelisProvider_LocationID");
            builder.Property(c => c.ZelisNetworkID).HasColumnName("ZelisNetworkID");
            builder.Property(c => c.Flags).HasColumnName("Flags");
            builder.Property(c => c.TierLevel).HasColumnName("TierLevel");
            builder.Property(c => c.TierLabel).HasColumnName("TierLabel");
            builder.Property(c => c.Messages).HasColumnName("Messages");
            builder.Property(c => c.DateCreated).HasColumnName("DateCreated");

            // Relationships
        }
    }
}