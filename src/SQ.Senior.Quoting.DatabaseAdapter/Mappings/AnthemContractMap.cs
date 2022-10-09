using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class AnthemContractMap : IEntityTypeConfiguration<AnthemContract> {
        public void Configure(EntityTypeBuilder<AnthemContract> builder) {

            builder.ToTable("AnthemContracts", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("ID");
            builder.Property(c => c.Contract_ID).HasColumnName("Contract_ID");

            // Relationships
        }
    }
}