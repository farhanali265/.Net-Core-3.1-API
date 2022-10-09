using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class ZelisErrorMap : IEntityTypeConfiguration<ZelisError> {
        public void Configure(EntityTypeBuilder<ZelisError> builder) {

            builder.ToTable("ZelisError", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder
                .Property(c => c.ID)
                .HasColumnName("ZelisErrorID")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.NpiNumbers).HasColumnName("NpiNumbers");

            builder.Property(c => c.Exception).HasColumnName("Exception");

            builder.Property(c => c.DateCreated).HasColumnName("DateCreated");

            // Relationships
        }
    }
}