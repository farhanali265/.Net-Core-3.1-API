using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class ZelisNetworkMap : IEntityTypeConfiguration<ZelisNetwork> {
        public void Configure(EntityTypeBuilder<ZelisNetwork> builder) {

            builder.ToTable("ZelisNetwork", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("ZelisNetworkID");
            builder.Property(c => c.Description).HasColumnName("Description");

            // Relationships
        }
    }
}