using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class StateMap : IEntityTypeConfiguration<State> {
        public void Configure(EntityTypeBuilder<State> builder) {

            builder.ToTable("States", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("ID");
            builder.Property(c => c.Name).HasColumnName("State");
            builder.Property(c => c.Abbreviation).HasColumnName("Abbreviation");

            // Relationships
        }
    }
}