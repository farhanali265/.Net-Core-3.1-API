using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class ZipFipsMappingMap : IEntityTypeConfiguration<ZipFipsMapping> {
        public void Configure(EntityTypeBuilder<ZipFipsMapping> builder) {

            builder.ToTable("ZipFipsMapping", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("MapId");

            builder.Property(c => c.CountyName).HasColumnName("CountyName");

            builder.Property(c => c.ZipCode).HasColumnName("ZipCode");

            builder.Property(c => c.ZipCodeType).HasColumnName("ZipCodeType");

            builder.Property(c => c.CountyFipsCode).HasColumnName("CountyFipsCode");

            builder.Property(c => c.CityName).HasColumnName("CityName");

            builder.Property(c => c.StateCode).HasColumnName("StateCode");

            builder.Property(c => c.AddressRecordCount).HasColumnName("AddressRecordCount");

            builder.Property(c => c.PrevalentCountyFlag).HasColumnName("PrevalentCountyFlag");

            builder.Property(c => c.MultipleCountyFlag).HasColumnName("MultipleCountyFlag");

            // Relationships

            // uses alternate key of StateCode = State.Abbreviation instead of the table key of the state
            builder
               .HasOne(c => c.State)
               .WithMany()
               .HasForeignKey(c => c.StateCode)
               .HasPrincipalKey(s => s.Abbreviation);
        }
    }
}