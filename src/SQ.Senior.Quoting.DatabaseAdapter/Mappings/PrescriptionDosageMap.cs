using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ.Senior.Quoting.DatabaseAdapter.Models;

namespace SQ.Senior.Quoting.DatabaseAdapter.Mappings {
    public class PrescriptionDosageMap : IEntityTypeConfiguration<PrescriptionDosage> {
        public void Configure(EntityTypeBuilder<PrescriptionDosage> builder) {

            builder.ToTable("DrugDosages", "dbo");
            builder.HasKey(c => c.ID);

            // Column Mappings
            builder.Property(c => c.ID).HasColumnName("DrugDosageID");
            builder.Property(c => c.PrescriptionID).HasColumnName("DrugID");
            builder.Property(c => c.DosageDRXID).HasColumnName("DosageDRXID");
            builder.Property(c => c.CommonDaysOfSupply).HasColumnName("CommonDaysOfSupply");
            builder.Property(c => c.CommonMetricQuantity).HasColumnName("CommonMetricQuantity");
            builder.Property(c => c.CommonUserQuantity).HasColumnName("CommonUserQuantity");
            builder.Property(c => c.IsCommonDosage).HasColumnName("IsCommonDosage");
            builder.Property(c => c.LabelName).HasColumnName("LabelName");
            builder.Property(c => c.ReferenceNDC).HasColumnName("ReferenceNDC");

            // Relationships
            builder
               .HasOne<Prescription>(dd => dd.Prescription)
               .WithMany()
               .HasForeignKey(dd => dd.PrescriptionID)
               .HasConstraintName("FK_DrugDosages_Drugs");

        }
    }
}